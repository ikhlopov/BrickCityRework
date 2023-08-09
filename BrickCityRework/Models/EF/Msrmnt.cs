using System.Collections.Generic;

namespace BrickCityRework.Models.EF
{
    public enum Msrmnt_Type
    {
        None,
        Weather,
        Price
    }
    public class Msrmnt
    {
        public int MsrmntID { get; set; }
        public Msrmnt_Type Msrmnt_Type { get; set; }

        public virtual Consumption Consumption { get; set; }
        public virtual Price Price { get; set; }
        public virtual Weather Weather { get; set; }
    }
}
