using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RL_smdp
{
    public partial class batchForm : Form
    {
        public batchForm()
        {
            InitializeComponent();
        }

        private void m_buttonStart_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public int TrailCount
        {
            get
            {
                return int.Parse(m_textTrials.Text);
            }
        }

        public string FileName
        {
            get
            {
                return m_textFileName.Text;
            }
        }

        private void m_buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
