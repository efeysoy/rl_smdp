using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TreeNode
{
    public TreeNode Parent;
    public List<TreeNode> Children;
    public int Value;
    public bool v;

    public TreeNode(TreeNode parent, int value)
    {
        this.Parent = parent;
        this.Value = value;
        if(parent != null)
            this.v = parent.v;
    }

    public bool FamilyContains(int value)
    {
        if (this.Value == value)
            return true;
        if(Parent == null)
            return false;
        return Parent.FamilyContains(value);
    }
}
