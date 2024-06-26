using Microsoft.AspNetCore.Mvc;
using AzureFileStorageApi.Models;
using AzureFileStorageApi.Data;
using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AzureFileStorageApi.Controllers
{
    // monolithic data controller because this project is tiny.
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public DataController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public class DataRequest
        {
            public int Id { get; set; }
            public string? Filename { get; set; }
            public string? Size { get; set; }
            public string? ContentType { get; set; }
            public string? FilenameExtension { get; set; }
            public string? FilePath { get; set; }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                // Connection settings for Azure Blob Storage
                string connectionString = "UseDevelopmentStorage=true";
                string containerName = "files";

                // Initialize BlobServiceClient and BlobContainerClient
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync();

                // Get the incoming request
                var request = HttpContext.Request;
                if (request.Form.Files.Count != 1)
                {
                    // Return a bad request response if there are no files or more than one file
                    Console.WriteLine("Bad request: Please upload a single file.");
                    return BadRequest("Please upload a single file.");
                }

                // Get the first file from the form data
                var file = request.Form.Files[0];
                if (file.Length > 0)
                {
                    // Generate a unique blob name
                    string filename = file.FileName;
                    string filenameExtension = Path.GetExtension(file.FileName);
                    string blobName = Path.GetFileNameWithoutExtension(filename) + "-" + Guid.NewGuid().ToString() + filenameExtension;
                    BlobClient blobClient = containerClient.GetBlobClient(blobName);

                    // Get file details
                    long fileSizeInBytes = file.Length;
                    long fileSizeInKB = fileSizeInBytes / 1024;
                    string contentType = file.ContentType;
                    string filepath = blobClient.Uri.ToString();

                    // Upload file to Azure Blob Storage
                    await blobClient.UploadAsync(file.OpenReadStream(), true);

                    // Save file details to database
                    var data = new AzureFileStorageApi.Models.Data
                    {
                        Filename = filename,
                        Size = fileSizeInKB.ToString(),
                        ContentType = contentType,
                        FilenameExtension = filenameExtension,
                        FilePath = filepath,
                    };

                    // Attempt to add the data to the repository
                    try
                    {
                        await _dataRepository.AddAsync(data);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions related to database operations
                        Console.WriteLine($"Database error occurred: {ex.Message}");
                        return StatusCode(500, $"Database error: {ex.Message}");
                    }

                    // Log successful file upload
                    Console.WriteLine("Image uploaded successfully.");
                    return Ok("Image uploaded successfully.");
                }
                else
                {
                    // Return a bad request response if the uploaded file is empty
                    Console.WriteLine("Bad request: Empty file uploaded.");
                    return BadRequest("Empty file uploaded.");
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions and return a 500 Internal Server Error response
                Console.WriteLine($"Internal server error occurred: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("files")]
        public async Task<IActionResult> GetAllFiles()
        {
            try
            {
                // Connection settings for Azure Blob Storage
                string connectionString = "UseDevelopmentStorage=true";
                string containerName = "files";

                // Initialize BlobServiceClient and BlobContainerClient
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync();

                // Retrieve all blobs from the container
                var blobItems = new List<string>();
                await foreach (var blobItem in containerClient.GetBlobsAsync())
                {
                    // Generate a URL to access each blob
                    var blobUri = containerClient.Uri + "/" + blobItem.Name;
                    blobItems.Add(blobUri.ToString());
                }

                // Log successful retrieval of all files
                Console.WriteLine("Retrieved all files successfully.");
                return Ok(blobItems);
            }
            catch (Exception ex)
            {
                // Log any exceptions and return a 500 Internal Server Error response
                Console.WriteLine($"Internal server error occurred: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetData(string? start_date = null, string? end_date = null)
        {
            try
            {
                // Parse dates
                DateTime? startDate = !string.IsNullOrEmpty(start_date) ? DateTime.Parse(start_date) : (DateTime?)null;
                DateTime? endDate = !string.IsNullOrEmpty(end_date) ? DateTime.Parse(end_date) : (DateTime?)null;

                // Retrieve data from the repository asynchronously
                var data = await _dataRepository.GetFilteredDataAsync(startDate, endDate);

                // Log successful retrieval of data
                Console.WriteLine("Retrieved data successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                // Log any other exceptions and return a 500 Internal Server Error response
                Console.WriteLine($"Error occurred while fetching data: {ex.Message}");
                return StatusCode(500, $"An error occurred while fetching data. Please try again later.");
            }
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            // Return information about the API
            return Ok("This is a Azure Blob Storage file system.");
        }
       }
}
