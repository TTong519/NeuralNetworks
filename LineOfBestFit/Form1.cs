using CommonLib;
using static System.Math;
namespace LineOfBestFit
{
    public partial class Form1 : Form
    {
        List<Point> Points;
        Bitmap bmp;
        Graphics gfx;
        PointF Line;
        List<double> MAEHistory;
        double RecentMAERange;
        double MutationRate;
        public Form1()
        {
            InitializeComponent();
        }

        private void Display_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseE = (MouseEventArgs)e;
            Points.Add(new(mouseE.X, mouseE.Y));
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            while(RecentMAERange < 0.05)
            {
                if(MAEHistory.Count >= 2)
                {
                    List<double> temp = new List<double>();
                    for(int i = 0; i < Min(MAEHistory.Count, 10); i++)
                    {
                        temp.Add(MAEHistory[i]);
                    }
                    RecentMAERange = Abs(temp.Max() - temp.Min());
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Points = new();
            Line = new();
            bmp = new Bitmap(Display.Width, Display.Height);
            gfx = Graphics.FromImage(bmp);
            Line = new();
            Line.GenerateLine(new PointF(Display.Left, Display.Top), new PointF(Display.Right, Display.Bottom));
            MAEHistory = new();
            RecentMAERange = double.MaxValue;
            MutationRate = 0.25;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gfx.Clear(BackColor);
            foreach(Point p in Points)
            {
                gfx.FillEllipse(Brushes.Black, p.X - 5, p.Y - 5, 10, 10);
            }
            Display.Image = bmp;
        }
    }
}
