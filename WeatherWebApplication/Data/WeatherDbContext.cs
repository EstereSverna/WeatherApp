using Microsoft.EntityFrameworkCore;
using WeatherWebApplication.Models;

public class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

    public DbSet<WeatherData> WeatherData { get; set; }
}
