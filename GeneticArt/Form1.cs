namespace GeneticArt
{
    public partial class Form1 : Form
    {
        Bitmap inBmp;
        Bitmap outBmp;
        Graphics gfx;
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
        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            outBmp.Save(saveFileDialog.FileName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inBmp = new Bitmap(Constants.CANVAS_WIDTH, Constants.CANVAS_HEIGHT);
            outBmp = new Bitmap(Constants.CANVAS_WIDTH, Constants.CANVAS_HEIGHT);
            gfx = Graphics.FromImage(outBmp);
        }

        private void Start_Click(object sender, EventArgs e)
        {

        }
    }
}
