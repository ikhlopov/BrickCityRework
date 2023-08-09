namespace BrickCityRework.Models.EF
{
    public class Price
    {
        public int PriceID { get; set; }
        public int MsrmntID { get; set; }
        public int ConsumerID { get; set; }
        public float Value { get; set; }

        public virtual Msrmnt Msrmnt { get; set; }
        public virtual Consumer Consumer { get; set; }
    }
}
