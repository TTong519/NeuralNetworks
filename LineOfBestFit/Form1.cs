using CommonLib;
using System.Numerics;
using static System.Math;

namespace LineOfBestFit
{
    public partial class Form1 : Form
    {
        List<Point> Points = [];
        Bitmap? bmp;
        Graphics? gfx;
        PointF Line;
        PointF LastLine;
        List<double> MAEHistory = [];
        double RecentMAERange;
        double MutationRate;

        public Form1()
        {
            InitializeComponent();
        }

        private void Display_Click(object? sender, EventArgs e)
        {
            MouseEventArgs mouseE = (MouseEventArgs)e;
            for (int i = 0; i < 5; i++)
            {
                Points.Add(new(mouseE.X, mouseE.Y));
            }
        }

        private void DrawButton_Click(object? sender, EventArgs e)
        {
            while (RecentMAERange > 0.001)
            {
                if (MAEHistory.Count >= 2)
                {
                    var temp = MAEHistory.Take(Min(MAEHistory.Count, 10)).ToList();
                    RecentMAERange = Abs(temp.Max() - temp.Min());
                }
                var dists = new List<double>();
                foreach (Point p in Points)
                {
                    dists.Add(p.Distance((Vector2)Line));
                }
                double mae = dists.Average();
                if (mae > MAEHistory[0])
                {
                    Line = new((Vector2)LastLine);
                }
                else
                {
                    LastLine = new((Vector2)Line);
                    MAEHistory.Insert(0, mae);
                }
                Line.X += (float)((Random.Shared.NextDouble() - 0.5) * MutationRate);
            }
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            Points = [];
            Line = new();
            bmp = new Bitmap(Display.Width, Display.Height);
            gfx = Graphics.FromImage(bmp);
            Line.GenerateLine(new PointF(Display.Left, Display.Top), new PointF(Display.Right, Display.Bottom));
            LastLine = new((Vector2)Line);
            MAEHistory = [double.MaxValue];
            RecentMAERange = double.MaxValue;
            MutationRate = 2;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            gfx.Clear(BackColor);
            foreach (Point p in Points)
            {
                gfx.FillEllipse(Brushes.Black, p.X - 5, p.Y - 5, 10, 10);
            }
            gfx.DrawLine(Pens.Red, Display.Left, Line.X * Display.Left + Line.Y, Display.Right, Line.X * Display.Right + Line.Y);
            Display.Image = bmp;
        }
    }
}
