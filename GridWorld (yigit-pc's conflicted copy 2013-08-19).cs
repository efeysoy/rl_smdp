using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

public delegate void RunFinishedDelegate();
public partial class GridWorld : UserControl, Environment
{
    GridObj data;
    public event RunFinishedDelegate RunFinished;
    private bool m_showArrows = false;
    public bool m_simulate = false;
    private Point m_mouseLocation = new Point(0,0);
    private int[] actionDirections = new int[] { 0, 1, 2, 3 };
    private int[] actionDirections2 = new int[] { 1, 0, 3, 2 };

    public Agent Agent = null;
    public GridWorld()
    {
        data = new GridObj(10, new Point(9,9), MouseLocation);
        InitializeComponent();
    }

    public bool ShowArrows
    {
        get { return m_showArrows; }
        set { m_showArrows = value; }
    }

    public bool Simulate
    {
        get { return m_simulate; }
        set { m_simulate = value; }
    }

    public GridWorld(int size)
        : this()
    {

    }

    public GridObj GridObject
    {
        get
        {
            return data.Clone();
        }
        set
        {
            data = value;
        }
    }

    public int getActionCount(int s)
    {
        return 4;
    }
    
    public void SwitchActions()
    {
        int[] temp = actionDirections;
        actionDirections = actionDirections2;
        actionDirections2 = temp;
    }


    int actCount = 0;
    public void ExecuteAction(int s, int a, ref int sP, ref double Reward)
    {
        Point p = stateToLocation(s);
        int r = 0;
        if (a >= 4)
        {
            //sP = Mouse.Q[s].Actions[a].O.Execute(s,ref sP, Qoptions[s[0], s[1]][a - 4].Option.executeOption(s, ref steps);
            throw new Exception();
        }
        else
        {
            if (a == actionDirections[0])
                p.Y--;
            if (a == actionDirections[1])
                p.Y++;
            if (a == actionDirections[2])
                p.X--;
            if (a == actionDirections[3])
                p.X++;

            if (p.X < 0 || p.Y < 0 || p.X >= GridSize || p.Y >= GridSize)
                p = stateToLocation(s);

            if (isBlock(p))
                p = stateToLocation(s);
        }
        if (isGoal(p))
        {
            r = 1;

            actCount++;
            if (actCount >= 12)
            {
                SwitchActions();
                actCount = 0;
            }

        }
        Reward = r;
        sP = locationToState(p);
    }

    public bool isGoal(int s)
    {
        return isGoal(stateToLocation(s));
    }

    public void LoadData(string FileName)
    {
        Clear();
        data = GridObj.LoadData(FileName);
        drawMe();
    }

    public void SaveData(string FileName)
    {
        //Reset();
        //FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
        //byte[] array = new byte[(6 + data.Size * data.Size) * 4];
        //int index = 0;
        //BitConverter.GetBytes(data.Size).CopyTo(array, index);
        //index += 4;
        //for (int i = 0; i < data.grid.GetLength(0); i++){
        //    for (int j = 0; j < data.grid.GetLength(1); j++){
        //        BitConverter.GetBytes(data.grid[i, j]).CopyTo(array, index);
        //        index += 4;
        //    }
        //}
        //BitConverter.GetBytes(m_mouseLocationBU.X).CopyTo(array, index);
        //index += 4;
        //BitConverter.GetBytes(m_mouseLocationBU.Y).CopyTo(array, index);
        //index += 4;
        //BitConverter.GetBytes(m_cheeseLocationBU.X).CopyTo(array, index);
        //index += 4;
        //BitConverter.GetBytes(m_cheeseLocationBU.Y).CopyTo(array, index);
        //index += 4;
        //fs.Write(array, 0, array.Length);
        //fs.Flush();
        //fs.Close();
    }

   // List<Cat> m_catsBackup = new List<Cat>();
    public void Reset()
    {
        m_mouseLocation = data.agentLocation;
        drawMe();
    }


    public void Clear()
    {
        data.grid = new int[data.Size, data.Size];
        //ShowArrows = false;
        m_simulate = false;
        drawMe();
    }

    public Point PixelToLocation(Point p, Size ImageSize)
    {
        int x = 0, y = 0;
        float start = 0;
        float step_size = (ImageSize.Width - data.Size + 1) / (float)data.Size;
        for (int i = 0; i < data.Size; i++)
        {
            if (p.X >= start - 1 && p.X <= start + step_size)
            {
                x = i;
                break;
            }
            start += step_size + 1;
        }

        start = 0;
        step_size = (ImageSize.Height - data.Size + 1) / (float)data.Size;
        for (int i = 0; i < data.Size; i++)
        {
            if (p.Y >= start - 1 && p.Y <= start + step_size)
            {
                y = i;
                break;
            }
            start += step_size + 1;
        }
        return new Point(x, y);
    }

    public RectangleF LocationToRectange(Point p, Size ImageSize)
    {
        float x = 0, y = 0, width = 0,height = 0;
        float step_size = ((ImageSize.Width - data.Size - 1) / (float)data.Size) + 1;
        x = p.X * step_size;
        width = step_size;
        step_size = ((ImageSize.Height - data.Size - 1) / (float)data.Size) + 1;
        y = p.Y * step_size;
        height = step_size;
        return new RectangleF(x, y, width, height);
    }

    Point m_pointer;
    public Point Pointer
    {
        get
        {
            return m_pointer;
        }
        set
        {
            bool change = false;
            if (m_pointer != value)
                change = true;
            m_pointer = value;
            if(change)
                OnPaint(null);
        }
    }

    //public void AddCat(Cat c)
    //{
    //    m_cats.Add(c);
    //    m_catsBackup.Add(new Cat(c.Location, c.Direction, this));
    //    drawMe();
    //}

    public void AddBlock(Point p)
    {
        Point loc = PixelToLocation(p, Size);
        data.grid[loc.X, loc.Y] = data.grid[loc.X, loc.Y] == 1 ? 0 : 1;
        drawMe();
    }

    public bool isGoal(Point p)
    {
        return data.goalLocation == p;
    }

    public bool isBlock(Point p)
    {
        return data.grid[p.X, p.Y] == 1;
    }


    public void Run()
    {
        drawMe();
    }

    public void RunResult()
    {
        if (Agent == null)
        {
            MessageBox.Show("Learn first!");
            return;
        }
        while (true)
        {
            Point p = new Point(m_mouseLocation.X, m_mouseLocation.Y);
            int s = locationToState(p);
            int a = Agent.Q[s].GetMaxAction();

            if (a == actionDirections[0])               //UP
                p.Y--;
            if (a == actionDirections[1])               //Down
                p.Y++;
            if (a == actionDirections[2])               //Left
                p.X--;
            if (a == actionDirections[3])               //Right
                p.X++;
            if (a >= 4)
            {
                Option op = Agent.Q[s].Actions[a].O;;
                int sP = locationToState(p);
                while (sP != op.m_subgoal.State)
                {
                    int[] temp = op.maxA(sP);

                    p = stateToLocation(temp[0]);
                    m_mouseLocation = p;
                    sP = temp[0];
                    drawMe();
                    Thread.Sleep(100);
                }
            }

            if (!(p.X < 0 || p.Y < 0 || p.X >= GridSize || p.Y >= GridSize || isBlock(p)))
                m_mouseLocation = p;

            drawMe();
            if (data.goalLocation == m_mouseLocation)
                break;
            Thread.Sleep(100);
        }
        if (RunFinished != null)
            RunFinished();
    }

    public Point stateToLocation(int s)
    {
        int x = s % GridSize;
        int y = (s - x) / GridSize;
        return new Point(x, y);
    }

    public int locationToState(Point p)
    {
        int s = p.Y * GridSize + p.X;
        return s;
    }

    public int locationToState(int x, int y)
    {
        int s = y * GridSize + x;
        return s;
    }

    public List<int> StartLearn(ReinforcementLearningAlgorithms algorithm)
    {
        if (Agent == null)
            throw new Exception("Agent not set!");
        switch (algorithm)
        {
            //case ReinforcementLearningAlgorithms.Sarsa:
            //    Mouse.LearnSarsa(500);
            //    break;
            //case ReinforcementLearningAlgorithms.WatkinsQLambda:
            //    Mouse.LearnWatkins(500);
            //    break;
            //case ReinforcementLearningAlgorithms.DynaQ:
            //    Mouse.LearnDynaQ(5);
            //    break;
            case ReinforcementLearningAlgorithms.PrioritizedSweeping:
                Agent.LearnPS(10, 500);
                break;
            default:
                Agent = null;
                break;
        }
        if (Agent != null)
            return Agent.EpisodeSteps;
        else
            return null;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        if (data != null && this.Width > 2 && this.Height > 2)
            drawMe();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if(data != null)
            drawMe();
    }

    public void drawMe()
    {
        if (!m_simulate)
        {
            Size ImageSize = Size;
            Bitmap bmp = new Bitmap(Size.Width, Size.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            if (m_showArrows && Agent != null)
            {
                for (int i = 0; i < GridSize; i++)
                {
                    for (int j = 0; j < GridSize; j++)
                    {
                        bool allZero = true;
                        int s = locationToState(i,j);
                        for (int t = 0; t < Agent.Q[s].Actions.Count; t++)
                        {
                            if (Agent.Q[s][t] > 0)
                            {
                                allZero = false;
                                break;
                            }
                        }

                        if(allZero == false)
                        {
                            int a = Agent.Q[s].GetMaxAction();
                            Image img = RL_smdp.Properties.Resources.Up1;
                            if (a == 1)
                                img = RL_smdp.Properties.Resources.Down1;
                            if (a == 2)
                                img = RL_smdp.Properties.Resources.Left1;
                            if (a == 3)
                                img = RL_smdp.Properties.Resources.Right1;
                            if (a >= 4)
                            {
                                Bitmap bmap = new Bitmap(30, 30); //Arrow size 15x15
                                Graphics gr = Graphics.FromImage(bmap);
                                gr.FillRectangle(Brushes.Red, 1, 1, 28, 28);
                                string st = Agent.Q[s].Actions[a].O.m_subgoal.State.ToString();
                                gr.DrawString(st, Font, Brushes.White, 1, 1);
                                gr.Dispose();
                                img = bmap;
                            }
                            if (Agent.Q[s][a] < 0)
                            {
                                Graphics gimg = Graphics.FromImage(img);
                                gimg.DrawRectangle(Pens.Red, new Rectangle(0, 0, 14, 14));
                                gimg.Dispose();
                            }
                            g.DrawImage(img, LocationToRectange(new Point(i, j), ImageSize));
                            //g.DrawString(Mouse.Model[i, j, cat,0][3].ToString(),this.Font,Brushes.Green,LocationToRectange(new Point(i, j),ImageSize));
                        }
                    }
                }
            }
            if (data != null)
            {
                for (int i = 0; i < data.grid.GetLength(0); i++)
                {
                    for (int j = 0; j < data.grid.GetLength(1); j++)
                        if (data.grid[i, j] == 1)
                            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 95, 100, 120)), LocationToRectange(new Point(i, j), ImageSize));
                }
            }

            g.FillEllipse(new SolidBrush(Color.FromArgb(190, 250, 150, 0)), LocationToRectange(CheeseLocation, ImageSize));
            g.FillEllipse(new SolidBrush(Color.FromArgb(190, 50, 255, 0)), LocationToRectange(MouseLocation, ImageSize));



            float start = 0;
            float step_size = (ImageSize.Width - data.Size + 1) / (float)data.Size;
            for (int i = 0; i < data.Size; i++)
            {
                start += step_size;
                g.DrawLine(Pens.Black, start, 0, start, ImageSize.Height - 1);
                start++;
            }

            start = 0;
            step_size = (ImageSize.Height - data.Size + 1) / (float)data.Size;
            for (int i = 0; i < data.Size; i++)
            {
                start += step_size;
                g.DrawLine(Pens.Black, 0, start, ImageSize.Width - 1, start);
                start++;
            }



            if (m_pointer != null)
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, 50, 100, 255)), LocationToRectange(m_pointer, ImageSize));


            Graphics tmp = this.CreateGraphics();
            tmp.DrawImage(bmp, 0, 0);
            tmp.Dispose();
            g.Dispose();
            bmp.Dispose();
            System.GC.Collect();
            Application.DoEvents();
        }
    }

    public void GridWorld_MouseMove(object sender, MouseEventArgs e)
    {
        Pointer = PixelToLocation(e.Location, Size);
    }

    public int GridSize
    {
        get
        {
            return data.Size;
        }
        set
        {

            data.Size = value;
        }
    }

    private void GridWorld_Load(object sender, EventArgs e)
    {

    }

    public int State
    {
        get
        {
            return locationToState(m_mouseLocation);
        }
        set
        {
            m_mouseLocation = stateToLocation(value);
        }
    }

    public Point MouseLocation
    {
        get
        {
            return m_mouseLocation;
        }
        set
        {
            m_mouseLocation = value;
            if (Agent != null)
                Agent.currentState = locationToState(value);
            data.agentLocation = value;
            drawMe();
        }
    }

    public Point CheeseLocation
    {
        get
        {
            return data.goalLocation;
        }
        set
        {
            data.goalLocation = value;
            drawMe();
        }
    }
}

public enum ReinforcementLearningAlgorithms
{
    WatkinsQLambda = 0,
    Sarsa,
    DynaQ,
    PrioritizedSweeping
}
