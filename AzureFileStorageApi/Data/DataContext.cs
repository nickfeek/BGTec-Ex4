// Data/DataContext.cs
using Microsoft.EntityFrameworkCore;
using AzureFileStorageApi.Models;

namespace AzureFileStorageApi.Data
{
    public class DataContext : DbContext
    {
        // virtual to allow moq to mock the Data property (TODO: maybe use interfaces instead)
        public virtual DbSet<AzureFileStorageApi.Models.Data> Data { get; set; } // Use the full namespace for clarity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AzureFileStorageApi.Models.Data>()
                .Property(d => d.TimestampProcessed)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=data.db");
    }
}
