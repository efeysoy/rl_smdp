using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


public class SubGoal
{
    public SubGoal(int State)
    {
        this.State = State;
    }

    public List<int> I_s = new List<int>();
    public List<SubGoal> Options = new List<SubGoal>();
    public bool isGoal = false;
    public int State;
}