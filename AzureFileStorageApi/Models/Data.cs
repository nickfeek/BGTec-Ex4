using System;
using System.ComponentModel.DataAnnotations;

namespace AzureFileStorageApi.Models
{
    // Represents the data model for files stored in the application
    public class Data
    {
        // Primary key for the data entity
        public int Id { get; set; }

        // The filename of the file
        [Required(ErrorMessage = "Filename is required.")]
        public string Filename { get; set; } = "";

        // The size of the file
        [Required(ErrorMessage = "Size is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Size must be a number.")]
        public string Size { get; set; } = "";

        // The content type of the file
        [Required(ErrorMessage = "ContentType is required.")]
        public string ContentType { get; set; } = "";

        // The extension of the filename
        [Required(ErrorMessage = "FilenameExtension is required.")]
        public string FilenameExtension { get; set; } = "";

        // The timestamp when the file was processed
        [Required(ErrorMessage = "TimestampProcessed is required.")]
        public DateTime TimestampProcessed { get; set; }

        // The file path of the stored file (nullable)
        public string? FilePath { get; set; }
    }
}
