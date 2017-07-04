using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue {
    ArrayList points = new ArrayList();

    public int count()
    {
        return this.points.Count;
    }

    public bool contains(Point point)
    {
        foreach (Point p in points)
        {
            if (point.m_position == p.m_position)
                return true;

        }
        return false;
    }

    public Point firstPoint()
    {
        if(points.Count > 0)
            return (Point)points[0];
        return null;
    }

    public void pushBack(Point point)
    {
        this.points.Add(point);
        this.points.Sort();
    }

    public void remove(Point point)
    {
        this.points.Remove(point);
        this.points.Sort();
    }

    public void clear()
    {
        this.points.Clear();
    }
}
