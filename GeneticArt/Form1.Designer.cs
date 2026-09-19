namespace GeneticArt
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Out = new PictureBox();
            In = new PictureBox();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            Iterations = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)Out).BeginInit();
            ((System.ComponentModel.ISupportInitialize)In).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Iterations).BeginInit();
            SuspendLayout();
            // 
            // Out
            // 
            Out.BorderStyle = BorderStyle.Fixed3D;
            Out.Location = new Point(675, 5);
            Out.Name = "Out";
            Out.Size = new Size(500, 500);
            Out.TabIndex = 0;
            Out.TabStop = false;
            Out.Click += Out_Click;
            // 
            // In
            // 
            In.BorderStyle = BorderStyle.Fixed3D;
            In.Location = new Point(12, 5);
            In.Name = "In";
            In.Size = new Size(500, 500);
            In.TabIndex = 1;
            In.TabStop = false;
            In.Click += In_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            openFileDialog.FileOk += openFileDialog_FileOk;
            // 
            // saveFileDialog
            // 
            saveFileDialog.FileOk += saveFileDialog_FileOk;
            // 
            // Iterations
            // 
            Iterations.BorderStyle = BorderStyle.FixedSingle;
            Iterations.Location = new Point(534, 236);
            Iterations.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            Iterations.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            Iterations.Name = "Iterations";
            Iterations.Size = new Size(120, 23);
            Iterations.TabIndex = 2;
            Iterations.Value = new decimal(new int[] { 10000, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 561);
            Controls.Add(Iterations);
            Controls.Add(In);
            Controls.Add(Out);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Out).EndInit();
            ((System.ComponentModel.ISupportInitialize)In).EndInit();
            ((System.ComponentModel.ISupportInitialize)Iterations).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Out;
        private PictureBox In;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private NumericUpDown Iterations;
    }
}
