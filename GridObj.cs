using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


public class GridObj
{
    private int m_size;
    private int[,] grid;
    private int[,] flow;
    public Point goalLocation;
    public Point agentLocation;

    Color[] flowColors = new Color[] { Color.FromArgb(127, 255, 255, 255), 
                                       Color.FromArgb(127, 255, 255, 0), 
                                       Color.FromArgb(127, 0, 255, 255),
                                       Color.FromArgb(127, 255, 0, 255),
                                       Color.FromArgb(127, 0, 255, 0),
                                       Color.FromArgb(127, 0, 0, 255),
                                       Color.FromArgb(127, 0, 255, 127),
                                       Color.FromArgb(127, 127, 127, 0)};

    private GridObj()
    {
    }

    public void Clear()
    {
        grid = new int[m_size, m_size];
        flow = new int[m_size, m_size];
    }

    public GridObj Clone()
    {
        GridObj temp = new GridObj();
        temp.grid = (int[,])this.grid.Clone();
        temp.flow = (int[,])this.flow.Clone();
        temp.goalLocation = goalLocation;
        temp.agentLocation = agentLocation;
        temp.m_size = m_size;
        return temp;
    }

    public int GetLength(int index)
    {
        return m_size;
    }

    public int Size
    {
        get
        {
            return m_size;
        }
        set
        {
            int[,] temp = new int[value, value];
            int nTemp = m_size < value ? m_size : value;
            for (int i = 0; i < nTemp; i++)
                for (int j = 0; j < nTemp; j++)
                    temp[i, j] = grid[i, j];
            m_size = value;

            grid = temp;
            goalLocation = new Point(value - 1, value - 1);
        }
    }

    public GridObj(int size, Point goal, Point agent)
    {
        m_size = size;
        agentLocation = agent;
        grid = new int[size, size];
        flow = new int[size, size];
        goalLocation = goal;
    }

    public int this[int x, int y]
    {
        get
        {
            return grid[x, y];
        }
        set
        {
            grid[x, y] = value;
        }
    }

    public void setFlow(int x, int y, int value)
    {
        flow[x, y] = value;
    }

    public int getFlow(int x, int y)
    {
        return flow[x, y];
    }

    public Bitmap DrawBmp(int w, int h)
    {
        Bitmap bmp = new Bitmap(w, h);
        Graphics g = Graphics.FromImage(bmp);

        float ws = w / (float)grid.GetLength(0);
        float hs = h / (float)grid.GetLength(1);

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == 1)
                    g.FillRectangle(Brushes.DarkGray, i * ws, j * hs, ws, hs);
                else
                    g.FillRectangle(Brushes.White, i * ws, j * hs, ws, hs);

                g.FillRectangle(new SolidBrush(flowColors[getFlow(i,j)]), i * ws, j * hs, ws, hs);
            }
        }
        g.Dispose();
        return bmp;
    }

    public static GridObj LoadData(string FileName)
    {

        FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
        byte[] array = new byte[4];
        fs.Read(array, 0, 4);
        int size = BitConverter.ToInt32(array, 0);
        int[,] grid = new int[size, size];
        int[,] flow = new int[size, size];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                fs.Read(array, 0, 4);
                grid[i, j] = BitConverter.ToInt32(array, 0);
            }
        }

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                fs.Read(array, 0, 4);
                flow[i, j] = BitConverter.ToInt32(array, 0);
            }
        }
        fs.Read(array, 0, 4);
        int x = BitConverter.ToInt32(array, 0);
        fs.Read(array, 0, 4);
        int y = BitConverter.ToInt32(array, 0);
        Point agentLocation = new Point(x, y);

        fs.Read(array, 0, 4);
        x = BitConverter.ToInt32(array, 0);
        fs.Read(array, 0, 4);
        y = BitConverter.ToInt32(array, 0);
        Point goalLocation = new Point(size - 1, size - 1);
        // -- doğrusu bu -- Point goalLocation = new Point(x, y);

        fs.Close();
        GridObj obj = new GridObj();
        obj.m_size = size;
        obj.grid = grid;
        obj.flow = flow;
        obj.goalLocation = goalLocation;
        obj.agentLocation = agentLocation;

        return obj;
    }

    public static GridObj LoadFromStream(Stream stream)
    {
        FileStream fs = (FileStream)stream;
        
        byte[] array = new byte[4];
        fs.Read(array, 0, 4);
        int size = BitConverter.ToInt32(array, 0);
        GridObj o = new GridObj(size, new Point(0, 0), new Point(size - 1, size - 1));
        for (int i = 0; i < o.grid.GetLength(0); i++)
        {
            for (int j = 0; j < o.grid.GetLength(1); j++)
            {
                fs.Read(array, 0, 4);
                o.grid[i, j] = BitConverter.ToInt32(array, 0);
            }
        }

        for (int i = 0; i < o.grid.GetLength(0); i++)
        {
            for (int j = 0; j < o.grid.GetLength(1); j++)
            {
                fs.Read(array, 0, 4);
                o.flow[i, j] = BitConverter.ToInt32(array, 0);
            }
        }
        fs.Read(array, 0, 4);
        int x = BitConverter.ToInt32(array, 0);
        fs.Read(array, 0, 4);
        int y = BitConverter.ToInt32(array, 0);
        o.agentLocation = new Point(x, y);

        fs.Read(array, 0, 4);
        x = BitConverter.ToInt32(array, 0);
        fs.Read(array, 0, 4);
        y = BitConverter.ToInt32(array, 0);
        o.goalLocation = new Point(x, y);
        return o;
    }

    public void SaveData(string FileName)
    {
        FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
        byte[] array = new byte[(6 + m_size * m_size) * 8];
        int index = 0;
        BitConverter.GetBytes(m_size).CopyTo(array, index);
        index += 4;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                BitConverter.GetBytes(grid[i, j]).CopyTo(array, index);
                index += 4;
            }
        }

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                BitConverter.GetBytes(flow[i, j]).CopyTo(array, index);
                index += 4;
            }
        }
        BitConverter.GetBytes(agentLocation.X).CopyTo(array, index);
        index += 4;
        BitConverter.GetBytes(agentLocation.Y).CopyTo(array, index);
        index += 4;
        BitConverter.GetBytes(goalLocation.X).CopyTo(array, index);
        index += 4;
        BitConverter.GetBytes(goalLocation.Y).CopyTo(array, index);
        index += 4;
        fs.Write(array, 0, array.Length);
        fs.Flush();
        fs.Close();
    }

    public void SaveToStream(Stream stream)
    {
        FileStream fs = (FileStream)stream;
        byte[] array = new byte[(5 * 4) + (m_size * m_size) * 8];
        int index = 0;
        BitConverter.GetBytes(m_size).CopyTo(array, index);
        index += 4;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                BitConverter.GetBytes(grid[i, j]).CopyTo(array, index);
                index += 4;
            }
        }

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                BitConverter.GetBytes(flow[i, j]).CopyTo(array, index);
                index += 4;
            }
        }
        BitConverter.GetBytes(agentLocation.X).CopyTo(array, index);
        index += 4;
        BitConverter.GetBytes(agentLocation.Y).CopyTo(array, index);
        index += 4;
        BitConverter.GetBytes(goalLocation.X).CopyTo(array, index);
        index += 4;
        BitConverter.GetBytes(goalLocation.Y).CopyTo(array, index);
        index += 4;
        fs.Write(array, 0, array.Length);
        fs.Flush();
    }
}

