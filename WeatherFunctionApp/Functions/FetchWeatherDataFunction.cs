using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherFunctionApp.Interfaces;
using WeatherFunctionApp.Models;

namespace WeatherFunctionApp.Functions
{
    public class FetchWeatherDataFunction
    {
        private readonly HttpClient _httpClient;
        private readonly ITableStorageService _tableStorageService;
        private readonly IBlobService _blobService;

        public FetchWeatherDataFunction(HttpClient httpClient, ITableStorageService tableStorageService, IBlobService blobService)
        {
            _httpClient = httpClient;
            _tableStorageService = tableStorageService;
            _blobService = blobService;
        }

        [FunctionName("FetchWeatherData")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            string apiKey = Environment.GetEnvironmentVariable("OpenWeatherApiKey");
            string url = $"https://api.openweathermap.org/data/2.5/weather?q=London&appid={apiKey}";

            var logEntry = new WeatherLog
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                logEntry.IsSuccess = true;
                logEntry.LogMessage = "Data fetched successfully.";

                await _blobService.UploadBlobContentAsync($"{logEntry.RowKey}.json", response);

                await _tableStorageService.InsertLogAsync(logEntry);
            }
            catch (Exception ex)
            {
                logEntry.IsSuccess = false;
                logEntry.LogMessage = $"Error: {ex.Message}";

                await _tableStorageService.InsertLogAsync(logEntry);
            }
        }
    }
}