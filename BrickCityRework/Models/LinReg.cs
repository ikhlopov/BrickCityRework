namespace BrickCityRework.Models
{
    public struct Point
    {
        public float Msrmnt; public float Consumption; public DateTime date;
    }


    public class LinReg
    {
        // Принимает список точек "Consumption-Msrmnt", рассчитывает коэффициенты парной регрессии, выдает спрогнозированное потребление как Consumption = predict(Msrmnt)
        private float K1;
        private float K0;
        public void SetKoef()
        {
            float msrmntSum = 0;
            float msrmntSqSum = 0;
            float msrmntConsumptionSum = 0;
            float consumptionSum = 0;

            foreach (var point in Points)
            {
                var m = Math.Abs(point.Msrmnt);
                msrmntSum += m;
                msrmntSqSum += m*m;
                msrmntConsumptionSum += m * point.Consumption;
                consumptionSum += point.Consumption;
            }

            K1 = ((Points.Count * msrmntConsumptionSum) - (msrmntSum * consumptionSum))
                    / ((Points.Count * msrmntSqSum) - (msrmntSum * msrmntSum));
            K0 = (consumptionSum - K1 * msrmntSum) / Points.Count;
            KoefCalculated = true;
        }

        public List<Point> Points { get; set; } = new List<Point>();
        public float Msrmnt;
        private bool KoefCalculated;
        public string Name { get; set; }



        public float predict()
        {
            if (!KoefCalculated) SetKoef();
            return Msrmnt * K1 + K0;
        }
    }
}
