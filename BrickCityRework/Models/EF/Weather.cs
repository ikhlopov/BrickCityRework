namespace BrickCityRework.Models.EF
{
    public class Weather
    {
        public int WeatherID { get; set; }
        public int MsrmntID { get; set; }
        public float Value { get; set; }
        public virtual Msrmnt Msrmnt { get; set; }
    }
}
