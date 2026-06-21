using System.Drawing;
using System.Numerics;

namespace CommonLib
{
    public static class Math
    {
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
        public static double Distance(this Point point1, Point point2)
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
        public static void Normalize(this ref double value, double min, double max, double dmin, double dmax)
        {
            value = ((value - min) / (max - min)) * (dmax - dmin) + dmin;
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
        public static void Normalize(this ref PointF point, PointF min, PointF max, PointF dmin, PointF dmax)
        {
            point.X = ((point.X - min.X) / (max.X - min.X)) * (dmax.X - dmin.X) + dmin.X;
            point.Y = ((point.Y - min.Y) / (max.Y - min.Y)) * (dmax.Y - dmin.Y) + dmin.Y;
        }
        public static void Normalize(this List<PointF> points, PointF min, PointF max, PointF dmin, PointF dmax)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new(((points[i].X - min.X) / (max.X - min.X)) * (dmax.X - dmin.X) + dmin.X, ((points[i].Y - min.Y) / (max.Y - min.Y)) * (dmax.Y - dmin.Y) + dmin.Y);
            }
        }
        public static void Normalize(this PointF[] points, PointF min, PointF max, PointF dmin, PointF dmax)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new(((points[i].X - min.X) / (max.X - min.X)) * (dmax.X - dmin.X) + dmin.X, ((points[i].Y - min.Y) / (max.Y - min.Y)) * (dmax.Y - dmin.Y) + dmin.Y);
            }
        }
        public static Vector2 Normalize(Vector2 point, Vector2 minimum, Vector2 max, Vector2 dmin, Vector2 dmax)
        {
            point.X = ((point.X - minimum.X) / (max.X - minimum.X)) * (dmax.X - dmin.X) + dmin.X;
            point.Y = ((point.Y - minimum.Y) / (max.Y - minimum.Y)) * (dmax.Y - dmin.Y) + dmin.Y;
            return point;
        }
        public static double Identity(double val)
        {
            return val;
        }
    }
}
