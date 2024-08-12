using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherFunctionApp.Interfaces;

namespace WeatherFunctionApp.Functions
{
    public class GetLogsFunction
    {
        private readonly ITableStorageService _tableStorageService;

        public GetLogsFunction(ITableStorageService tableStorageService)
        {
            _tableStorageService = tableStorageService;
        }

        [FunctionName("GetLogs")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "logs")] HttpRequest req)
        {
            string from = req.Query["from"];
            string to = req.Query["to"];

            if (!DateTimeOffset.TryParse(from, out DateTimeOffset fromDate) || !DateTimeOffset.TryParse(to, out DateTimeOffset toDate))
            {
                return new BadRequestObjectResult("Invalid date format. Please use a valid date format.");
            }

            var logs = await _tableStorageService.GetLogsAsync(fromDate, toDate).ToListAsync();
            return new OkObjectResult(logs);
        }
    }
}