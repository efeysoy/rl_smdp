namespace smdp
{
    partial class Chart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_graphic = new System.Windows.Forms.PictureBox();
            this.m_buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_graphic)).BeginInit();
            this.SuspendLayout();
            // 
            // m_graphic
            // 
            this.m_graphic.BackColor = System.Drawing.Color.White;
            this.m_graphic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_graphic.Location = new System.Drawing.Point(0, 0);
            this.m_graphic.Name = "m_graphic";
            this.m_graphic.Size = new System.Drawing.Size(742, 262);
            this.m_graphic.TabIndex = 0;
            this.m_graphic.TabStop = false;
            this.m_graphic.Paint += new System.Windows.Forms.PaintEventHandler(this.m_graphic_Paint);
            this.m_graphic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_graphic_MouseDown);
            this.m_graphic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_graphic_MouseMove);
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Location = new System.Drawing.Point(667, 0);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(75, 23);
            this.m_buttonSave.TabIndex = 1;
            this.m_buttonSave.Text = "Save";
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 262);
            this.Controls.Add(this.m_buttonSave);
            this.Controls.Add(this.m_graphic);
            this.Name = "Chart";
            this.Text = "Chart (Steps x Episodes)";
            this.Load += new System.EventHandler(this.Chart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_graphic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_graphic;
        private System.Windows.Forms.Button m_buttonSave;

    }
}