using Microsoft.AspNetCore.Mvc;

namespace WeatherWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherSummaries()
        {
            var summaries = await _weatherService.GetWeatherSummariesAsync();
            return Ok(summaries);
        }
    }
}
