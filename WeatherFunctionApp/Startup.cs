using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WeatherFunctionApp.Services;
using WeatherFunctionApp.Interfaces;
using Azure.Storage.Blobs;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;
using System;

[assembly: FunctionsStartup(typeof(WeatherFunctionApp.Startup))]

namespace WeatherFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(sp =>
            {
                string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                return new TableServiceClient(connectionString);
            });

            builder.Services.AddSingleton<ITableStorageService, TableStorageService>(sp =>
            {
                var tableServiceClient = sp.GetRequiredService<TableServiceClient>();
                var logger = sp.GetRequiredService<ILogger<TableStorageService>>();
                return new TableStorageService(tableServiceClient, "WeatherLogs", logger);
            });

            builder.Services.AddSingleton(sp =>
            {
                string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                return new BlobServiceClient(connectionString);
            });

            builder.Services.AddSingleton<IBlobService, BlobService>();
        }
    }
}