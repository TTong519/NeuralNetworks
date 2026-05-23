using System.Drawing;

namespace CommonLib
{
    public static class Math
    {
        public static double MAE(this List<double> list, List<double> target)
        {
            if (list.Count != target.Count) throw new ArgumentException("Both lists must have the same number of elements.");
            double total = 0;
            for (int i = 0; i < list.Count; i++)
            {
                total += AE(list[i], target[i]);
            }
            return total / list.Count;
        }
        public static double MSE(this List<double> list, List<double> target)
        {
            if (list.Count != target.Count) throw new ArgumentException("Both lists must have the same number of elements.");
            double total = 0;
            for (int i = 0; i < list.Count; i++)
            {
                total += SE(list[i], target[i]);
            }
            return total / list.Count;
        }
        public static double AE(double val, double target)
        {
            return System.Math.Abs(val - target);
        }
        public static double SE(double val, double target)
        {
            return System.Math.Pow(val - target, 2);
        }
        public static PointF GetIntersection(this PointF L1, PointF L2)
        {
            float X = (L2.Y - L1.Y) / (L1.X - L2.X);
            float Y = (X * L1.X) + L1.Y;
            return new(X, Y);
        }
        public static void GenerateLine(this ref PointF Out, float slope, PointF point)
        {
            Out = new(slope, point.Y - (slope * point.X));
        }
        public static void GenerateLine(this ref PointF Out, PointF point1, PointF point2)
        {
            float slope = (point2.Y - point1.Y) / (point2.X - point1.X);
            Out = new(slope, point1.Y - (slope * point1.X));
        }
        public static double Distance(this PointF point1, PointF point2)
        {
            return System.Math.Sqrt(System.Math.Pow(point2.X - point1.X, 2) + System.Math.Pow(point2.Y - point1.Y, 2));
        }
        public static double Distance(this Point point, System.Numerics.Vector2 Line)
        {
            PointF PerpLine = new();
            PerpLine.GenerateLine((float)System.Math.ReciprocalEstimate(-1 * Line.X), point);
            PointF ClosestPoint = ((PointF)Line).GetIntersection(PerpLine);
            return Distance(point, ClosestPoint);
        }
        public static double Average(this List<double> list)
        {
            double total = 0;
            foreach (double d in list)
            {
                total += d;
            }
            return total / list.Count;
        }
        public static void Normalize(this ref double val, double min, double max, double dmin, double dmax)
        {
            val = ((val - min) / (max - min)) * (dmax - dmin) + dmin;
        }
        public static void Normalize(this double[] vals, double dmin, double dmax)
        {
            double min = vals.Min();
            double max = vals.Max();
            for (int i = 0; i < vals.Length; i++)
            {
                vals[i] = ((vals[i] - min) / (max - min)) * (dmax - dmin) + dmin;
            }
        }
        public static void Normalize(this List<double> vals, double dmin, double dmax)
        {
            double min = vals.Cast<double>().Min();
            double max = vals.Cast<double>().Max();
            for (int i = 0; i < vals.Count; i++)
            {
                vals[i] = ((vals[i] - min) / (max - min)) * (dmax - dmin) + dmin;
            }
        }
    }
}
