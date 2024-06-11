using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AzureFileStorageApi
{
    public class Program
    {
        // Entry point of the application
        public static void Main(string[] args)
        {
            // Build and run the host
            CreateHostBuilder(args).Build().Run();
        }

        // Create the host builder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // Create a default host builder
            Host.CreateDefaultBuilder(args)
                // Configure the web host with default settings
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Use the Startup class to configure the application
                    webBuilder.UseStartup<Startup>();
                });
    }
}
