namespace AzureFileStorageApi.Models
{
    // Represents the data model for files stored in the application
    public class Data
    {
        // Primary key for the data entity
        public int Id { get; set; }

        // The filename of the file
        public string Filename { get; set; } = "";

        // The size of the file
        public string Size { get; set; } = "";

        // The content type of the file
        public string ContentType { get; set; } = "";

        // The extension of the filename
        public string FilenameExtension { get; set; } = "";

        // The timestamp when the file was processed
        public DateTime TimestampProcessed { get; set; }

        // The file path of the stored file (nullable)
        public string? FilePath { get; set; }
    }
}
