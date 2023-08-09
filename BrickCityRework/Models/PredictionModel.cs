using BrickCityRework.Models.EF;

namespace BrickCityRework.Models
{
    public class PredictionResult
    {
        public string Name; public float predict;
    }
    public class PredictionModel
    {

        private LinReg LinReg = new LinReg();
        public List<Point> points { get; set; } = new List<Point>();
        public Consumer Consumer { get; set; } = new Consumer();

        private float MsrmntPredict(DateTime date)
        {
            List<float> LastWeek = new List<float>();

            LastWeek = points
            .Where(p => p.date <= date && p.date >= date.AddDays(-7))
            .Select(p => p.Msrmnt)
            .ToList();
                      
            return LastWeek.Sum() / LastWeek.Count;
        }

        public PredictionResult Predict(DateTime date)
        {
            LinReg.Points = points;
            LinReg.Msrmnt = Math.Abs(MsrmntPredict(date));
            var predict = LinReg.predict();
            return new PredictionResult { Name = Consumer.Name, predict = predict};
        }
    }
}
