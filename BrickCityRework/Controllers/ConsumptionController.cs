using BrickCityRework.Data;
using BrickCityRework.Models;
using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BrickCityRework.Controllers
{
    public class ConsumptionController : Controller
    {
        private readonly ILogger<ConsumptionController> _logger;
        private BrickCityContext _context;

        public ConsumptionController(ILogger<ConsumptionController> logger, BrickCityContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Consumption()
        {
            var formData = DefaultFormData();
            return ShowConsumptionGraph(formData);

        }

        [HttpPost]
        public IActionResult Consumption(ConsumpGraphFormData formData)
        {
            return ShowConsumptionGraph(formData);
        }

        private ActionResult ShowConsumptionGraph(ConsumpGraphFormData formData)
        {
            FormDataCorrect(formData);
            formData.Files = _context.Files.ToList();
            var graphData = GetConsumpAreaGraphDataFromContext(formData);

            var model = new ConsumptionGraph();
            model.AddSeries(graphData);

            ViewBag.FormData = formData;
            return View("ConsumptionAreaGraph", model);
        }

        //----------   Context calls   ----------//

        //Если выбранные даты вне диапазона - устанавливаются граничные даты
        private void FormDataCorrect(ConsumpGraphFormData dataToCorrect)
        {
            var stats = new FileStats(_context, dataToCorrect.FileID);

            if ( !stats.DateInDiap(dataToCorrect.Fromdate)) { dataToCorrect.Fromdate = stats.MinRecordDate; }
            if ( !stats.DateInDiap(dataToCorrect.Todate)) { dataToCorrect.Todate = stats.MaxRecordDate; }
            
        }

        //Дефолтное заполнение параметров (самый новый файл, крайние даты)
        private ConsumpGraphFormData DefaultFormData()
        {
            ConsumpGraphFormData data = new ConsumpGraphFormData();
            data.FileID = _context.Files.AsEnumerable().MaxBy(f => f.Date).FileModelID;
            return data;
        }

        // Запрос содержимого графика из БД по заданным параметрам
        private List<AreaSeries> GetConsumpAreaGraphDataFromContext(ConsumpGraphFormData formData)
        {
            // TODO: добавить в лог сколько времени занял запрос
            List<AreaSeries> graphData = _context.Consumers
                .Where(consumer => consumer.FileModelID == formData.FileID)
                .Select(
                Consumer => new AreaSeries
                {
                    Name = Consumer.Name,
                    Data = Consumer.Consumptions
                    .Where(c => (c.Date >= formData.Fromdate && c.Date <= formData.Todate))
                    .Select(
                    consump => new AreaSeriesData
                    {
                        X = (consump.Date - DateTime.UnixEpoch).TotalMilliseconds,
                        Y = consump.Value
                    }).ToList()
                }).ToList();

            return graphData;
        }
    }
}
