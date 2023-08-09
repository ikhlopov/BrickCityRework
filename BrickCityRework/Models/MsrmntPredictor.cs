using BrickCityRework.Data;
using BrickCityRework.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace BrickCityRework.Models
{
    public class MsrmntPredictor
    {
        private BrickCityContext context;
        public MsrmntPredictor(BrickCityContext context)
        {
            this.context = context;
        }

        public float predict(DateTime date, Consumer consumer)
        {
            switch(consumer.TypeOfConsumer)
            {
                case (Consumer_Type.Plant): return WeatherPredict(date, consumer);
                case (Consumer_Type.House): return PricePredict(date, consumer);
                default: return 0;
            }
        }

        private float PricePredict(DateTime date, Consumer consumer)
        {
            List<float> LastWeek = new List<float>();
            if (consumer == null)
            {
                    LastWeek = context.Consumptions
                    .Where(c => c.Consumer == consumer)
                    .Where(c => c.Date <= date && c.Date >= date.AddDays(-7))
                    .Select(c => c.Msrmnt.Price.Value)
                    .ToList();               
            }
            return LastWeek.Sum() / LastWeek.Count;
        }
        private float WeatherPredict(DateTime date, Consumer consumer)
        {
            List<float> LastWeek = new List<float>();
            if (consumer == null)
            {
                LastWeek = context.Consumptions
                .Where(c => c.Consumer == consumer)
                .Where(c => c.Date <= date && c.Date >= date.AddDays(-7))
                .Select(c => c.Msrmnt.Weather.Value)
                .ToList();
            }
            return LastWeek.Sum() / LastWeek.Count;
        }
    }
}
