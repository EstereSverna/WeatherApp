using WeatherWebApplication.Models;

public interface IWeatherService
{
    Task<WeatherData> GetWeatherDataAsync(string city, string country);
    Task<IEnumerable<WeatherSummary>> GetWeatherSummariesAsync();
}
