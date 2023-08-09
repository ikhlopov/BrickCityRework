using Highsoft.Web.Mvc.Charts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BrickCityRework.Models.EF
{
    public enum Consumer_Type
    {
        None,
        House,
        Plant
    }
    public class Consumer
    {
        public int ConsumerID { get; set; }
        public string Name { get; set; }
        public int FileModelID { get; set; }
        public Consumer_Type TypeOfConsumer { get; set; }

        public virtual List<Consumption> Consumptions { get; set; }
        public virtual List<Price> Prices { get; set; }
        public virtual FileModel File { get; set; }
    }
}