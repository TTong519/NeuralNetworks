using System.Drawing;

namespace CommonLib
{
    public static class Math
    {
        public static double MAE(this List<double> list, List<double> target)
        {
            if (list.Count != target.Count)
                throw new ArgumentException("Both lists must have the same number of elements.");
            double total = 0;
            for (int i = 0; i < list.Count; i++)
            {
                total += System.Math.Abs(list[i] - target[i]);
            }
            return total / list.Count;
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
        public static double Distance(this Point point, PointF Line)
        {
            PointF PerpLine = new();
            PerpLine.GenerateLine(-1 / Line.X, point);
            PointF ClosestPoint = Line.GetIntersection(PerpLine);
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
    }
}
