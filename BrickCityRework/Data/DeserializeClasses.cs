
using BrickCityRework.Models.EF;
using System.Text.Json;

namespace BrickCityRework.Data
{
    public class City
    {
        public List<ConsumerDS>? houses {
            get;
            set; 
        }
        public List<ConsumerDS>? plants { get; set; }
        public static City GetCityFromJSON(string jsonString)
        {
            var city = JsonSerializer.Deserialize<City>(jsonString);
            return city;
        }
        public IEnumerable<ConsumerDS> all { get { return Enumerable.Concat(houses, plants).ToList(); } }
    }

    public class ConsumerDS
    {
        public int ConsumerId { get; set; }
        public string Name { get; set; }
        public List<ConsumptionDS> consumptions { get; set; }
        public Consumer_Type ConsumerType { get; set; }
    }
    public class ConsumptionDS
    {
        public DateTime Date { get; set; }
        public float Weather { get; set; }
        public float Price { get; set; }
        public float Consumption { get; set; }
    }
}
