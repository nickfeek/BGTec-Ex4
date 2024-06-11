using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AzureFileStorageApi.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AzureFileStorageApi.Models.Data>> GetAllAsync()
        {
            return await _context.Data.ToListAsync();
        }

        public async Task<List<AzureFileStorageApi.Models.Data>> GetFilteredDataAsync(DateTime? startDate, DateTime? endDate)
        {
            IQueryable<AzureFileStorageApi.Models.Data> query = _context.Data;

            if (startDate != null)
                query = query.Where(data => data.TimestampProcessed.Date >= startDate.Value.Date);

            if (endDate != null)
                query = query.Where(data => data.TimestampProcessed.Date <= endDate.Value.Date);

            return await query.ToListAsync();
        }

        public async Task AddAsync(AzureFileStorageApi.Models.Data entity)
        {
            _context.Data.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
