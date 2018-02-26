using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace smdp
{
    public partial class Chart : Form
    {
        Color[] chartColors = new Color[] { Color.FromArgb(127, 255, 0, 9), 
                                       Color.FromArgb(127, 0, 255, 0), 
                                       Color.FromArgb(127, 0, 0, 255),
                                       Color.FromArgb(127, 255, 0, 255),
                                       Color.FromArgb(127, 0, 255, 255),
                                       Color.FromArgb(127, 0, 0, 255)};

        public List<int> values;
        public List<int> valueColorIndexes;

        public Chart(List<int> vals, List<int> valColIndex)
        {
            values = vals;
            valueColorIndexes = valColIndex;
            InitializeComponent();
            zbpd = zbp++;
        }

        private void Chart_Load(object sender, EventArgs e)
        {

        }


        int m_graphicX = 0;
        private void m_graphic_MouseMove(object sender, MouseEventArgs e)
        {
            m_graphicX = e.X;
            m_graphic.Refresh();
        }

        private void m_graphic_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (values == null)
                    return;
                if (values.Count <= 0)
                    return;
                //int maxVal = int.Parse(m_textMaxVal.Text);
                //if (values.Count <= maxVal || !m_checkMaxVal.Checked)
                {
                    float rateX = (m_graphic.Width - 50) / (float)values.Count;
                    int max = values[0];
                    int total = 0;
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (values[i] > max)
                            max = values[i];
                        total += values[i];
                    }
                    float rateY = (m_graphic.Height - 60) / (float)max;
                    Bitmap bmp = new Bitmap(m_graphic.Width, m_graphic.Height);
                    Graphics g = Graphics.FromImage(bmp);//e.Graphics;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.White);
                    g.DrawString(max.ToString(), this.Font, Brushes.Black, 5, 5);
                    SizeF strSize = g.MeasureString(values.Count.ToString(), this.Font);
                    g.DrawString(values.Count.ToString(), this.Font, Brushes.Black, new PointF(m_graphic.Width - strSize.Width, m_graphic.Height - 45));

                    g.DrawString("Total Steps: " + total.ToString(), this.Font, Brushes.Black, new PointF(m_graphic.Width - 200, m_graphic.Height - 25));


                    g.DrawLine(Pens.Black, 0, m_graphic.Height - 50, m_graphic.Width, m_graphic.Height - 50);
                    g.DrawLine(Pens.Black, 50, 0, 50, m_graphic.Height);
                    PointF[] pts = new PointF[values.Count];

                    PointF[] pts2 = new PointF[values.Count];

                    //PointF[] pts3 = new PointF[values.Count + (values.Count - 1) * 3];

                    //float[] valuesH = new float[values.Count + (values.Count - 1) * 3];

                    //for (int i = 0; i < values.Count - 1; i++)
                    //{
                    //    valuesH[i * 4] = values[i];
                    //    valuesH[(i + 1) * 4] = values[i + 1];
                    //    valuesH[i * 4 + 2] = ((values[i] + values[i + 1]) / 2.0f);
                    //    valuesH[i * 4 + 1] = ((values[i] + valuesH[i * 4 + 2]) / 2.0f);
                    //    valuesH[i * 4 + 3] = ((valuesH[i * 4 + 1] + values[i + 1]) / 2.0f);
                    //}

                    //pts[0] = new PointF(50f, m_graphic.Height - 50f);
                    int val = 0, x = 0;
                    for (int i = 0; i < values.Count; i++)
                    {
                        val += values[i];
                        x = val / (i + 1);
                        pts[i] = new PointF((float)((i) * rateX) + 50, m_graphic.Height - 50f - (x * rateY));
                    }

                    //float val2 = 0, x2 = 0;
                    //for (int i = 0; i < valuesH.Length; i++)
                    //{
                    //    {
                    //        val2 = 0;
                    //        for (int j = 0; j <= i; j++)
                    //        {
                    //            val2 += (valuesH[j] * (float)Math.Pow(m_trackAcc.Value / 100.0, i - j));
                    //        }
                    //        x2 = val2 / ((i + 1) * 0.9f);
                    //        //x2 = valuesH[i];
                    //        pts3[i] = new PointF((i * rateX / 4) + 50, m_graphic.Height - 50f - (x2 * rateY));
                    //    }
                    //}

                    for (int i = 0; i < values.Count; i++)
                    {

                        pts2[i] = new PointF((float)((i) * rateX) + 50, m_graphic.Height - 50f - (values[i] * rateY));
                    }
                    int mouseVal = (int)(values.Count * (m_graphicX - 50) / (m_graphic.Width - 50));

                    if (mouseVal >= 0)
                    {

                        int subtotal = 0;
                        for (int z = 0; z <= mouseVal; z++)
                            subtotal += values[z];
                        Text = mouseVal + ". episode: " + values[mouseVal] + " step(s) - Total Steps: " + subtotal;
                        PointF pointer = new PointF((float)((mouseVal) * rateX) + 47, m_graphic.Height - 53f - (values[mouseVal] * rateY));
                        g.DrawEllipse(Pens.Blue, new RectangleF(pointer, new SizeF(6, 6)));
                    }
                    //pts[values.Count + 1] = new PointF(pts[values.Count].X, m_graphic.Height - 50);
                    //GraphicsPath path = new GraphicsPath();
                    //path.AddLines(pts);
                    //HatchBrush hBrush3 = new HatchBrush(HatchStyle.Weave, Color.Orange, Color.Red);
                    //g.FillPath(hBrush3, path);
                    Pen p = new Pen(Brushes.Gray, 3f);
                    if (pts.Length > 1)
                    {
                        //g.DrawLines(p, pts3);
                        //g.DrawLines(Pens.Blue, pts);

                        Pen pn = new Pen(Color.Black,2);
                        if (zbpd % 2 == 0)
                            pn.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        for (int i = 0; i < pts2.Length - 1; i++){
                            //Pen pn = new Pen(Color.Black,2);
                            //if (valueColorIndexes[i] == 1)
                            //    pn.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            g.DrawLine(pn, pts2[i], pts2[i + 1]);
                        }
                        //g.DrawLines(Pens.Red, pts2);
                    }
                    //for (int i = 1; i < values.Count; i++)
                    //{
                    //    g.DrawLine(Pens.Red, (float)((i - 1) * rateX) + 50,m_graphic.Height - 50f - (values[i - 1] * rateY), (float)(i * rateX) + 50, m_graphic.Height - 50f - (values[i] * rateY));
                    //}
                    m_graphic.BackgroundImage = bmp;
                }
                //else
                //{

                //    float rateX = (m_graphic.Width - 50) / (float)maxVal;
                //    int max = values[values.Count - maxVal];
                //    int total = 0;
                //    for (int i = values.Count - maxVal; i < values.Count; i++)
                //    {
                //        if (values[i] > max)
                //            max = values[i];
                //        total += values[i];
                //    }
                //    float rateY = (m_graphic.Height - 60) / (float)max;
                //    Bitmap bmp = new Bitmap(m_graphic.Width, m_graphic.Height);
                //    Graphics g = e.Graphics;//Graphics.FromImage(bmp);
                //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //    g.Clear(Color.White);
                //    g.DrawString(max.ToString(), this.Font, Brushes.Black, 5, 5);
                //    SizeF strSize = g.MeasureString(maxVal.ToString(), this.Font);
                //    g.DrawString(maxVal.ToString(), this.Font, Brushes.Black, new PointF(m_graphic.Width - strSize.Width, m_graphic.Height - 45));

                //    g.DrawString("Total Steps: " + total.ToString(), this.Font, Brushes.Black, new PointF(m_graphic.Width - maxVal, m_graphic.Height - 25));


                //    g.DrawLine(Pens.Black, 0, m_graphic.Height - 50, m_graphic.Width, m_graphic.Height - 50);
                //    g.DrawLine(Pens.Black, 50, 0, 50, m_graphic.Height);
                //    PointF[] pts = new PointF[maxVal];

                //    PointF[] pts2 = new PointF[maxVal];

                //    //pts[0] = new PointF(50f, m_graphic.Height - 50f);
                //    int val = 0, x = 0;
                //    int j = 0;
                //    for (int i = values.Count - maxVal; i < values.Count; i++)
                //    {
                //        val += values[i];
                //        x = val / (j + 1);
                //        pts[j] = new PointF((float)((j) * rateX) + 50, m_graphic.Height - 50f - (x * rateY));
                //        j++;
                //    }

                //        j = 0;
                //        for (int i = values.Count - maxVal; i < values.Count; i++)
                //        {
                //            pts2[j] = new PointF((float)((j) * rateX) + 50, m_graphic.Height - 50f - (values[i] * rateY));
                //            j++;
                //        }
                //        int mouseVal = (int)(maxVal * (m_graphicX - 50) / (m_graphic.Width - 50));

                //        if (mouseVal >= 0)
                //        {

                //            int subtotal = 0;
                //            for (int z = 0; z <= mouseVal; z++)
                //                subtotal += values[values.Count - maxVal + z];
                //            Text = mouseVal + ". episode: " + values[values.Count - maxVal + mouseVal] + " step(s) - Total Steps: " + subtotal;
                //            PointF pointer = new PointF((float)((mouseVal) * rateX) + 47, m_graphic.Height - 53f - (values[values.Count - maxVal + mouseVal] * rateY));
                //            g.DrawEllipse(Pens.Blue, new RectangleF(pointer, new SizeF(6, 6)));
                //        }
                //        //pts[values.Count + 1] = new PointF(pts[values.Count].X, m_graphic.Height - 50);
                //        //GraphicsPath path = new GraphicsPath();
                //        //path.AddLines(pts);
                //        //HatchBrush hBrush3 = new HatchBrush(HatchStyle.Weave, Color.Orange, Color.Red);
                //        //g.FillPath(hBrush3, path);
                //        if (pts.Length > 1)
                //        {
                //            g.DrawLines(Pens.Red, pts);
                //            g.DrawLines(Pens.Green, pts2);
                //        }
                //        //for (int i = 1; i < values.Count; i++)
                //        //{
                //        //    g.DrawLine(Pens.Red, (float)((i - 1) * rateX) + 50,m_graphic.Height - 50f - (values[i - 1] * rateY), (float)(i * rateX) + 50, m_graphic.Height - 50f - (values[i] * rateY));
                //        //}
                //        //m_graphic.BackgroundImage = bmp;
                //    }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Exception at chart m_graphic_Paint()");
            }
        }
        static int zbp = 0;
        int zbpd;

        private void m_graphic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                PictureBox pbx = (PictureBox)sender;
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Jpeg | *.jpg";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_graphic.BackgroundImage.Save(dlg.FileName);
                }
            }
        }

        private void m_buttonSave_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("chart.txt");
            writer.Write("");
            for (int i = 0; i < values.Count; i++)
            {
                writer.WriteLine(values[i].ToString());
            }

            writer.Close();
        }
    }
}
