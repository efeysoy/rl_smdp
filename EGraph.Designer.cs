namespace RL_smdp
{
    partial class EGraph
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_grph = new System.Windows.Forms.PictureBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.m_labelMax = new System.Windows.Forms.Label();
            this.m_labelMin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_grph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(824, 51);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // m_grph
            // 
            this.m_grph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grph.Location = new System.Drawing.Point(61, 5);
            this.m_grph.Name = "m_grph";
            this.m_grph.Size = new System.Drawing.Size(768, 258);
            this.m_grph.TabIndex = 0;
            this.m_grph.TabStop = false;
            this.m_grph.Click += new System.EventHandler(this.m_grph_Click);
            this.m_grph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Location = new System.Drawing.Point(61, 252);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(768, 17);
            this.hScrollBar1.TabIndex = 2;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(8, 272);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(821, 45);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // m_labelMax
            // 
            this.m_labelMax.Location = new System.Drawing.Point(5, 5);
            this.m_labelMax.Name = "m_labelMax";
            this.m_labelMax.Size = new System.Drawing.Size(55, 247);
            this.m_labelMax.TabIndex = 4;
            this.m_labelMax.Text = "0,000";
            // 
            // m_labelMin
            // 
            this.m_labelMin.Location = new System.Drawing.Point(8, 238);
            this.m_labelMin.Name = "m_labelMin";
            this.m_labelMin.Size = new System.Drawing.Size(52, 23);
            this.m_labelMin.TabIndex = 5;
            this.m_labelMin.Text = "-0,000";
            // 
            // EGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 364);
            this.Controls.Add(this.m_labelMin);
            this.Controls.Add(this.m_labelMax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.m_grph);
            this.Name = "EGraph";
            this.Text = "EGraph";
            this.Load += new System.EventHandler(this.EGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_grph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_grph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label m_labelMax;
        private System.Windows.Forms.Label m_labelMin;
    }
}