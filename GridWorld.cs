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
public delegate void OneStepTakenDelegate();
public partial class GridWorld : UserControl, Environment
{

    Color[] flowColors = new Color[] { Color.FromArgb(127, 255, 255, 255), 
                                       Color.FromArgb(127, 255, 255, 0), 
                                       Color.FromArgb(127, 0, 255, 255),
                                       Color.FromArgb(127, 255, 0, 255),
                                       Color.FromArgb(127, 0, 255, 0),
                                       Color.FromArgb(127, 0, 0, 255),
                                       Color.FromArgb(127, 0, 255, 127),
                                       Color.FromArgb(127, 127, 127, 0)};

    GridObj data;
    GridList m_gridlist;
    public event RunFinishedDelegate RunFinished;
    public event OneStepTakenDelegate StepPassed;
    private bool m_showArrows = false;
    public bool m_simulate = false;
    private Point m_mouseLocation = new Point(0,0);
    public bool checkSwitchContext = false;

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
        if (m_gridlist.SelectedIndex + 1 >= m_gridlist.Count)
        {
            m_gridlist.SelectedIndex = 0;
        }
        else
        {
            m_gridlist.SelectedIndex++;
        }
        GridObject = m_gridlist.Get(m_gridlist.SelectedIndex);
        //int[] temp = actionDirections;
        //actionDirections = actionDirections2;
        //actionDirections2 = temp;
    }

    public GridList GridList
    {
        get { return m_gridlist; }
        set { m_gridlist = value; }
    }


    public int actCount = 0;
    public void ExecuteAction(int s, int a, ref int sP, ref double Reward)
    {
        Point p = stateToLocation(s);
        double r = 0;
        if (a >= 4)
        {
            //sP = Mouse.Q[s].Actions[a].O.Execute(s,ref sP, Qoptions[s[0], s[1]][a - 4].Option.executeOption(s, ref steps);
            throw new Exception();
        }
        else
        {
            int flow = data.getFlow(p.X, p.Y);
            if (flow == 5)
            {
                if (a == 1)
                    p.Y--;
                if (a == 0)
                    p.Y++;
                if (a == 3)
                    p.X--;
                if (a == 2)
                    p.X++;
            }
            else if (flow == 6)
            {
                if (a == 2)
                    p.Y--;
                if (a == 3)
                    p.Y++;
                if (a == 1)
                    p.X--;
                if (a == 0)
                    p.X++;
            }
            else if (flow == 7)
            {
                if (a == 3)
                    p.Y--;
                if (a == 2)
                    p.Y++;
                if (a == 0)
                    p.X--;
                if (a == 1)
                    p.X++;
            }
            else
            {
                if (a == 0)
                    p.Y--;
                if (a == 1)
                    p.Y++;
                if (a == 2)
                    p.X--;
                if (a == 3)
                    p.X++;
            }
            if (p.X < 0 || p.Y < 0 || p.X >= GridSize || p.Y >= GridSize)
                p = stateToLocation(s);

            if (isBlock(p))
            {
                p = stateToLocation(s);
                r = -0.001;
            }
            Point pTmp = p;
            if (flow > 0 && flow < 5)
            {
                if (flow == 1 && a != 1)//&& a != 0)
                    p.Y--;
                if (flow == 2 && a != 0)// && a != 1)
                    p.Y++;
                if (flow == 3 && a != 3)// && a != 2)
                    p.X--;
                if (flow == 4 && a != 2)// && a != 3)
                    p.X++;

                if (p.X < 0 || p.Y < 0 || p.X >= GridSize || p.Y >= GridSize)
                    p = pTmp;

                if (isBlock(p))
                {
                    p = pTmp;
                    //r = -1;
                }
            }
        }
        if (isGoal(p))
        {
            r = 10;

            //Environment change by episodes
            if (m_gridlist.Count > 1)
            {
                actCount++;
                if (actCount >= tmp)
                {
                    tmp = 10 + Agent.rand.Next(10);
                    SwitchActions();
                    actCount = 0;
                }
            }
            ////////////////////////////////

        }


        Reward = r;
        sP = locationToState(p);

        //Environment change by steps
        //if (m_gridlist.Count > 1)
        //{
        //    actCount++;
        //    if (actCount >= 100000)
        //    {
        //        SwitchActions();
        //        actCount = 0;
        //        //System.Diagnostics.Debug.WriteLine("State " + s + " to " + sP + " with action " + a);
        //    }
        //}
        ////////////////////////////////
    }
    int tmp = 16;

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
        data.SaveData(FileName);
    }

   // List<Cat> m_catsBackup = new List<Cat>();
    public void Reset()
    {
        m_mouseLocation = data.agentLocation;
        drawMe();
    }


    public void Clear()
    {
        data.Clear();
        //ShowArrows = false;
        m_simulate = false;
        drawMe();
    }

    public Point PixelToLocation(Point p)
    {
        int x = 0, y = 0;
        float start = 0;
        float step_size = (this.Width - data.Size + 1) / (float)data.Size;
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
        step_size = (this.Height - data.Size + 1) / (float)data.Size;
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
        Point loc = PixelToLocation(p);
        data[loc.X, loc.Y] = 1;
        drawMe();
    }

    public void RemoveBlock(Point p)
    {
        Point loc = PixelToLocation(p);
        data[loc.X, loc.Y] = 0;
        drawMe();
    }

    public bool isGoal(Point p)
    {
        return data.goalLocation == p;
    }

    public bool isBlock(Point p)
    {
        return data[p.X, p.Y] == 1;
    }


    public void Run()
    {
        drawMe();
    }

    public void RunResult()
    {
        double oldRho = Agent.rho;
        Agent.rho = 0.1;
        if (Agent == null)
        {
            MessageBox.Show("Learn first!");
            return;
        }

        for (int i = 0; i < Agent.contexts.Count; i++)
            Agent.contexts[i].Em = 0;

        while (true)
        {
            Point p = new Point(m_mouseLocation.X, m_mouseLocation.Y);
            int s = locationToState(p);
            int a = Agent.Q[s].GetMaxAction();
            int flow = data.getFlow(p.X, p.Y);
            if (a < 4)
            {
                if (flow == 5)
                {
                    if (a == 1)               //UP
                        p.Y--;
                    if (a == 0)               //Down
                        p.Y++;
                    if (a == 3)               //Left
                        p.X--;
                    if (a == 2)               //Right
                        p.X++;
                }
                else if (flow == 6)
                {
                    if (a == 2)
                        p.Y--;
                    if (a == 3)
                        p.Y++;
                    if (a == 1)
                        p.X--;
                    if (a == 0)
                        p.X++;
                }
                else if (flow == 7)
                {
                    if (a == 3)
                        p.Y--;
                    if (a == 2)
                        p.Y++;
                    if (a == 0)
                        p.X--;
                    if (a == 1)
                        p.X++;
                }
                else
                {
                    if (a == 0)               //UP
                        p.Y--;
                    if (a == 1)               //Down
                        p.Y++;
                    if (a == 2)               //Left
                        p.X--;
                    if (a == 3)               //Right
                        p.X++;
                }
                if (p.X < 0) p.X = 0;
                if (p.Y < 0) p.Y = 0;
                if (p.X >= GridSize) p.X = GridSize - 1;
                if (p.Y >= GridSize) p.Y = GridSize - 1;
                if (isBlock(p))
                    p = new Point(m_mouseLocation.X, m_mouseLocation.Y);
                Point pTmp = p;

                if (flow > 0)
                {
                    if (flow == 1 && a != 1)
                        p.Y--;
                    if (flow == 2 && a != 0)
                        p.Y++;
                    if (flow == 3 && a != 3)
                        p.X--;
                    if (flow == 4 && a != 2)
                        p.X++;

                    if (p.X < 0 || p.Y < 0 || p.X >= GridSize || p.Y >= GridSize)
                        p = pTmp;

                    if (isBlock(p))
                    {
                        p = pTmp;
                    }
                }
                if (checkSwitchContext)
                    Agent.checkAndSwitchContextNoNew(s, a, locationToState(p), 0);
                if (StepPassed != null)
                    StepPassed();
                    
            }
            else if (a >= 4)
            {
                OptionN op = Agent.Q[s].Actions[a].O;
                int sP = locationToState(p);
                while (sP != op.SubgoalState)
                {
                    flow = data.getFlow(p.X, p.Y);
                    Point pr = p;
                    if (!op.initSet.Keys.Contains(sP))
                        break;
                    int[] temp = op.maxA(sP);
                    if (flow == 5)
                    {
                        if (temp[1] == 1)               //UP
                            p.Y--;
                        if (temp[1] == 0)               //Down
                            p.Y++;
                        if (temp[1] == 3)               //Left
                            p.X--;
                        if (temp[1] == 2)               //Right
                            p.X++;
                    }
                    else if (flow == 6)
                    {
                        if (temp[1] == 2)
                            p.Y--;
                        if (temp[1] == 3)
                            p.Y++;
                        if (temp[1] == 1)
                            p.X--;
                        if (temp[1] == 0)
                            p.X++;
                    }
                    else if (flow == 7)
                    {
                        if (temp[1] == 3)
                            p.Y--;
                        if (temp[1] == 2)
                            p.Y++;
                        if (temp[1] == 0)
                            p.X--;
                        if (temp[1] == 1)
                            p.X++;
                    }
                    else
                    {
                        if (temp[1] == 0)               //UP
                            p.Y--;
                        if (temp[1] == 1)               //Down
                            p.Y++;
                        if (temp[1] == 2)               //Left
                            p.X--;
                        if (temp[1] == 3)               //Right
                            p.X++;
                    }
                    if (p.X < 0) p.X = 0;
                    if (p.Y < 0) p.Y = 0;
                    if (p.X >= GridSize) p.X = GridSize - 1;
                    if (p.Y >= GridSize) p.Y = GridSize - 1;
                    if (isBlock(p))
                    {
                        p = pr;

                    }
                    m_mouseLocation = p;
                    if (checkSwitchContext)
                        if (Agent.checkAndSwitchContextNoNew(sP, temp[1], locationToState(p), 0))
                        {
                            break;
                        }
                    if (StepPassed != null)
                        StepPassed();
                    sP = locationToState(p);
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
        Agent.rho = oldRho;
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
                Agent.LearnPS(20, 500);
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
        System.GC.Collect();
        try
        {
            if (!m_simulate)
            {
                Size ImageSize = Size;
                Bitmap bmp = new Bitmap(Size.Width, Size.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);


                for (int i = 0; i < GridSize; i++)
                {
                    for (int j = 0; j < GridSize; j++)
                    {


                        if (m_showArrows && Agent != null)
                        {
                            bool allZero = true;
                            int s = locationToState(i, j);

                            // Check if Q(s) is not learned
                            for (int t = 0; t < Agent.Q[s].Actions.Count; t++)
                            {
                                if (Agent.Q[s][t] > 0)
                                {
                                    allZero = false;
                                    break;
                                }
                            }


                            if (allZero == false) // If agent can make some decision on Q(s)
                            {
                                int a = Agent.Q[s].GetMaxAction();
                                Image img = RL_smdp.Properties.Resources.Up1;
                                if (a == 1)
                                    img = RL_smdp.Properties.Resources.Down1;
                                if (a == 2)
                                    img = RL_smdp.Properties.Resources.Left1;
                                if (a == 3)
                                    img = RL_smdp.Properties.Resources.Right1;
                                if (a >= 4) // Option
                                {
                                    Bitmap bmap = new Bitmap(30, 30); //Arrow size 15x15
                                    Graphics gr = Graphics.FromImage(bmap);
                                    gr.FillRectangle(Brushes.Red, 1, 1, 28, 28);
                                    string st = Agent.Q[s].Actions[a].O.SubgoalState.ToString();
                                    gr.DrawString(st, Font, Brushes.White, 1, 1);
                                    gr.Dispose();
                                    img = bmap;
                                }

                                if (Agent.Q[s][a] < 0)  //???
                                {
                                    Graphics gimg = Graphics.FromImage(img);
                                    gimg.DrawRectangle(Pens.Red, new Rectangle(0, 0, 14, 14));
                                    gimg.Dispose();
                                }
                                g.DrawImage(img, LocationToRectange(new Point(i, j), ImageSize));  // Draw arrow


                            }
                        }


                        // Draw blocks
                        if (data != null)
                        {
                            if (data[i, j] == 1)
                                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 95, 100, 120)), LocationToRectange(new Point(i, j), ImageSize));

                            g.FillRectangle(new SolidBrush(flowColors[data.getFlow(i, j) % 8]), LocationToRectange(new Point(i, j), ImageSize));
                        }
                    }
                }


                //Draw blocks and flow



                //Draw agent and goal
                g.FillEllipse(new SolidBrush(Color.FromArgb(190, 250, 150, 0)), LocationToRectange(CheeseLocation, ImageSize));
                g.FillEllipse(new SolidBrush(Color.FromArgb(190, 50, 255, 0)), LocationToRectange(MouseLocation, ImageSize));



                // Draw vertical lines
                float start = 0;
                float step_size = (ImageSize.Width - data.Size + 1) / (float)data.Size;
                for (int i = 0; i < data.Size; i++)
                {
                    start += step_size;
                    g.DrawLine(Pens.Black, start, 0, start, ImageSize.Height - 1);
                    start++;
                }


                // Draw horizonal lines
                start = 0;
                step_size = (ImageSize.Height - data.Size + 1) / (float)data.Size;
                for (int i = 0; i < data.Size; i++)
                {
                    start += step_size;
                    g.DrawLine(Pens.Black, 0, start, ImageSize.Width - 1, start);
                    start++;
                }


                // Draw mouse pointer
                if (m_pointer != null)
                    g.FillRectangle(new SolidBrush(Color.FromArgb(100, 50, 100, 255)), LocationToRectange(m_pointer, ImageSize));


                // Apply last image to the control and run garbage collector
                Graphics tmp = this.CreateGraphics();
                tmp.DrawImage(bmp, 0, 0);
                tmp.Dispose();
                g.Dispose();
                bmp.Dispose();
                System.GC.Collect();
                Application.DoEvents();
            }
        }
        catch
        {
            System.Diagnostics.Debug.WriteLine("Exception at gridworld drawMe()");//Do Nothing;
        }
    }

    public void setFlow(int x, int y, int flow)
    {
        data.setFlow(x, y, flow);
    }

    public void GridWorld_MouseMove(object sender, MouseEventArgs e)
    {
        Pointer = PixelToLocation(e.Location);
    }

    public int GridSize
    {
        get
        {
            return data.Size;
        }
        set
        {
            data = new GridObj(value, new Point(value - 1, value - 1), MouseLocation);
            //data.Size = value;
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
