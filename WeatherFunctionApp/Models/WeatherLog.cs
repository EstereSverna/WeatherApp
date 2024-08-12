using Azure;
using System;
using Azure.Data.Tables;

namespace WeatherFunctionApp.Models
{
    public class WeatherLog : ITableEntity
    {
        public string PartitionKey { get; set; } = "WeatherLog";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset? Timestamp { get; set; }
        public string LogMessage { get; set; }
        public bool IsSuccess { get; set; }
        public ETag ETag { get; set; }
    }
}
