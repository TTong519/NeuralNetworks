using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GeneticArt
{
    internal class Triangle
    {
        Color color;
        Vector2[] points;
        public Triangle(Color color, Vector2[] points)
        {
            this.color = color;
            this.points = points;
        }
        public void Mutate(Random r)
        {

        }
    }
}
