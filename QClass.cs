using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class QClass
{
    List<State> States = new List<State>();
    Agent m_agent;

    public bool DisableOptions = false;

    public QClass(Agent agent, bool DisableOptions)
    {
        m_agent = agent;
        this.DisableOptions = DisableOptions;
    }

    public State this[int stateID]
    {
        get
        {
            while (stateID >= States.Count)
            {
                States.Add(null);
            }

            if (States[stateID] == null)
            {
                States[stateID] = new State(stateID, m_agent, DisableOptions);
                int a = m_agent.environment.getActionCount(stateID);
                for (int i = 0; i < a; i++)
                {
                    States[stateID].Actions.Add(new Action());
                }
            }
            return States[stateID];
        }
    }
}
