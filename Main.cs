using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace RL_smdp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            m_gridWorld.RunFinished += new RunFinishedDelegate(m_gridWorld_RunFinished);
            
            //Microsoft.Glee.GraphViewerGdi.GViewer gw = new Microsoft.Glee.GraphViewerGdi.GViewer();
            //gw.Size = new Size(500, 500);
            //gw.BackColor = Color.Blue;

            //this.Controls.Add(gw);
            //gw.Graph = new Microsoft.Glee.Drawing.Graph("Test");
            //gw.Graph.AddNode("1");
            //gw.Graph.AddNode("2");
            //gw.Graph.AddNode("3");
            //gw.Graph.AddNode("4");
            //gw.Graph.AddEdge("1", "2");
            //gw.Graph.AddEdge("1", "3");
            //gw.Graph.AddEdge("4", "2");
            //gw.Graph.AddEdge("3", "1");
            ////e.ArrowHeadAtSource = true;
            //gw.Graph.GraphAttr.NodeAttr.Padding = 3;
            
        }

        void m_gridWorld_RunFinished()
        {
            m_gridWorld.MouseLocation = new Point(0, 0);
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    GLEE dlg = new GLEE(agent);
        //    dlg.Show();
        //}

        Agent agent;
        private void button2_Click(object sender, EventArgs e)
        {
            Context.id = 0;

            agent = new Agent(m_gridWorld.State, m_gridWorld);
            Agent.Emin = double.Parse(m_textEmin.Text);
            Agent.rho = double.Parse(m_textRho.Text);
            agent.DisableOptions = m_checkDisableOpt.Checked;
            if (m_txtstopepisode.Text != "0" && m_txtstopepisode.Text != "")
            {
                agent.stopByEpisode = true;
                agent.stopEpisode = int.Parse(m_txtstopepisode.Text);
            }
            m_gridWorld.Agent = agent;
            m_gridWorld.actCount = 0;
            
        }

        private void m_buttonLearn_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Learn));
            m_buttonLearn.Enabled = false;
            t.Start();
        }

        void Learn()
        {
            if (agent == null)
            {
                agent = new Agent(m_gridWorld.State, m_gridWorld);
                Agent.Emin = double.Parse(m_textEmin.Text);
                Agent.rho = double.Parse(m_textRho.Text);
                agent.DisableOptions = m_checkDisableOpt.Checked;
                if (m_txtstopepisode.Text != "0" && m_txtstopepisode.Text != "")
                {
                    agent.stopByEpisode = true;
                    agent.stopEpisode = int.Parse(m_txtstopepisode.Text);
                }
                m_gridWorld.Agent = agent;
            }
            agent.oneStepLearn = false;
            m_gridWorld.StartLearn(ReinforcementLearningAlgorithms.PrioritizedSweeping);
            //DrawModel();
            m_gridWorld.Run();
            //checkOptions();
            m_buttonLearn.Enabled = true;
        }

        private void m_buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Grid File(*.grd)|*.grd";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            if (File.Exists(dlg.FileName))
            {
                m_gridWorld.LoadData(dlg.FileName);
                //m_textSize.Text = m_gridWorld.GridSize.ToString();
            }
            else
                MessageBox.Show("File is not exists.");

        }

        smdp.Chart ch;
        private void button3_Click(object sender, EventArgs e)
        {
            if (agent == null)
            {
                MessageBox.Show("'Create Agent > Learn' before");
                return;
            }
            ch = new smdp.Chart(agent.EpisodeSteps, agent.ContextTrack);
            ch.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GraphDrawing gd = new GraphDrawing(agent.Model);
            gd.Show();
        }

        private void m_graphicModel_MouseEnter(object sender, EventArgs e)
        {
            DrawModel();
        }

        private void m_graphicModel_MouseMove(object sender, MouseEventArgs e)
        {
            m_gridWorld.GridWorld_MouseMove(sender, e);
            DrawModel();
        }

        private void DrawModel()
        {
            Size ImageSize = m_gridWorld.Size;
            if (ImageSize.Width <= 0 || ImageSize.Height <= 0)
                return;
            Bitmap bmp = new Bitmap(ImageSize.Width, ImageSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            

            if (agent != null)
            {
                int temp = 0;
                for (int i = 0; i < m_gridWorld.GridSize; i++)
                {
                    for (int j = 0; j < m_gridWorld.GridSize; j++)
                    {
                        RectangleF r = m_gridWorld.LocationToRectange(new Point(i, j), ImageSize);
                        float hW = r.Width / 2f;
                        float hH = r.Height / 2f;
                        Bitmap bmpT = new Bitmap((int)r.Width,(int)r.Height);
                        Graphics gT = Graphics.FromImage(bmpT);
                        //if (maxHist > 0)
                        //    gT.Clear(Color.FromArgb(128,(int)(255 - (128 * agent.firstVisitHist[i, j] / maxHist)),
                        //                            (int)(255 - (128 * agent.firstVisitHist[i, j] / maxHist)),
                        //                            (int)(255 - (128 * agent.firstVisitHist[i, j] / maxHist))));
                        gT.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        

                        Pen pen = Pens.Gray;
                        if (j * m_gridWorld.GridSize + i >= agent.Model.States.Count) continue;
                        int N = j * m_gridWorld.GridSize + i;
                        ModelState modelvalues = null;
                        try
                        {
                            modelvalues = agent.Model.States[N];
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Exception at Main.cs DrawModel()");
                        }


                        if(modelvalues == null || modelvalues.Count <= 0) continue;
                        Color c = Color.LightBlue;
                        if (agent.subgoalValues != null)
                        {
                            //for (int t = 0; t < agent.Model.Values[N].Count; t++)
                            //{
                            //    if (agent.Model[N, t] != null && agent.Model[N, t].sP != N && agent.subgoalValues[agent.Model[N, t].sP] <= agent.subgoalValues[N])
                            //    {
                            //        c = Color.White;
                            //        break;
                            //    }
                            //}
                            if (!agent.isSubgoal(N))
                                c = Color.White;
                            gT.Clear(c);


                            gT.DrawString(agent.subgoalValues[N].ToString("0.000"), this.Font, Brushes.Red, 0, 0);
                            //gT.DrawString(agent.subgoalValues2[N].ToString("0.000"), this.Font, Brushes.OrangeRed, 0, 10);
                            //if (agent.initiationSetMembers != null)
                            //    gT.DrawString(agent.initiationSetMembers[N].ToString(), this.Font, Brushes.Green, 0, 15);
                        }
                        if (modelvalues.Count > 0 && modelvalues[0] != null && modelvalues[0].s != modelvalues[0].sP)
                        {
                            gT.DrawLine(pen, hW, hH,hW, 3);
                            gT.DrawLine(pen, hW, 3, hW + 2, 5);
                            gT.DrawLine(pen, hW, 3, hW - 2, 5);
                            temp++;
                        }
                        if (modelvalues.Count > 1 && modelvalues[1] != null && modelvalues[1].s != modelvalues[1].sP)
                        {
                            gT.DrawLine(pen, hW, hH, hW, (r.Height) - 3);
                            gT.DrawLine(pen, hW, (r.Height) - 3f, (hW) + 2, (r.Height) - 5f);
                            gT.DrawLine(pen, hW, (r.Height) - 3f, (hW) - 2, (r.Height) - 5f);
                            temp++;
                        }
                        if (modelvalues.Count > 2 && modelvalues[2] != null && modelvalues[2].s != modelvalues[2].sP)
                        {
                            gT.DrawLine(pen, hW, hH, 3, hH);
                            gT.DrawLine(pen, 3, hH, 5, (hH) + 2);
                            gT.DrawLine(pen, 3, hH, 5, (hH) - 2);
                            temp++;
                        }
                        if (modelvalues.Count > 3 && modelvalues[3] != null && modelvalues[3].s != modelvalues[3].sP)
                        {
                            gT.DrawLine(pen, hW, hH, r.Width - 3, hH);
                            gT.DrawLine(pen, r.Width - 3, hH,r.Width - 5, (hH) + 2);
                            gT.DrawLine(pen, r.Width - 3, hH,r.Width - 5, (hH) - 2);
                            temp++;
                        }

                        gT.DrawRectangle(Pens.LightGray, 0, 0, r.Width, r.Height);
                        g.DrawImage(bmpT, r.Location);
                    }
                }
            }

            g.Dispose();
            m_graphicModel.Image = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(agent == null)
                agent = new Agent(m_gridWorld.State, m_gridWorld);
            agent.oneStepLearn = true;
            agent.LearnPS(20, 500);
            agent.oneStepLearn = false;
        }

        bool m_addblock = false;
        bool m_setagent = false;
        bool m_setgoal = false;
        private void m_buttonAddBlock_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = -1;
            if (m_addblock)
            {
                m_addblock = false;
                m_buttonAddBlock.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                m_addblock = true;
                m_buttonAddBlock.BackColor = Color.Orange;
            }
        }

        private void m_gridWorld_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_addblock)
            {
                if (m_gridWorld.isBlock(m_gridWorld.PixelToLocation(e.Location)))
                    m_gridWorld.RemoveBlock(e.Location);
                else
                    m_gridWorld.AddBlock(e.Location);
            }

            if (m_setgoal)
                m_gridWorld.CheeseLocation = m_gridWorld.PixelToLocation(e.Location);

            if (m_setagent)
                m_gridWorld.MouseLocation = m_gridWorld.PixelToLocation(e.Location);

            if (activeFlowButton >= 0)
            {
                Point p = m_gridWorld.PixelToLocation(e.Location);
                m_gridWorld.setFlow(p.X, p.Y, activeFlowButton);
            }
        }

        int agentLocation;
        Thread m_thread;
        private void m_buttonRun_Click(object sender, EventArgs e)
        {
            if (m_thread == null)
            {
                agentLocation = agent.currentState;
                m_buttonRun.Text = "Pause";
                m_thread = new Thread(new ThreadStart(m_gridWorld.RunResult));
                m_thread.Start();
            }
            else
            {
                m_buttonRun.Text = "Run";
                agent.currentState = agentLocation;
                m_thread.Abort();
                m_thread = null;
            }
        }

        private void m_checkDisableOpt_CheckedChanged(object sender, EventArgs e)
        {
            if (agent != null)
                agent.DisableOptions = m_checkDisableOpt.Checked;
        }

        private void m_gridWorld_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = m_gridWorld.PixelToLocation(e.Location);
            int t = p.Y * m_gridWorld.GridSize + p.X;
            m_labelLoc.Text = "Location: " + t.ToString();
            if (mouseDown && m_addblock)
            {
                if (m_removeBlock)
                    m_gridWorld.RemoveBlock(e.Location);
                else
                    m_gridWorld.AddBlock(e.Location);
            }
            if (mouseDown && activeFlowButton >= 0)
            {
                m_gridWorld.setFlow(p.X, p.Y, activeFlowButton);
            }
        }

        private void m_buttonAddGrid_Click(object sender, EventArgs e)
        {
            gridList1.Add(m_gridWorld.GridObject);
        }

        private void gridList1_SelectionMade(int index)
        {
            m_gridWorld.GridObject = gridList1.Get(index);
            gridList1.SelectedIndex = index;
            m_gridWorld.Pointer = new Point(0, 1);
            m_gridWorld.Pointer = new Point(0, 0);
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            if (m_gridWorld.Agent == null)
                return;
            for (int i = 0; i < m_gridWorld.Agent.contexts.Count; i++)
                comboBox1.Items.Add(m_gridWorld.Agent.contexts[i]);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_gridWorld.Agent.SwitchContext((Context)comboBox1.SelectedItem);
        }

        private void m_buttonActSwitch_Click(object sender, EventArgs e)
        {
            m_gridWorld.SwitchActions();
        }

        int activeFlowButton;

        private void m_buttonUp_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = 1;
            m_buttonUp.BackColor = Color.Orange;
        }

        private void m_buttonDown_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = 2;
            m_buttonDown.BackColor = Color.Orange;
        }

        private void m_buttonLeft_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = 3;
            m_buttonLeft.BackColor = Color.Orange;
        }

        private void m_buttonRight_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = 4;
            m_buttonRight.BackColor = Color.Orange;
        }

        private void m_buttonNone_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = 0;
            m_buttonNone.BackColor = Color.Orange;
        }

        public void DisableAllFlowButtons()
        {
            m_buttonInv.BackColor = this.BackColor;
            m_buttonUp.BackColor = this.BackColor;
            m_buttonDown.BackColor = this.BackColor;
            m_buttonLeft.BackColor = this.BackColor;
            m_buttonRight.BackColor = this.BackColor;
            m_buttonL.BackColor = this.BackColor;
            m_buttonR.BackColor = this.BackColor;
            m_buttonNone.BackColor = this.BackColor;
            m_addblock = false;
            m_buttonAddBlock.BackColor = this.BackColor;
            m_setagent = false;
            m_buttonSetAgentLoc.BackColor = this.BackColor;
            m_setgoal = false;
            m_buttonSetGoalLoc.BackColor = this.BackColor;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = -1;
        }

        private void m_buttonSetAgentLoc_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = -1;
            if (m_setagent)
            {
                m_setagent = false;
                m_buttonSetAgentLoc.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                m_setagent = true;
                m_buttonSetAgentLoc.BackColor = Color.Orange;
            }
        }

        private void m_buttonSetGoalLoc_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = -1;
            if (m_setgoal)
            {
                m_setgoal = false;
                m_buttonSetGoalLoc.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                m_setgoal = true;
                m_buttonSetGoalLoc.BackColor = Color.Orange;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OptionDetails od = new OptionDetails(agent.currentContext);
            od.Show();
        }

        private void m_checkDetectCS_CheckedChanged(object sender, EventArgs e)
        {
            m_gridWorld.checkSwitchContext = m_checkDetectCS.Checked;
        }

        private void m_buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Grid File(*.grd)|*.grd";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            
            m_gridWorld.SaveData(dlg.FileName);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            m_gridWorld.GridSize = int.Parse(m_textMapsSize.Text);
        }

        bool mouseDown = false;
        bool m_removeBlock = false;
        private void m_gridWorld_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            if (m_gridWorld.isBlock(m_gridWorld.PixelToLocation(e.Location)))
                m_removeBlock = true;
            else
                m_removeBlock = false;
        }

        private void m_gridWorld_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void gridList1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Grid List File(*.grdl)|*.grdl";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            FileStream fs = new FileStream(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.WriteByte((byte)gridList1.Count);
            for (int i = 0; i < gridList1.Count; i++)
                gridList1.Get(i).SaveToStream(fs);
            fs.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Grid List File(*.grdl)|*.grdl";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            for (; gridList1.Count > 0; )
                gridList1.Remove(0);
            FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
            int count = fs.ReadByte();
            for (int i = 0; i < count; i++)
            {
                gridList1.Add(GridObj.LoadFromStream(fs));
            }
            if (count > 0)
                gridList1_SelectionMade(0);
            fs.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = 5;
            m_buttonInv.BackColor = Color.Orange;
        }

        private void m_buttonStop_Click(object sender, EventArgs e)
        {
            agent.forceStop = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            new EGraph(agent).Show();
        }

        private void m_gridWorld_StepPassed()
        {
            string s = "";
            for (int i = 0; i < agent.contexts.Count; i++)
            {
                s += agent.contexts[i].Em + "\n\r";
            }
            m_labelInfo.Text = s;
        }

        private void m_buttonL_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = 6;
            m_buttonL.BackColor = Color.Orange;
        }

        private void m_buttonR_Click(object sender, EventArgs e)
        {
            DisableAllFlowButtons();
            activeFlowButton = 7;
            m_buttonR.BackColor = Color.Orange;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int si = agent.currentContext.Model.StateCount;
            int[,] matrix = new int[si, si];
            for (int i = 0; i < si; i++)
            {
                ModelState s = agent.currentContext.Model[i];
                if (s == null)
                    continue;
                for (int j = 0; j < s.Count; j++)
                {
                    if (s[j] != null && s[j].sP != s.State)
                    {
                        matrix[i, s[j].sP] = 1;
                    }
                }
            }

            StreamWriter writer = new StreamWriter("model.txt");
            writer.Write("");
            for (int i = 0; i < si; i++)
            {
                writer.Write("\t" + "" + i.ToString());
            }
            writer.WriteLine();

            for (int i = 0; i < si; i++)
            {
                writer.Write("" + i.ToString());
                for (int j = 0; j < si; j++)
                {
                    writer.Write("\t" + matrix[i, j].ToString());
                }
                writer.WriteLine();
            }
            writer.Close();

            writer = new StreamWriter("model_edges.txt");
            writer.WriteLine("Target\tSource\n");

            for (int i = 0; i < si; i++)
            {
                ModelState s = agent.currentContext.Model[i];
                if (s == null)
                    continue;
                for (int j = 0; j < s.Count; j++)
                {
                    if (s[j] != null && s[j].sP != s.State)
                    {
                        writer.WriteLine(i + "\t" + s[j].sP);
                    }
                }
            }
            writer.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < m_gridWorld.GridObject.GetLength(0); i++)
                for (int j = 0; j < m_gridWorld.GridObject.GetLength(1); j++)
                    if (!m_gridWorld.isBlock(new Point(i, j)))
                        m_gridWorld.setFlow(i, j, 5);
        }

        private void m_buttonBatch_Click(object sender, EventArgs e)
        {
            batchForm dlg = new batchForm();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                batch_trialCount = dlg.TrailCount;
                batch_filename = dlg.FileName;
                Thread t = new Thread(new ThreadStart(batchLearn));
                t.Start();
            }
        }

        int batch_trialCount = 0;
        string batch_filename = "";
        void batchLearn()
        {
            m_buttonLearn.Enabled = false;
            FileStream wr = new FileStream(batch_filename + ".txt", FileMode.Create);
            FileStream wr2 = new FileStream(batch_filename + "_sg.txt", FileMode.Create);
            FileStream wr3 = new FileStream(batch_filename + "_opt.txt", FileMode.Create);
            FileStream wr4 = new FileStream(batch_filename + "_time.txt", FileMode.Create);
            for (int i = 0; i < batch_trialCount; i++)
            {
                ////////////RESET///////////////
                Context.id = 0;
                this.Text = i.ToString();
                agent = new Agent(m_gridWorld.State, m_gridWorld);
                Agent.Emin = double.Parse(m_textEmin.Text);
                Agent.rho = double.Parse(m_textRho.Text);
                agent.DisableOptions = m_checkDisableOpt.Checked;
                if (m_txtstopepisode.Text != "0" && m_txtstopepisode.Text != "")
                {
                    agent.stopByEpisode = true;
                    agent.stopEpisode = int.Parse(m_txtstopepisode.Text);
                }
                m_gridWorld.Agent = agent;
                m_gridWorld.actCount = 0;
                ////////////////////////////////////////

                ////////////SET//SEED///////////////////
                agent.rand = new Random(i);
                ////////////////////////////////////////


                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                ////////////LEARN////////////////////////
                agent.oneStepLearn = false;
                m_gridWorld.StartLearn(ReinforcementLearningAlgorithms.PrioritizedSweeping);
                ////////////////////////////////////////
                sw.Stop();
                wr4.Write(BitConverter.GetBytes(sw.ElapsedMilliseconds), 0, sizeof(long));
                wr4.Flush();


                /////////SAVE///RESULTS/////////////////
                wr.Write(BitConverter.GetBytes(agent.EpisodeSteps.Count), 0, 4);
                for (int j = 0; j < agent.EpisodeSteps.Count; j++)
                {
                    wr.Write(BitConverter.GetBytes(agent.EpisodeSteps[j]), 0, 4);
                }
                //wr.WriteLine();
                wr.Flush();
                ////////////////////////////////////////

                /////////SAVE///SUB/GOAL/VARS/////////////////
                wr2.Write(BitConverter.GetBytes(agent.subgoalVar.Count), 0, sizeof(int));
                for (int j = 0; j < agent.subgoalVar.Count; j++)
                {
                    wr2.Write(BitConverter.GetBytes(agent.subgoalVar[j]), 0, sizeof(double));
                }
                //wr.WriteLine();
                wr2.Flush();
                ////////////////////////////////////////

                /////////SAVE///OPTION///LEARN///STATUS OF VALUES/////////////////
                wr3.Write(BitConverter.GetBytes(agent.optLearnedVals), 0, sizeof(int));
                wr3.Write(BitConverter.GetBytes(agent.optNonLearnedVals), 0, sizeof(int));
                wr3.Write(BitConverter.GetBytes(m_gridWorld.GridObject.Size), 0, sizeof(int));
                //wr.WriteLine();
                wr3.Flush();
                ////////////////////////////////////////
            }
            wr.Close();
            wr2.Close();
            wr3.Close();
            wr4.Close();
            m_buttonLearn.Enabled = true;
        }

        private void m_gridWorld_Load(object sender, EventArgs e)
        {

        }
    }
}
