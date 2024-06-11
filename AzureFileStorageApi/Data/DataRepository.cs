using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AzureFileStorageApi.Data;
using AzureFileStorageApi.Models;

namespace AzureFileStorageApi.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AzureFileStorageApi.Models.Data>> GetAllAsync()
        {
            return await _context.Data.ToListAsync();
        }

        public async Task<AzureFileStorageApi.Models.Data> GetByIdAsync(int id)
        {
            return await _context.Data.FindAsync(id);
        }

        public async Task<AzureFileStorageApi.Models.Data> AddAsync(AzureFileStorageApi.Models.Data entity)
        {
            _context.Data.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AzureFileStorageApi.Models.Data> UpdateAsync(AzureFileStorageApi.Models.Data entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Data.FindAsync(id);
            if (entity == null)
                return false;

            _context.Data.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
