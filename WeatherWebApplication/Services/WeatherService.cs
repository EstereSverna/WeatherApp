using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using WeatherWebApplication.Configurations;
using WeatherWebApplication.Models;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly WeatherDbContext _dbContext;
    private readonly WeatherApiSettings _apiSettings;

    public WeatherService(HttpClient httpClient, WeatherDbContext dbContext, IOptions<WeatherApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
        _apiSettings = apiSettings.Value;
    }

    public async Task<WeatherData> GetWeatherDataAsync(string city, string country)
    {
        string url = $"{_apiSettings.BaseUrl}{city},{country}&appid={_apiSettings.ApiKey}&units=metric";

        var response = await _httpClient.GetStringAsync(url);
        var data = JObject.Parse(response);

        return new WeatherData
        {
            Country = country,
            City = city,
            Temperature = data["main"]["temp"].Value<double>(),
            LastUpdateTime = DateTime.UtcNow
        };
    }

    public async Task<IEnumerable<WeatherSummary>> GetWeatherSummariesAsync()
    {
        var weatherData = await _dbContext.WeatherData.ToListAsync();

        var summaries = weatherData
            .GroupBy(w => new { w.City, w.Country })
            .Select(g => new WeatherSummary
            {
                City = g.Key.City,
                Country = g.Key.Country,
                MinTemperature = g.Min(w => w.Temperature),
                MaxTemperature = g.Max(w => w.Temperature)
            })
            .ToList();

        return summaries;
    }
}
