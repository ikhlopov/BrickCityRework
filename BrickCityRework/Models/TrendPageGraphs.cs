using BrickCityRework.Models.EF;

namespace BrickCityRework.Models
{
    public class TrendPageGraphs
    {
        public List<FileModel> files = new List<FileModel>();
        public int selectedFileId;
        public TrendGraph HousesGraph { get; set; }
        public TrendGraph PlantsGraph { get; set; }
    }
}
