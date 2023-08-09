using Highsoft.Web.Mvc.Charts;
using System;

namespace BrickCityRework.Models.EF
{
    public class Consumption
    {

        public int ConsumptionID { get; set; }
        public int ConsumerID { get; set; }
        public int MsrmntID { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }

        public virtual Msrmnt Msrmnt { get; set; }
        public virtual Consumer Consumer { get; set; }

        //---------------------------------   Функции взаимодействия с сущностью потребления   --------------------------------//

        public bool DateIsInRange(DateTime from, DateTime to)
        {
            return Date <= to && Date >= from;
        }
    }



}
