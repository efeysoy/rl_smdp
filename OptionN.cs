using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OptionN
{
    public Agent m_agent;
    public Dictionary<int, double> V;
    public Dictionary<int, OptionState> initSet = new Dictionary<int, OptionState>();
    int SubGoalState;
    public OptionN(int SubGoalState, Agent agent)
    {
        m_agent = agent;
        this.SubGoalState = SubGoalState;
    }

    public int SubgoalState
    {
        get
        {
            return this.SubGoalState;
        }
    }

    public void AddSASp(int s, int a, int sP)
    {
        if (!initSet.Keys.Contains(s))
            initSet.Add(s, new OptionState(s));

        OptionState os = initSet[s];
        if (os.aSp.Keys.Contains(a))
            throw new Exception("Action already exists on current option-state!");
        os.aSp.Add(a, sP);
    }

    
    public void Execute(int s, ref int sP, ref int steps)
    {
        int lclsteps = 0;
        sP = s;
        double r_temp = 0;
        //steps--;
        int max = 5;
        while (sP != SubGoalState)
        {
            int[] temp = maxA(sP);
            int a = temp[1];
            s = sP;
            if (temp[0] == -1)
                break;// theres no choice in initiation set from this state
            //sP = temp[0];
            bool contextChanged = m_agent.executeAction(sP, temp[1], ref sP, ref r_temp, ref steps);
            if (sP == s)
            {
                max--;
                if (max == 0)
                {
                    break;
                }
            }
            else
                max = 5;
            if (contextChanged || temp[0] != sP)
                break;
            if (m_agent.Model[sP, temp[1]] == null)
            {
                ModelValue m = new ModelValue(s, a, sP, r_temp, 1);
                m_agent.Model[s, a] = m;
            }
            else
                m_agent.Model[s, a].Update(s, a, sP, r_temp, 1);
            //steps++;
            lclsteps++;
            if (lclsteps > 100)
                break;
        }
        //steps++;
    }

    public void Learn()
    {
        V = new Dictionary<int, double>();
        foreach (int k in initSet.Keys)
            V.Add(k, 0);
        if (V.Count <= 1)
            return; ///DÜZELTİLECEK
        double delta = 0;
        double theta = 0.5;
        double gamma = 0.7;
        V[SubGoalState] = 20;
        int[] keys = V.Keys.ToArray();
        do
        {
            delta = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                int s = keys[i];
                double v = V[s];
                int[] temp = maxA(s);
                if (temp[0] < 0)
                    continue;
                double reward = 0;
                if (temp[0] == SubGoalState)
                    reward = 1;
                //else if (temp[0] == s)
                //    reward = -1;
                V[s] = reward + gamma * V[temp[0]];
                delta = delta >= Math.Abs(v - V[s]) ? delta : Math.Abs(v - V[s]);
            }
        } while (delta > theta);


    }

    public int[] maxA(int s)
    {
        List<int> tempStates = new List<int>();

        foreach (int i in initSet[s].aSp.Keys)
        {
            if (initSet[s].aSp[i] != 0 && initSet.Keys.Contains(initSet[s].aSp[i]))
            {
                if (initSet[s].aSp[i] == SubGoalState)
                    return new int[] { initSet[s].aSp[i], i };
                if (tempStates.Count <= 0)
                {
                    tempStates.Add(i);
                    continue;
                }
                if (V[initSet[s].aSp[tempStates[0]]] < V[initSet[s].aSp[i]])
                {
                    tempStates.Clear();
                    tempStates.Add(i);
                }
                else if (V[initSet[s].aSp[tempStates[0]]] == V[initSet[s].aSp[i]])
                {
                    tempStates.Add(i);
                }
            }
        }
        if (tempStates.Count <= 0)
            return new int[] { -1, -1 };

        int a = tempStates[m_agent.rand.Next(tempStates.Count)];
        return new int[] { initSet[s].aSp[a], a };
    }

    double e = 0.1;
    public int[] e_greedy(int s)
    {
        if (m_agent.rand.NextDouble() > e)
            return maxA(s);
        List<int> tempStates = new List<int>();

        for (int i = 0; i < initSet[s].aSp.Count; i++)
        {
            if (V.ContainsKey(initSet[s].aSp[i]))
            {
                tempStates.Add(i);
            }
        }

        if (tempStates.Count <= 0)
            return new int[] { -1, -1 };

        int a = tempStates[m_agent.rand.Next(tempStates.Count)];
        return new int[] { initSet[s].aSp[a], a };
    }

    public override string ToString()
    {
        return "Option " + 0 + " to state " + SubGoalState;
    }
}

public class OptionState
{
    int s;
    public Dictionary<int, int> aSp = new Dictionary<int, int>();
    public OptionState(int s)
    {
        this.s = s;
    }
}
