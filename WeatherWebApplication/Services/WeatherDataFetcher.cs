public class WeatherDataFetcher : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public WeatherDataFetcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();
                var dbContext = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();

                var cities = new[] { ("Riga", "Latvia"), ("Liepaja", "Latvia"), ("Helsinki", "Finland"), ("Tampere", "Finland"), ("Madrid", "Spain"), ("Barcelona", "Spain") };

                foreach (var (city, country) in cities)
                {
                    var weatherData = await weatherService.GetWeatherDataAsync(city, country);
                    dbContext.WeatherData.Add(weatherData);
                }

                await dbContext.SaveChangesAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
