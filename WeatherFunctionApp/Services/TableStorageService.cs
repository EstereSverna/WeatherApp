using Azure;
using Azure.Data.Tables;
using System;
using System.Threading.Tasks;
using WeatherFunctionApp.Interfaces;
using WeatherFunctionApp.Models;
using Microsoft.Extensions.Logging;

namespace WeatherFunctionApp.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly TableClient _tableClient;
        private readonly ILogger<TableStorageService> _logger;

        public TableStorageService(TableServiceClient tableServiceClient, string tableName, ILogger<TableStorageService> logger)
        {
            _tableClient = tableServiceClient.GetTableClient(tableName);
            _tableClient.CreateIfNotExists();
            _logger = logger;
        }

        public async Task InsertLogAsync(WeatherLog log)
        {
            try
            {
                await _tableClient.AddEntityAsync(log);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting log: {ex.Message}");
                throw;
            }
        }

        public AsyncPageable<WeatherLog> GetLogsAsync(DateTimeOffset from, DateTimeOffset to)
        {
            try
            {
                return _tableClient.QueryAsync<WeatherLog>(e => e.Timestamp >= from && e.Timestamp <= to);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error querying logs: {ex.Message}");
                throw;
            }
        }
    }
}
