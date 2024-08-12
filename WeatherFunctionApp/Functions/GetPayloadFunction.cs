using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using WeatherFunctionApp.Interfaces;

namespace WeatherFunctionApp.Functions
{
    public class GetPayloadFunction
    {
        private readonly IBlobService _blobService;

        public GetPayloadFunction(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [FunctionName("GetPayload")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "payload/{id}")] HttpRequest req, string id)
        {
            var response = await _blobService.GetBlobContentAsync($"{id}.json");

            return new OkObjectResult(response);
        }
    }
}
