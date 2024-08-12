using System.ComponentModel.DataAnnotations;

namespace WeatherWebApplication.Models
{
    public class WeatherData
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(56)]
        public string Country { get; set; }

        [MaxLength(85)]
        public string City { get; set; }

        public double Temperature { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
