using BrickCityRework.Data;
using BrickCityRework.Models;
using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace BrickCityRework.Controllers
{
    public class TrendController : Controller
    {
        private readonly ILogger<TrendController> _logger;
        private BrickCityContext _context;

        public TrendController(ILogger<TrendController> logger, BrickCityContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Trend()
        {
            int fileID = _context.Files.AsEnumerable().MaxBy(f => f.Date).FileModelID;
            return Trend(fileID);
        }
        [HttpPost]
        public IActionResult Trend(int fileID)
        {
            var model = new TrendPageGraphs
            {
                HousesGraph = new TrendGraph(Models.EF.Consumer_Type.House, GetWeatherConsumptionSeries(_context, fileID)),
                PlantsGraph = new TrendGraph(Models.EF.Consumer_Type.Plant, GetPriceConsumptionSeries(_context, fileID)),
                files = _context.Files.ToList(),
                selectedFileId = fileID
            };

            return View(model);
        }





        //----------   Context calls   ----------//

        private List<ScatterSeries> GetWeatherConsumptionSeries(BrickCityContext context, int FileID)
        {
            List<ScatterSeries> result = new List<ScatterSeries>();
            result = context.Consumers
                .Where(c => c.FileModelID == FileID)
                .Where(c => c.TypeOfConsumer == Models.EF.Consumer_Type.House)
                .Select(consumer => new ScatterSeries
                {
                    Name = consumer.Name,
                    Data = consumer.Consumptions
                    .Select(
                    consump => new ScatterSeriesData
                    {
                        X = consump.Msrmnt.Weather.Value,
                        Y = consump.Value
                    }).ToList()
                }).ToList();
            return result;
        }

        private List<ScatterSeries> GetPriceConsumptionSeries(BrickCityContext context, int FileID)
        {
            List<ScatterSeries> result = new List<ScatterSeries>();
            result = context.Consumers
                .Where(c => c.FileModelID == FileID)
                .Where(c => c.TypeOfConsumer == Models.EF.Consumer_Type.Plant)
                .Select(consumer => new ScatterSeries
                {
                    Name = consumer.Name,
                    Data = consumer.Consumptions
                    .Select(
                    consump => new ScatterSeriesData
                    {
                        X = consump.Msrmnt.Price.Value,
                        Y = consump.Value
                    }).ToList()
                }).ToList();
            return result;
        }
    }
}
