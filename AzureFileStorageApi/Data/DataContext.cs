using Microsoft.EntityFrameworkCore;
using AzureFileStorageApi.Models;

namespace AzureFileStorageApi.Data
{
    // DbContext class for managing database connections and entities
    public class DataContext : DbContext
    {
        // DbSet representing the Data entity in the database
        // Use the full namespace for clarity
        public DbSet<AzureFileStorageApi.Models.Data> Data { get; set; }

        // Configures the model for the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set default value and generation behavior for TimestampProcessed property
            modelBuilder.Entity<AzureFileStorageApi.Models.Data>()
                .Property(d => d.TimestampProcessed)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        }

        // Configures the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=data.db");
    }
}
