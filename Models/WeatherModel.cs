namespace Weather.Models
{
    public class WeatherModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string WeatherMain { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIcon { get; set; }
        public string MainTemp { get; set; }
        public string MainHumidity { get; set; }
    }
}