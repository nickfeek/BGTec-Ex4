using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFileStorageApi.Data
{
    public interface IDataRepository
    {
        Task<List<AzureFileStorageApi.Models.Data>> GetAllAsync();
        Task<List<AzureFileStorageApi.Models.Data>> GetFilteredDataAsync(DateTime? startDate, DateTime? endDate);
        Task AddAsync(AzureFileStorageApi.Models.Data entity);
    }
}
