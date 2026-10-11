namespace GeneticArt
{
    public partial class Form1 : Form
    {
        Bitmap inBmp;
        Bitmap outBmp;
        List<TriangleArt> Population;
        public Form1()
        {
            InitializeComponent();
        }

        private void In_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void Out_Click(object sender, EventArgs e)
        {

            saveFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            inBmp = new Bitmap(openFileDialog.FileName);
            In.Image = inBmp;
            foreach (TriangleArt art in Population)
            {
                art.Target = inBmp;
            }
        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            outBmp.Save(saveFileDialog.FileName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inBmp = new Bitmap(Constants.CANVAS_WIDTH, Constants.CANVAS_HEIGHT);
            outBmp = new Bitmap(Constants.CANVAS_WIDTH, Constants.CANVAS_HEIGHT);
            Population = new List<TriangleArt>();
            for(int i = 0; i < Constants.POPULATION_SIZE; i++)
            {
                Population.Add(new TriangleArt(Constants.TRIANGLE_MAX, inBmp));
                Population.Last().UpdError();
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < Iterations.Value; i++)
            {
                foreach (TriangleArt art in Population)
                {
                    art.MutateArt(new Random());
                    art.UpdError();
                }
                Population.Sort((a, b) => a.Error.CompareTo(b.Error));
                for(int j = 1; j < Population.Count; j++)
                {
                    Population[0].CopyTo(Population[j]);
                }
            }
            outBmp = Population[0].Draw();
            Out.Image = outBmp;
            Error.Text = Population[0].Error.ToString();
        }
    }
}
