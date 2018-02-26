namespace RL_smdp
{
    partial class OptionDetails
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
            this.m_graph = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_comboOptions = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_graph)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_graph
            // 
            this.m_graph.BackColor = System.Drawing.Color.White;
            this.m_graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_graph.Location = new System.Drawing.Point(0, 46);
            this.m_graph.Name = "m_graph";
            this.m_graph.Size = new System.Drawing.Size(490, 258);
            this.m_graph.TabIndex = 0;
            this.m_graph.TabStop = false;
            this.m_graph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_graph_MouseDown);
            this.m_graph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_graph_MouseMove);
            this.m_graph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_graph_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_comboOptions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 46);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(323, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Option";
            // 
            // m_comboOptions
            // 
            this.m_comboOptions.FormattingEnabled = true;
            this.m_comboOptions.Location = new System.Drawing.Point(53, 12);
            this.m_comboOptions.Name = "m_comboOptions";
            this.m_comboOptions.Size = new System.Drawing.Size(264, 21);
            this.m_comboOptions.TabIndex = 0;
            this.m_comboOptions.SelectedIndexChanged += new System.EventHandler(this.m_comboOptions_SelectedIndexChanged);
            // 
            // OptionDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 304);
            this.Controls.Add(this.m_graph);
            this.Controls.Add(this.panel1);
            this.Name = "OptionDetails";
            this.Text = "OptionDetails";
            ((System.ComponentModel.ISupportInitialize)(this.m_graph)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_graph;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_comboOptions;
        private System.Windows.Forms.Button button1;
    }
}