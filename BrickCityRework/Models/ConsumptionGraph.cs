using Highsoft.Web.Mvc.Charts;

namespace BrickCityRework.Models
{
    public class ConsumptionGraph
    {
        // Создается заполненный шаблон графика потребления
        public Highcharts _graph = new Highcharts
        {
            Title = new Title { Text = "Распределение потребления" },
            Chart = new Chart
            {
                Type = ChartType.Area,
                ZoomType = ChartZoomType.X,
                Panning = new ChartPanning { Enabled = true },
                PanKey = ChartPanKey.Shift,

            },
            XAxis = new List<XAxis> { new XAxis { Type = "datetime" } },
            YAxis = new List<YAxis> { new YAxis { Title = new YAxisTitle { Text = "кВт" } } },
            PlotOptions = new PlotOptions
            {
                Area = new PlotOptionsArea
                {
                    Stacking = PlotOptionsAreaStacking.Normal
                }
            },
            Series = new List<Series>(),
            ID = "ConsumptionAreaGraph"
        };

        public void AddSeries(List<AreaSeries> series)
        {
            foreach (var s in series) _graph.Series.Add(s);
        }



        public Highcharts Graph { get { return _graph; } }
    }
}
