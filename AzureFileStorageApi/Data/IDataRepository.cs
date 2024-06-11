using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureFileStorageApi.Models;

namespace AzureFileStorageApi.Repositories
{
    public interface IDataRepository
    {
        Task<IEnumerable<AzureFileStorageApi.Models.Data>> GetAllAsync();
        Task<AzureFileStorageApi.Models.Data> GetByIdAsync(int id);
        Task<AzureFileStorageApi.Models.Data> AddAsync(AzureFileStorageApi.Models.Data entity);
        Task<AzureFileStorageApi.Models.Data> UpdateAsync(AzureFileStorageApi.Models.Data entity);
        Task<bool> DeleteAsync(int id);
    }
}
