using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AzureFileStorageApi.Controllers;
using AzureFileStorageApi.Data;
using AzureFileStorageApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.Reflection;

namespace AzureFileStorageApi.Tests
{
    [TestClass]
    public class DataControllerTests
    {
        #nullable disable
        private Process azuriteProcess;
        #nullable enable

        [TestInitialize]
        public void Initialize()
        {
            StartAzurite();
        }

        [TestCleanup]
        public void Cleanup()
        {
            StopAzurite();
        }

        private void StartAzurite()
        {
            // Get the directory of the test project
            string? testProjectDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Path to Azurite within the test project folder
            string? azuritePath = Path.Combine(testProjectDirectory, "Azurite");

            // Start Azurite process
            azuriteProcess = new Process();
            azuriteProcess.StartInfo.FileName = "cmd.exe";
            azuriteProcess.StartInfo.Arguments = $"/C azurite";
            azuriteProcess.StartInfo.WorkingDirectory = azuritePath;
            azuriteProcess.Start();
        }

        private void StopAzurite()
        {
            // Stop Azurite process
            if (azuriteProcess != null && !azuriteProcess.HasExited)
            {
                azuriteProcess.Kill();
                azuriteProcess.WaitForExit();
                azuriteProcess.Dispose();
            }
        }

        //[TestMethod]
        //public async Task GetData_ReturnsOkResult()
        //{
        //    var testData = new List<AzureFileStorageApi.Models.Data>
        //    {
        //        new AzureFileStorageApi.Models.Data { Id = 1, Filename = "test1.txt", Size = 24, ContentType = "text/plain", FilenameExtension = ".txt", TimestampProcessed = DateTime.Now, FilePath = "http://127.0.0.1:10000/devstoreaccount1/files/profile.jpg"},
        //        new AzureFileStorageApi.Models.Data { Id = 2, Filename = "test2.txt", Size = 35, ContentType = "text/plain", FilenameExtension = ".txt", TimestampProcessed = DateTime.Now, FilePath = "http://127.0.0.1:10000/devstoreaccount1/files/beachhouse.jpg"}
        //    }.AsQueryable();

        //    var mockDbSet = new Mock<DbSet<AzureFileStorageApi.Models.Data>>();
        //    mockDbSet.As<IQueryable<AzureFileStorageApi.Models.Data>>().Setup(m => m.Provider).Returns(testData.Provider);
        //    mockDbSet.As<IQueryable<AzureFileStorageApi.Models.Data>>().Setup(m => m.Expression).Returns(testData.Expression);
        //    mockDbSet.As<IQueryable<AzureFileStorageApi.Models.Data>>().Setup(m => m.ElementType).Returns(testData.ElementType);
        //    mockDbSet.As<IQueryable<AzureFileStorageApi.Models.Data>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

        //    var mockContext = new Mock<DataContext>();
        //    mockContext.Setup(c => c.Data).Returns(mockDbSet.Object);

        //    var controller = new DataController(mockContext.Object);
        //    var result = await controller.GetData();

        //    var statusCodeResult = result as ObjectResult;
            
        //    var errorMessage = statusCodeResult?.Value as string;

        //    Assert.IsNotNull(statusCodeResult);
        //    Assert.AreEqual(200, statusCodeResult.StatusCode);

        //    // var okResult = result as OkObjectResult;
        //    // Console.WriteLine(result);
        //    // // Console.WriteLine($"status={okResult.StatusCode}");

        //    // Assert.IsNotNull(okResult);
        //    // Assert.AreEqual(200, okResult.StatusCode);

        //    // var model = okResult.Value as IEnumerable<AzureFileStorageApi.Models.Data>;
        //    // Assert.IsNotNull(model);
        //    // Assert.AreEqual(2, model.Count());
        //}


        [TestMethod]
        public async Task GetInfo_Returns_OK_With_String()
        {
            var expectedString = "This is a Azure Blob Storage file system.";
            var mockContext = new Mock<DataContext>();
            var controller = new DataController(mockContext.Object);
            var result = await Task.Run(() => controller.GetInfo()); // Using Task.Run() to execute synchronously on a background thread
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var model = okResult.Value as string;
            Assert.IsNotNull(model);
            Assert.AreEqual(expectedString, model);
        }


        [TestMethod]
        public async Task GetAllFiles_Returns_OK()
        {
            var mockContext = new Mock<DataContext>();
            var controller = new DataController(mockContext.Object);
            var result = await Task.Run(() => controller.GetAllFiles()); // Using Task.Run() to execute synchronously on a background thread
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetData_Returns_OK()
        {
            var mockContext = new Mock<DataContext>();
            var controller = new DataController(mockContext.Object);
            var result = await Task.Run(() => controller.GetData()); // Using Task.Run() to execute synchronously on a background thread
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }


    }
}
