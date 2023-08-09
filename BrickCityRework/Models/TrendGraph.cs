using BrickCityRework.Models.EF;
using Highsoft.Web.Mvc.Charts;

namespace BrickCityRework.Models
{
    public struct setts
    {
        public string ID, plotTitle, XaxText;
    }
    public class TrendGraph
    {
        private readonly setts PlantSetts = new setts
        {
            ID = "plantsTrend",
            plotTitle = "Зависимость потребления заводов от цены на кирпич",
            XaxText = "Цена кирпича"
        };
        private readonly setts HouseSetts = new setts
        {
            ID = "housesTrend",
            plotTitle = "Зависимость потребления жилых домов от температуры",
            XaxText = "Температура, град."
        };

        private Highcharts graph = new Highcharts
        {
            Chart = new Chart
            {
                Type = ChartType.Scatter,
                ZoomType = ChartZoomType.Xy,
                Panning = new ChartPanning { Enabled = true },
                PanKey = ChartPanKey.Shift
            },
            YAxis = new List<YAxis>
            {
                new YAxis
                {
                    Title = new YAxisTitle { Text = "Потребление" }
                }
            }
        };
        private setts activeSetts;

        public TrendGraph(Consumer_Type type, List<ScatterSeries> series)
        {
            switch (type)
            {
                case Consumer_Type.House:
                    activeSetts = HouseSetts; break;
                case Consumer_Type.Plant:
                    activeSetts = PlantSetts; break;
            }
            graph.ID = activeSetts.ID;
            graph.XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle { Text = activeSetts.XaxText }
                }
            };
            graph.Title = new Title { Text = activeSetts.plotTitle };
            foreach (var s in series) graph.Series.Add(s);
        }

        public Highcharts Graph { get { return graph; } }
    }
}
