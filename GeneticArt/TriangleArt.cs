using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticArt
{
    public class TriangleArt
    {
        int MaxTriangles;
        List<Triangle> Triangles;
        public TriangleArt(int maxTriangles)
        {
            MaxTriangles = maxTriangles;
        }
        public void MutateArt(Random random)
        {

        }
        public void Draw(Graphics gfx)
        {
            foreach (Triangle triangle in Triangles)
            {
                triangle.Draw(gfx);
            }
        }
        public double GetError(Bitmap Target, Bitmap Output)
        {
            int sum = 0;
            for(int i = 0; i < Constants.CANVAS_WIDTH; i++)
            {
                for(int j = 0; j < Constants.CANVAS_HEIGHT; j++)
                {
                    Color c = Target.GetPixel(i, j);
                    Color x = Output.GetPixel(i, j);
                    sum += (int)Math.Pow(c.R - x.R + c.G - x.G + c.B - x.B + x.A - x.A, 2);
                }
            }
            return sum / (Constants.CANVAS_WIDTH *  Constants.CANVAS_HEIGHT);
        }
        public void CopyTo(TriangleArt other)
        {
            other = new(MaxTriangles);
            foreach (Triangle triangle in Triangles)
            {
                other.Triangles.Add(triangle);
            }
        }
    }
}
