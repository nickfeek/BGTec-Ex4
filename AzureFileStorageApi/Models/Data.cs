namespace AzureFileStorageApi.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Filename { get; set; } = "";
        public string Size { get; set; } = "";
        public string ContentType { get; set; } = "";
        public string FilenameExtension { get; set; } = "";
        public DateTime TimestampProcessed { get; set; } 
        public string? FilePath { get; set; }

    }
}
