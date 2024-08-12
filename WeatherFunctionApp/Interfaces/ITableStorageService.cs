using Azure;
using System;
using System.Threading.Tasks;
using WeatherFunctionApp.Models;

namespace WeatherFunctionApp.Interfaces
{
    public interface ITableStorageService
    {
        Task InsertLogAsync(WeatherLog log);

        AsyncPageable<WeatherLog> GetLogsAsync(DateTimeOffset from, DateTimeOffset to);
    }
}
