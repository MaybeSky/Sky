using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : IComparable {

    // m_totalCost = m_nodeCost + m_estimatedCost 
    public float m_totalCost;
    public float m_pointCost;
    public float m_estimatedCost;

    public Point m_parent;
    public Vector3 m_position;

    public Point()
    {
        m_totalCost = 0;
        m_pointCost = 0;
        m_estimatedCost = 0;
        m_parent = null;
    }

    public Point(Vector3 position)
    {
        m_totalCost = 0;
        m_pointCost = 0;
        m_estimatedCost = 0;
        m_parent = null;
        m_position = position;
    }

    public int CompareTo(object obj)
    {
        Point point = (Point)obj;
        if (this.m_totalCost < point.m_totalCost)
            return -1;
        if (this.m_totalCost > point.m_totalCost)
            return 1;
        return 0;
    }
}
