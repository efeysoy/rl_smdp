using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ModelClass
{
    public List<ModelState> States = new List<ModelState>();

    public List<ModelValue> All_SA_PredictedToLeadTo(int sP)
    {
        List<ModelValue> result = new List<ModelValue>();
        for (int i = 0; i < States.Count; i++)
        {
            if (States[i] == null) continue;

            for (int j = 0; j < States[i].Count; j++)
            {
                if (States[i][j] == null) continue;
                if (States[i][j].sP == sP)
                    result.Add(States[i][j]);
            }
        }
        return result;
    }

    public int StateCount
    {
        get
        {
            return States.Count; 
        }
    }

    public ModelValue this[int state, int action]
    {
        set
        {
            while (States.Count <= state)
            {
                States.Add(null);
            }
            if (States[state] == null)
                States[state] = new ModelState(state);

            while (States[state].Count <= action)
            {
                States[state].Add(null);
            }
            //if (Values[state.StateID][action].p < value.p)
            States[state][action] = value;

        }
        get
        {
            if (state >= States.Count)
                return null;
            else
            {
                if (States[state] == null)
                    return null;
                if (action >= States[state].Count)
                    return null;
                else
                    return States[state][action];
            }
        }
    }

    public ModelState this[int state]
    {
        set
        {
            while (States.Count <= state)
            {
                States.Add(null);
            }
            States[state] = value;

        }
        get
        {
            if (state >= States.Count)
                return null;
            else
            {
                return States[state];
            }
        }
    }
}

public class ModelState{
    int stateID;

    public bool LeadsTo(int s)
    {
        for (int i = 0; i < modelValues.Count; i++)
        {
            if (modelValues[i] != null && modelValues[i].sP == s)
                return true;
        }
        return false;
    }

    List<ModelValue> modelValues = new List<ModelValue>();
    public ModelState(int State)
    {
        stateID = State;
    }

    public int State
    {
        get
        {
            return stateID;
        }
    }

    public void Add(ModelValue value)
    {
        modelValues.Add(value);
    }

    public int Count
    {
        get
        {
            return modelValues.Count;
        }
    }

    public ModelValue this[int action]
    {
        get
        {
            if (action >= modelValues.Count)
                return null;
            return modelValues[action];
        }
        set
        {
            modelValues[action] = value;
        }
    }

    public object Clone()
    {
        throw new NotImplementedException();
    }
}

public class ModelValue
{
    public double reward;
    public static double Rmin = int.MaxValue, Rmax = int.MinValue;
    public int sP;
    public int steps;
    public int s, a;
    private int m_count = 0;
    public double Rm = 0;
    public Dictionary<int,double> Tm = new Dictionary<int,double>();
    public Dictionary<int, double> dTm = new Dictionary<int, double>();
    public static int M = 100; //number of past experiences
    public static double Omega = 0;
    
    public double Em = 0;

    public int Nm
    {
        get
        {
            return m_count > M ? M : m_count;
        }
    }

    public double Cm
    {
        get
        {
            return Nm / (double)M;
        }
    }

    private double ZR
    {
        get
        {
            if (Rmax - Rmin == 0)
                return 0;
            return (Rmax - Rmin);
        }
    }

    private double ZT
    { get { return 0.5 * (Math.Pow(Nm + 1, 2)); } }

    public int count
    {
        get { return m_count; }
    }

    public ModelValue(int s, int a, int sp, double reward, int steps)
    {
        this.s = s;
        this.a = a;
        this.reward = reward;
        this.steps = steps;
        this.sP = sp;
        m_count++;

    }

    public double tempRm = 0, tempCm = 0;
    double dRm;
    public Dictionary<int, double> tempTm = new Dictionary<int, double>();

    public double calculateEm(int sp, double reward)
    {
        if (reward < Rmin) Rmin = reward;
        if (reward > Rmax) Rmax = reward;

        dRm = (reward - Rm) / (Nm + 1);
        tempRm = Rm + dRm;

        if (!Tm.Keys.Contains(sp))
        {
            if (Tm.Count == 0)
            {
                Tm.Add(sp, 0);//1 mi olmalı?
                dTm.Add(sp, 0);
            }
            else
            {
                Tm.Add(sp, 0);
                dTm.Add(sp, 0);
            }
        }

        double totaldTm = 0;
        for (int i = 0; i < Tm.Count; i++)
        {
            KeyValuePair<int, double> val = Tm.ElementAt(i);
            if (val.Key == sp)
            {
                dTm[val.Key] = (1 - Tm[val.Key]) / (Nm + 1);
            }
            else
            {
                dTm[val.Key] = (0 - Tm[val.Key]) / (Nm + 1);
            }
            totaldTm += (dTm[val.Key] * dTm[val.Key]);
            tempTm[val.Key] = Tm[val.Key] + dTm[val.Key];
        }

        double eRm = 1 - 2 * (ZR * (dRm * dRm));

        double eTm = 1 - 2 * (ZT * totaldTm);

        double em = Cm * (Omega * eRm + (1 - Omega) * eTm);

        if (sp != sP)
            return -0.9;
        else
            return 0.9;
        return em;
    }

    public void Update(int s, int a, int sp, double reward, int steps)
    {
        this.s = s;
        this.a = a;
        this.reward = reward;
        this.steps = steps;
        this.sP = sp;
        m_count++;

        Rm = Rm + dRm;
        Tm = tempTm;

    }
}
