using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrickCityRework.Models.EF
{
    public class FileModel
    {

        public int FileModelID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Consumer> Consumers { get; set; }

        [NotMapped]
        public virtual string Content { get; set; }

    }
}
