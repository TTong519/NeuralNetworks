namespace LineOfBestFit
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
            components = new System.ComponentModel.Container();
            Display = new PictureBox();
            DrawButton = new Button();
            Timer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)Display).BeginInit();
            SuspendLayout();
            // 
            // Display
            // 
            Display.BorderStyle = BorderStyle.Fixed3D;
            Display.Location = new Point(6, 2);
            Display.Name = "Display";
            Display.Size = new Size(782, 415);
            Display.TabIndex = 0;
            Display.TabStop = false;
            Display.Click += Display_Click;
            // 
            // DrawButton
            // 
            DrawButton.FlatStyle = FlatStyle.Popup;
            DrawButton.Location = new Point(362, 423);
            DrawButton.Name = "DrawButton";
            DrawButton.Size = new Size(75, 23);
            DrawButton.TabIndex = 1;
            DrawButton.Text = "Draw";
            DrawButton.UseVisualStyleBackColor = true;
            DrawButton.Click += DrawButton_Click;
            // 
            // Timer
            // 
            Timer.Enabled = true;
            Timer.Interval = 10;
            Timer.Tick += Timer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DrawButton);
            Controls.Add(Display);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Display).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Display;
        private Button DrawButton;
        private System.Windows.Forms.Timer Timer;
    }
}
