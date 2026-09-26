using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Drawing;

namespace GeneticArt
{
    internal class Triangle
    {
        (byte R, byte G, byte B, byte A) color;
        Vector2[] points;
        public Triangle((byte R, byte G, byte B, byte A) color, Vector2[] points)
        {
            this.color = color;
            this.points = points;
        }
        public void Mutate(Random r)
        {

            if (r.NextDouble() < Constants.COLOR_MUTATION_CHANCE)
            {
                int mutationChannel = r.Next(1, 5);
                if (mutationChannel == 1)
                {
                    color.R = (byte)Math.Clamp(color.R + r.Next(Constants.COLOR_MUTATION_AMOUNT_MIN, Constants.COLOR_MUTATION_AMOUNT_MAX + 1), 0, 255);
                }
                else if (mutationChannel == 2)
                {
                    color.G = (byte)Math.Clamp(color.G + r.Next(Constants.COLOR_MUTATION_AMOUNT_MIN, Constants.COLOR_MUTATION_AMOUNT_MAX + 1), 0, 255);
                }
                else if (mutationChannel == 3)
                {
                    color.B = (byte)Math.Clamp(color.B + r.Next(Constants.COLOR_MUTATION_AMOUNT_MIN, Constants.COLOR_MUTATION_AMOUNT_MAX + 1), 0, 255);
                }
                else
                {
                    color.A = (byte)Math.Clamp(color.A + r.Next(Constants.COLOR_MUTATION_AMOUNT_MIN, Constants.COLOR_MUTATION_AMOUNT_MAX + 1), 0, 255);
                }
            }
            else
            {
                int mutationPoint = r.Next(0, 3);
                points[mutationPoint] += new Vector2(
                    points[mutationPoint].X + (float)(r.NextDouble() * (Constants.POINT_MUTATION_AMOUNT_MAX - Constants.POINT_MUTATION_AMOUNT_MIN) + Constants.POINT_MUTATION_AMOUNT_MIN),
                    points[mutationPoint].Y + (float)(r.NextDouble() * (Constants.POINT_MUTATION_AMOUNT_MAX - Constants.POINT_MUTATION_AMOUNT_MIN) + Constants.POINT_MUTATION_AMOUNT_MIN)
                );
            }
        }
        public Triangle Clone()
        {
            return new Triangle(color, (Vector2[])points.Clone());
        }
        public static Triangle RandomTriangle(Random r)
        {
            (byte R, byte G, byte B, byte A) color = (
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256),
                (byte)r.Next(Constants.RAND_COLOR_A_MIN, Constants.RAND_COLOR_A_MAX + 1)
            );
            Vector2[] points = new Vector2[3];
            double offsetX = r.NextDouble() * (1 - Constants.RAND_TRIANGLE_POINT_MAX);
            double offsetY = r.NextDouble() * (1 - Constants.RAND_TRIANGLE_POINT_MAX);
            for (int i = 0; i < 3; i++)
            {
                points[i] = new Vector2(
                    (float)((r.NextDouble() * (Constants.RAND_TRIANGLE_POINT_MAX - Constants.RAND_TRIANGLE_POINT_MIN) + Constants.RAND_TRIANGLE_POINT_MIN) + offsetX),
                    (float)((r.NextDouble() * (Constants.RAND_TRIANGLE_POINT_MAX - Constants.RAND_TRIANGLE_POINT_MIN) + Constants.RAND_TRIANGLE_POINT_MIN) + offsetY)
                );
            }
            return new Triangle(color, points);
        }
        public void Draw(Graphics gfx)
        {
            Point[] Verticies = new Point[3];
            for (int i = 0; i < 3; i++)
            {
                Verticies[i] = new Point(
                    (int)(points[i].X * Constants.CANVAS_WIDTH),
                    (int)(points[i].Y * Constants.CANVAS_HEIGHT)
                );
            }
            gfx.FillPolygon(new SolidBrush(Color.FromArgb(color.A, color.R, color.G, color.B)), Verticies);
        }
    }
}
