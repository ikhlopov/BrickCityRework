
using BrickCityRework.Data;
using BrickCityRework.Models;
using BrickCityRework.Models.EF;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BrickCityRework.Controllers
{
    public class FileController : Controller
    {
        // Контроллер вкладки "Файл"
        private BrickCityContext _context;

        public FileController(BrickCityContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<FileStats> stats = new List<FileStats>();
            List<FileModel> FileList = _context.Files.ToList();
            foreach (var item in FileList)
            {
                var stat = new FileStats(_context, item.FileModelID);
                stats.Add(stat);
            }
            return View("File", stats);
        }

        [HttpPost]
        public ActionResult Upload(IFormFile upload, string filename)
        {
            if (upload != null)
            {
                var newFile = AddFile(_context, FileStringFromUpload(upload), filename);
                DbInitializer.AddConsumersFromFileToContext(newFile, _context);
            }
            return RedirectToAction("Index");
        }

        // Принимает форму загрузки файла и выдает строку с его содержимым
        private string FileStringFromUpload(IFormFile upload)
        {
            string result;
            using (MemoryStream ms = new MemoryStream())
            {
                upload.CopyTo(ms);
                result = Encoding.UTF8.GetString(ms.ToArray());
            }
            return result;
        }

        private FileModel AddFile(BrickCityContext context, string content, string fileName)
        {
            FileModel newFile = new FileModel
            {
                Date = DateTime.Now,
                Name = fileName,
                Content = content

            };
            context.Files.Add(newFile);
            context.SaveChanges();
            return newFile;
        }


    }
}
