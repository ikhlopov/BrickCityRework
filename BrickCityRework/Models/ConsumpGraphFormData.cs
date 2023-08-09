using BrickCityRework.Models.EF;

namespace BrickCityRework.Models
{
    public class ConsumpGraphFormData
    {
        public List<FileModel> Files { get; set; }
        public int FileID { get; set; } = -1;
        public DateTime Fromdate { get; set; } = DateTime.MinValue;
        public DateTime Todate { get; set; } = DateTime.MinValue;

        public string strFromDate { get { return Fromdate.ToString("yyyy-MM-dd"); } }
        public string strToDate { get { return Todate.ToString("yyyy-MM-dd"); } }
    }
}
