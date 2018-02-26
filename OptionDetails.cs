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
    public partial class OptionDetails : Form
    {
        Context context;
        public OptionDetails(Context cx)
        {
            context = cx;
            InitializeComponent();
            for (int i = 0; i < cx.options.Count; i++)
            {
                m_comboOptions.Items.Add(cx.options[i]);
            }
        }

        int offsetX = 0, offsetY = 0, oldoffsetX = 0, oldoffsetY = 0;
        private void m_comboOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptionN o = (OptionN)m_comboOptions.SelectedItem;
            if (o == null)
                return;
            List<int> used = new List<int>();
            Bitmap bmp = new Bitmap(m_graph.Width * 2, m_graph.Height * 2);
            Graphics g = Graphics.FromImage(bmp);
            draw(o, used, o.SubgoalState, g, new PointF(m_graph.Width + offsetX, m_graph.Height + offsetY));
            g.Dispose();
            m_graph.BackgroundImageLayout = ImageLayout.Stretch;
            m_graph.BackgroundImage = bmp;
        }

        float size = 1;
        void draw(OptionN o,List<int> used, int state, Graphics g, PointF pos)
        {
            Color colArrow = Color.Black, colNode = Color.LightGray, colValue = Color.Black, colState = Color.Black;
            float arrow = 5, node = 20, space = 60, hnode = 10;
            arrow *= size;
            node *= size;
            space *= size;
            hnode *= size;

            Font f = new Font(this.Font.FontFamily, this.Font.Size * size);
            if (!o.V.Keys.Contains(state))
                return;
            if (used.Contains(state))
                return;
            used.Add(state);
            g.FillEllipse(new SolidBrush(colNode), new RectangleF(pos, new SizeF(node, node)));
            g.DrawString(state.ToString(), f, new SolidBrush(colState), pos);
            g.DrawString(o.V[state].ToString("0.00"), f, new SolidBrush(colValue), new PointF(pos.X + node, pos.Y + node));

            foreach (int i in o.initSet[state].aSp.Keys)
            {
                PointF p = new PointF(0,0);
                if (i == 0)
                    p = new PointF(pos.X, pos.Y - space);
                else if (i == 1)
                    p = new PointF(pos.X, pos.Y + space);
                else if (i == 2)
                    p = new PointF(pos.X - space, pos.Y);
                else if (i == 3)
                    p = new PointF(pos.X + space, pos.Y);

                if (o.initSet[state].aSp[i] != null)
                {
                    PointF pA = new PointF();
                    PointF pB = new PointF();
                    Pen pn = new Pen(colArrow);
                    if (pos.X == p.X)
                    {
                        pA.X = pos.X + hnode;
                        pB.X = p.X + hnode;
                        if (pos.Y > p.Y)
                        {
                            pA.Y = pos.Y;
                            pB.Y = p.Y + node;
                            
                            g.DrawLine(pn, pB, new PointF(pB.X - arrow, pB.Y + arrow));
                            g.DrawLine(pn, pB, new PointF(pB.X + arrow, pB.Y + arrow));
                        }
                        else
                        {
                            pA.Y = pos.Y + node;
                            pB.Y = p.Y;

                            g.DrawLine(pn, pB, new PointF(pB.X - arrow, pB.Y - arrow));
                            g.DrawLine(pn, pB, new PointF(pB.X + arrow, pB.Y - arrow));
                        }
                    }
                    if (pos.Y == p.Y)
                    {
                        pA.Y = pos.Y + hnode;
                        pB.Y = p.Y + hnode;
                        if (p.X > pos.X)
                        {
                            pA.X = pos.X + node;
                            pB.X = p.X;

                            g.DrawLine(pn, pB, new PointF(pB.X - arrow, pB.Y - arrow));
                            g.DrawLine(pn, pB, new PointF(pB.X - arrow, pB.Y + arrow));
                        }
                        else
                        {
                            pA.X = pos.X;
                            pB.X = p.X + node;
                            g.DrawLine(pn, pB, new PointF(pB.X + arrow, pB.Y - arrow));
                            g.DrawLine(pn, pB, new PointF(pB.X + arrow, pB.Y + arrow));
                        }
                    }

                    g.DrawLine(Pens.Blue, pA, pB); 
                    draw(o, used, o.initSet[state].aSp[i], g, p);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Jpeg | *.jpg";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            m_graph.BackgroundImage.Save(dlg.FileName);
            
        }

        bool zoom = false, move = false;
        int startX, startY;
        float oldsize = 1f;
        private void m_graph_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                move = true;
                oldoffsetX = offsetX;
                oldoffsetY = offsetY;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                zoom = true;
                oldsize = size;
            }
            startX = e.X;
            startY = e.Y;
        }

        private void m_graph_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                offsetX = oldoffsetX + (e.X - startX);
                offsetY = oldoffsetY + (e.Y - startY);
                m_comboOptions_SelectedIndexChanged(null, null);
            }
            if (zoom)
            {
                float len = (e.Y - startY) / 100f;
                Text = len.ToString();
                size = oldsize * (float)Math.Pow(0.1, len);
                m_comboOptions_SelectedIndexChanged(null, null);
            }
        }

        private void m_graph_MouseUp(object sender, MouseEventArgs e)
        {
            zoom = move = false;
        }





    }
}
