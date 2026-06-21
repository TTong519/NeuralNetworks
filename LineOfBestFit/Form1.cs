using CommonLib;
using System.Net.Mail;
using System.Numerics;
using static System.Math;

namespace LineOfBestFit
{
    public partial class Form1 : Form
    {
        List<Point> Points = [];
        Bitmap? bmp;
        Graphics? gfx;
        Perceptron Line;

        public Form1()
        {
            InitializeComponent();
        }

        private void Display_Click(object? sender, EventArgs e)
        {
            MouseEventArgs mouseE = (MouseEventArgs)e;
            for (int i = 0; i < 1; i++)
            {
                Points.Add(new(mouseE.X, mouseE.Y));
            }
        }

        private void DrawButton_Click(object? sender, EventArgs e)
        {
            PointF[] points = Points.Select(p => new PointF(p.X, p.Y)).ToArray();
            points.Normalize(new(0, 0), new(Display.Width, Display.Height), new(0, 0), new(5, 5));
            double[][] inputs = new double[Points.Count][];
            double[] targets = new double[Points.Count];
            foreach (var (point, index) in points.Select((value, i) => (value, i)))
            {
                inputs[index] = new[] { (double)point.X };
                targets[index] = point.Y;
            }
            for (int i = 0; i < 1000; i++)
            {
                Line.TrainWithGradientDecent(inputs, targets);
            }
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            Points = [];
            Line = new(initWeights : [0.0], 2.5, 0.5, ErrorFuncs.MSE, ActivationFuncs.Identity, Random.Shared, 0.05);
            bmp = new Bitmap(Display.Width, Display.Height);
            gfx = Graphics.FromImage(bmp);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            gfx.Clear(BackColor);
            foreach (Point p in Points)
            {
                gfx.FillEllipse(Brushes.Black, p.X - 5, p.Y - 5, 10, 10);
            }
            PointF Left = new(0, (float)Line.Compute([0.0]));
            PointF Right = new(5, (float)Line.Compute([5.0]));
            Left.Normalize(new(0, 0), new(5, 5), new(0, 0), new(Display.Width, Display.Height));
            Right.Normalize(new(0, 0), new(5, 5), new(0, 0), new(Display.Width, Display.Height));
            gfx.DrawLine(Pens.Red, Left, Right);
            Display.Image = bmp;
        }
    }
}
