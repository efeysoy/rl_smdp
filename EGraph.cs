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
    public partial class EGraph : Form
    {
        Agent m_agent;
        Color[] chartColors = new Color[] { Color.FromArgb(127, 255, 0, 9), 
                                       Color.FromArgb(127, 0, 255, 0), 
                                       Color.FromArgb(127, 0, 0, 255),
                                       Color.FromArgb(127, 255, 0, 255),
                                       Color.FromArgb(127, 0, 255, 255),
                                       Color.FromArgb(127, 0, 0, 255)};
        public EGraph(Agent agent)
        {
            InitializeComponent();
            m_agent = agent;
        }

        private void EGraph_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        public void draw()
        {
           
            float size = 1;
            if (trackBar1.Value != 0)
                size = 1f / trackBar1.Value;
            float min = 0;
            float max = 0;
            int maxLen = 0;
            int realMaxLen;
            for (int i = 0; i < m_agent.contexts.Count; i++)
            {
                if (m_agent.contexts[i].Em_l.Count > maxLen)
                    maxLen = m_agent.contexts[i].Em_l.Count;
                for (int j = 0; j < m_agent.contexts[i].Em_l.Count; j++)
                {
                    if (m_agent.contexts[i].Em_l[j] < min)
                        min = (float)m_agent.contexts[i].Em_l[j];
                    if (m_agent.contexts[i].Em_l[j] > max)
                        max = (float)m_agent.contexts[i].Em_l[j];
                }
            }
            realMaxLen = maxLen;
            m_labelMax.Text = max.ToString("0.000");
            m_labelMin.Text = min.ToString("0.000");

            maxLen = (int)(maxLen * size);
            hScrollBar1.Maximum = realMaxLen - maxLen;
            int scroll = hScrollBar1.Value;

            float diff = (float)(max - min);
            float rate = m_grph.Height / diff;
            float wrate = (float)m_grph.Width / maxLen;
            Graphics g = m_grph.CreateGraphics();
            g.Clear(Color.White);
            g.DrawLine(Pens.Black, 0, max * rate, m_grph.Width, max * rate);
            PointF[][] points = new PointF[m_agent.contexts.Count][];
            for (int c = 0; c < m_agent.contexts.Count; c++)
            {
                points[c] = new PointF[maxLen];
                int offset = realMaxLen - m_agent.contexts[c].Em_l.Count;
                for (int i = 0; i < maxLen; i++)
                {
                    if (i < offset - scroll)
                    {
                        points[c][i] = new PointF(0,max * rate);
                        continue;
                    }
                    points[c][i] = new PointF((float)(wrate * i),
                        (float)(max * rate - m_agent.contexts[c].Em_l[i - offset + scroll] * rate));
                }
            }

            for (int i = 0; i < m_agent.contexts.Count; i++)
            {
                g.DrawLines(new Pen(chartColors[i]), points[i]);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            hScrollBar1.Value = 0;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            draw();
        }

        private void m_grph_Click(object sender, EventArgs e)
        {
            draw();
        }

    }
}
