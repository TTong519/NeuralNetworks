using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticArt
{
    public class TriangleArt
    {
        int MaxTriangles;
        List<Triangle> Triangles;
        public double Error;
        public TriangleArt(int maxTriangles)
        {
            Triangles = new List<Triangle>();
            MaxTriangles = maxTriangles;
            Error = double.NaN;
        }
        public void MutateArt(Random random)
        {
            double mutationType = random.NextDouble();
            if(mutationType <= Constants.ADD_TRIANGLE_CHANCE && Triangles.Count < MaxTriangles)
            {
                Triangles.Add(Triangle.RandomTriangle(random));
            }
            else if (mutationType <= Constants.ADD_TRIANGLE_CHANCE + Constants.REMOVE_TRIANGLE_CHANCE && Triangles.Count > 0)
            {
                int indexToRemove = random.Next(0, Triangles.Count);
                Triangles.RemoveAt(indexToRemove);
            }
            else if (mutationType <= Constants.ADD_TRIANGLE_CHANCE + Constants.REMOVE_TRIANGLE_CHANCE + Constants.MUTATE_TRIANGLE_CHANCE && Triangles.Count > 0)
            {
                int indexToMutate = random.Next(0, Triangles.Count);
                Triangles[indexToMutate].Mutate(random);
            }
        }
        public Bitmap Draw()
        {
            Bitmap bmp = new Bitmap(Constants.CANVAS_WIDTH, Constants.CANVAS_HEIGHT);
            Graphics gfx = Graphics.FromImage(bmp);
            foreach (Triangle triangle in Triangles)
            {
                triangle.Draw(gfx);
            }
            return bmp;
        }
        public void UpdError(Bitmap Target)
        {
            Bitmap Output = Draw();
            int sum = 0;
            for(int i = 0; i < Constants.CANVAS_WIDTH; i++)
            {
                for(int j = 0; j < Constants.CANVAS_HEIGHT; j++)
                {
                    Color c = Target.GetPixel(i, j);
                    Color x = Output.GetPixel(i, j);
                    sum += (int)(Math.Pow(c.R - x.R, 2) + Math.Pow(c.G - x.G, 2) + Math.Pow(c.B - x.B, 2) + Math.Pow(c.A - x.A, 2));
                }
            }
            Error = sum / (Constants.CANVAS_WIDTH * Constants.CANVAS_HEIGHT);
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
