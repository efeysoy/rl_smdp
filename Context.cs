using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Context
{
    public QClass Q;
    public int currentState;
    public ModelClass Model = new ModelClass();
    public double e = 0.7;
    public bool m_optsLearned = false;
    public List<SubGoal> Subgoals = new List<SubGoal>();
    public List<int> SubgoalCounts = new List<int>();
    public List<int> EpisodeSteps = new List<int>();
    public double std = 100; //Standart Deviation to stop learning
    public int EpsilonDiscount = 0;
    public int[] initiationSetMembers = null;
    public List<InitiationSet> initiationSets = new List<InitiationSet>();
    public double m_Em = 0; //trace of quality RLCD
    public List<double> Em_l = new List<double>();
    public static int id = 0;
    public int cID = 0;
    public List<OptionN> options = new List<OptionN>();

    private Context()
    {
        cID = id++;
    }

    public double Em
    {
        get
        {
            return m_Em;
        }
        set
        {
            Em_l.Add(value);
            m_Em = value;
        }
    }

    public static Context NewContext(Agent agent, bool DisableOptions, int currentState)
    {
        Context x = new Context();
        x.Q = new QClass(agent, DisableOptions);
        x.currentState = currentState;

        return x;
    }

    public override string ToString()
    {
        return "Context " + cID;
    }
}
