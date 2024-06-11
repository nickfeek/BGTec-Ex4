using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureFileStorageApi.Data
{
    // Interface for the data repository, defining methods for data access
    public interface IDataRepository
    {
        // Asynchronously retrieves all data entities
        Task<List<AzureFileStorageApi.Models.Data>> GetAllAsync();

        // Asynchronously retrieves filtered data entities based on start and end dates
        Task<List<AzureFileStorageApi.Models.Data>> GetFilteredDataAsync(DateTime? startDate, DateTime? endDate);

        // Asynchronously adds a new data entity
        Task AddAsync(AzureFileStorageApi.Models.Data entity);
    }
}
