using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using WeatherFunctionApp.Interfaces;

namespace WeatherFunctionApp.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobContainerClient = blobServiceClient.GetBlobContainerClient("weatherdata");
        }

        public async Task<string> GetBlobContentAsync(string blobName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);

            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (var streamReader = new StreamReader(download.Content))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        public async Task<BlobContentInfo> UploadBlobContentAsync(string blobName, string content)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            return await blobClient.UploadAsync(new BinaryData(content));
        }
    }
}