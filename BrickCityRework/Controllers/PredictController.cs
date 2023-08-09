using BrickCityRework.Data;
using BrickCityRework.Models;
using BrickCityRework.Models.EF;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BrickCityRework.Controllers
{

    public class PredictController : Controller
    {
        private readonly ILogger<PredictController> _logger;
        private BrickCityContext _context;

        public PredictController(ILogger<PredictController> logger, BrickCityContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public IActionResult Predict()
        {
            var file = _context.Files.AsEnumerable().MaxBy(f => f.Date);
            var date = new FileStats(_context, file.FileModelID).MaxRecordDate;
            var FileID = file.FileModelID;

            ViewBag.files = _context.Files.ToList();
            ViewBag.date = date;
            ViewBag.selectedFileId = FileID;

            var model = new List<PredictionResult>();
            var predictions = CreatePredictions(FileID);
            foreach (var p in predictions) model.Add(p.Predict(date));
            return View(model);
        }

        [HttpPost]
        public IActionResult Predict(int FileID, DateTime date)
        {
            ViewBag.files = _context.Files.ToList();
            ViewBag.date = date;
            ViewBag.selectedFileId = FileID;

            var model = new List<PredictionResult>();
            var predictions = CreatePredictions(FileID);
            foreach (var p in predictions) model.Add(p.Predict(date));
            return View(model);
        }


        //----------   Context calls   ----------//

        public List<PredictionModel> CreatePredictions(int FileID)
        {
            var houses = _context.Consumers
                .Where(c => c.FileModelID == FileID)
                .Where(c => c.TypeOfConsumer == Consumer_Type.House)
                .Select(c => new PredictionModel
                {
                    Consumer = c,
                    points = c.Consumptions.Select(consump => new Point 
                    { 
                        Consumption = consump.Value,
                        date = consump.Date,
                        Msrmnt = consump.Msrmnt.Weather.Value
                    }).ToList()
                }).ToList();
            var plants = _context.Consumers
                .Where(c => c.FileModelID == FileID)
                .Where(c => c.TypeOfConsumer == Consumer_Type.Plant)
                .Select(c => new PredictionModel
                {
                    Consumer = c,
                    points = c.Consumptions.Select(consump => new Point
                    {
                        Consumption = consump.Value,
                        date = consump.Date,
                        Msrmnt = consump.Msrmnt.Price.Value
                    }).ToList()
                }).ToList();
            return houses.Concat(plants).ToList();

        }


    }
}
