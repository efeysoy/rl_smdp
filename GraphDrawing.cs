using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RL_smdp
{
    public partial class GraphDrawing : Form
    {
        ModelClass m_model;
        Diagram di;
        public GraphDrawing(ModelClass model)
        {
            InitializeComponent();
            m_model = model;
            di = new Diagram();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (di.isArranging)
                e.Graphics.DrawString("Calculating...", this.Font, Brushes.Red, 0, 0);
            else
            {
                Graphics g = this.CreateGraphics();
                g.Clear(Color.White);
                di.Draw(g, new Rectangle(12, 12, ClientSize.Width, ClientSize.Height));///Rectangle.FromLTRB(12, 12, ClientSize.Width - 12, ClientSize.Height - 12));
            }
        }

        private void Draw()
        {
            di.Arrange();
            Invalidate();
        }

        private void GraphDrawing_Load(object sender, EventArgs e)
        {
            List<Node> list = new List<Node>();
            for (int i = 0; i < m_model.States.Count; i++)
            {
                if (m_model.States[i] == null)
                {
                    list.Add(null);
                    continue;
                }
                Node n = new SpotNode(Color.Blue);
                list.Add(n);
                n.ID = i;
            }

            for (int i = 0; i < m_model.States.Count; i++)
            {
                if (m_model.States[i] == null) continue;
                for (int j = 0; j < m_model.States[i].Count; j++)
                {
                    if(m_model.States[i][j] == null) continue;
                    list[i].AddChild(list[m_model[i, j].sP]);
                }
            }
            di.AddNode(list[0]);

            Thread bg = new Thread(Draw);
            bg.IsBackground = true;
            bg.Start();
            Thread.Sleep(20);
        }

        private void GraphDrawing_Shown(object sender, EventArgs e)
        {


        }

    }
}
