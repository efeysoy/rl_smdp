using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface Environment
{
    void ExecuteAction(int s, int a, ref int sP, ref double Reward);
    int getActionCount(int s);
    int State
    {
        get;
        set;
    }
    bool isGoal(int s);

}
