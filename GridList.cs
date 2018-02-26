using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

    public delegate void Selection(int index);
    public partial class GridList : UserControl
    {
        List<GridObj> list = new List<GridObj>();
        public event Selection SelectionMade;
        Pen selectionPen = new Pen(Brushes.SkyBlue, 3.0f);

        int m_selectedIndex = -1;

        public int SelectedIndex
        {
            get { return m_selectedIndex; }
            set { m_selectedIndex = value; }
        }

        public int Count
        {
            get { return list.Count; }
        }

        public GridList()
        {
            InitializeComponent();
            hScrollBar.Maximum = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            Bitmap bmp = new Bitmap(this.Width, this.Height - hScrollBar.Height);
            Graphics g = Graphics.FromImage(bmp);

            int size = this.Height - hScrollBar.Height;

            for (int i = 0; i < list.Count; i++)
            {
                if (i * size > this.Width || i + hScrollBar.Value >= list.Count)
                    break;
                g.DrawLine(Pens.Black, (size + 1) * i - 1, 0, (size + 1) * i - 1, this.Height - hScrollBar.Height);
                g.DrawImage(list[i + hScrollBar.Value].DrawBmp(size, size), (size + 1) * i, 0);
                if (cursorActive && cursorLoc == i)
                {
                    g.DrawRectangle(selectionPen, (size + 1) * i, 0, size- 1, size - 1);
                }
            }
            g.Dispose();
            base.OnPaint(e);
            e.Graphics.DrawImage(bmp,0,0);
        }

        public void Add(GridObj o)
        {
            list.Add(o);
            hScrollBar.Maximum = list.Count;
            Invalidate();
        }

        public GridObj Get(int index)
        {
            return list[index];
        }

        public void Remove(int index)
        {
            list.RemoveAt(index);
            hScrollBar.Maximum = list.Count;
            Invalidate();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.Invalidate();
        }

        private void GridList_MouseClick(object sender, MouseEventArgs e)
        {
            int size = this.Height - hScrollBar.Height;
            int index = e.X / (size + 1);

            index += hScrollBar.Value;

            if (index < list.Count)
            {
                if (SelectionMade != null && e.Button == MouseButtons.Left)
                    SelectionMade(index);
                if (e.Button == MouseButtons.Right)
                {
                    if (MessageBox.Show("Do you really want to delete grid #" + index + "?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Remove(index);
                }
            }
        }


        bool cursorActive = false;
        int cursorLoc = 0;
        private void GridList_MouseMove(object sender, MouseEventArgs e)
        {
            int prevloc = cursorLoc;
            bool prevAct = cursorActive;
            cursorActive = false;
            int size = this.Height - hScrollBar.Height;
            int index = e.X / (size + 1);

            //index += hScrollBar.Value;

            if (index < list.Count)
            {
                cursorActive = true;
                cursorLoc = index;
                if(!prevAct || cursorLoc != prevloc)
                    Invalidate();
            }
        }

        private void GridList_Load(object sender, EventArgs e)
        {

        }
    }
