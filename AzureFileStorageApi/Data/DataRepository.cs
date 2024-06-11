using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AzureFileStorageApi.Data
{
    // Repository class for data access operations
    public class DataRepository : IDataRepository
    {
        // Instance of the DataContext for database access
        private readonly DataContext _context;

        // Constructor for injecting DataContext dependency
        public DataRepository(DataContext context)
        {
            _context = context;
        }

        // Retrieves all data entities asynchronously
        public async Task<List<AzureFileStorageApi.Models.Data>> GetAllAsync()
        {
            return await _context.Data.ToListAsync();
        }

        // Retrieves filtered data entities based on start and end dates asynchronously
        public async Task<List<AzureFileStorageApi.Models.Data>> GetFilteredDataAsync(DateTime? startDate, DateTime? endDate)
        {
            // Start building query for data retrieval
            IQueryable<AzureFileStorageApi.Models.Data> query = _context.Data;

            // Filter data by start_date if provided
            if (startDate != null)
                query = query.Where(data => data.TimestampProcessed.Date >= startDate.Value.Date);

            // Filter data by end_date if provided
            if (endDate != null)
                query = query.Where(data => data.TimestampProcessed.Date <= endDate.Value.Date);

            // Execute the query and retrieve the data
            return await query.ToListAsync();
        }

        // Adds a new data entity asynchronously
        public async Task AddAsync(AzureFileStorageApi.Models.Data entity)
        {
            // Add the entity to the database context
            _context.Data.Add(entity);

            // Save changes to the database asynchronously
            await _context.SaveChangesAsync();
        }
    }
}
