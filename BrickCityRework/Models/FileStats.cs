using BrickCityRework.Data;

namespace BrickCityRework.Models
{
    public class FileStats
    {


        private BrickCityContext _context;
        private int _fileID;

        public FileStats(BrickCityContext context, int fileID)
        {
            _context = context;
            _fileID = fileID;
        }
        public string Name { get { return _context.Files.Where(f => f.FileModelID == _fileID).Single().Name; } }
        public DateTime Date { get { return _context.Files.Where(f => f.FileModelID == _fileID).Single().Date; } }
        public DateTime MinRecordDate
        {
            get
            {
               var res = _context.Consumptions
                    .Where(c => c.Consumer.FileModelID == _fileID)
                    .Select(c => c.Date)
                    .Min();
                return res;
            }
        }

        public DateTime MaxRecordDate
        {
            get
            {
                var res = _context.Consumptions
                     .Where(c => c.Consumer.FileModelID == _fileID)
                     .Select(c => c.Date)
                     .Max();
                return res;
            }
        }

        public int ConsumersCount
        {
            get
            {
                int res = _context.Consumers
                    .Where(c => c.FileModelID == _fileID)
                    .Count();
                return res;
            }
        }

        public int ConsumptionsCount
        {
            get
            {
                var res = _context.Consumptions
                    .Where(c => c.Consumer.FileModelID == _fileID)
                    .Count();
                return res;
            }
        }

        public bool DateInDiap(DateTime date) { return date<=MaxRecordDate && date>=MinRecordDate; }
        
    }
}
