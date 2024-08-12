using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace WeatherFunctionApp.Interfaces
{
    public interface IBlobService
    {
        Task<string> GetBlobContentAsync(string blobName);

        Task<BlobContentInfo> UploadBlobContentAsync(string blobName, string content);
    }
}