using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class State
{
    int m_id;
    Agent agent;
    public List<Action> Actions = new List<Action>();
    public bool DisableOptions = false;

    public State(int ID, Agent agent, bool DisableOptions)
    {
        m_id = ID;
        this.agent = agent;
        this.DisableOptions = DisableOptions;
    }

    public int StateID
    {
        get
        {
            return m_id;
        }
    }

    public double this[int action]
    {
        get
        {
            if (action < 0)
                return 0;
            return Actions[action].Q;
        }
        set
        {
            Actions[action].Q = value;
        }
    }

    Dictionary<int, int> visitN = new Dictionary<int, int>();
    public int GetMaxAction()
    {
        if (Actions.Count <= 0)
            return -1;
        List<int> templ = new List<int>();
        templ.Add(0);
        int a = 0;
        for (int i = 1; i < Actions.Count; i++)
        {
            if (DisableOptions && Actions[i].O != null)
                continue;
            if (Actions[i].Q > Actions[templ[0]].Q)
            {
                templ.Clear();
                templ.Add(i);
            }
            if (Actions[i].Q == Actions[templ[0]].Q)
            {
                templ.Add(i);
            }
        }
        a = templ[agent.rand.Next() % templ.Count];
        return a;
    }

    public int GetRandomAction(int exclude)
    {
        if (Actions.Count <= 0)
            return -1;
        List<int> templ = new List<int>();
        for (int i = 0; i < Actions.Count; i++)
        {
            if (exclude == i)
                continue;
            if (DisableOptions && Actions[i].O != null)
                continue;
            templ.Add(i);
        }
        return templ[agent.rand.Next() % templ.Count];
    }


}

public class Action
{
    public double Q = 0;
    public OptionN O = null;
    public Action() { }
    public Action(OptionN o)
    {
        O = o;
    }
}

