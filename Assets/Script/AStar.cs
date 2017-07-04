using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AStar : MonoBehaviour
{
    public static PriorityQueue m_openList = new PriorityQueue();
    public static PriorityQueue m_closeList = new PriorityQueue();

    public static ArrayList findPath(Point currentPoint, Point foodPrefab)
    {
        m_openList.pushBack(currentPoint);
        Point minCostPoint = null;

        while (m_openList.count() != 0)
        {
            minCostPoint = m_openList.firstPoint();

            if (minCostPoint.m_position == foodPrefab.m_position)
            {
                return getPath(minCostPoint);
            }

            m_openList.remove(minCostPoint);
            m_closeList.pushBack(minCostPoint);

            ArrayList surroundPoints = getSurroundPoint(minCostPoint);

            for (int i = 0; i < surroundPoints.Count; i++)
            {
                Point target = (Point)surroundPoints[i];
                if (!m_openList.contains(target))
                {
                    target.m_parent = minCostPoint;
                    target.m_pointCost = getPointCost(minCostPoint, target);
                    target.m_estimatedCost = getEstimatedCost(target, foodPrefab);
                    target.m_totalCost = getTotalCost(target);
                    m_openList.pushBack(target);
                }

                else
                {
                    float currentCost = getPointCost(minCostPoint, target);
                    if (currentCost < target.m_pointCost)
                    {
                        target.m_parent = minCostPoint;
                        target.m_pointCost = currentCost;
                        target.m_totalCost = getTotalCost(target);
                    }
                }
            }

        }
        return getPath(minCostPoint);
    }

    public static float getTotalCost(Point target)
    {
        return target.m_pointCost + target.m_estimatedCost;
    }

    public static float getPointCost(Point point, Point target)
    {
        float currentCost = 1;
        float parentCost = target.m_parent == null ? 0 : target.m_parent.m_pointCost;
        return currentCost + parentCost;
    }

    public static float getEstimatedCost(Point point, Point foodPrefab)
    {

        Vector3 estimatedCost = point.m_position - foodPrefab.m_position;
        return estimatedCost.magnitude;
    }

    public static ArrayList getSurroundPoint(Point point)
    {
        ArrayList surroundPoints = new ArrayList();
        for (int i = (int)point.m_position.x - 1; i <= (int)point.m_position.x + 1; i++)
            for (int j = (int)point.m_position.z - 1; j <= (int)point.m_position.z + 1; j++)
                if (isCanreach(point, new Point(new Vector3(i, 0, j))))
                {
                    Point surroundPoint = new Point(new Vector3(i, 0, j));
                    surroundPoints.Add(surroundPoint);
                }
        return surroundPoints;
    }

    public static bool isCanreach(Point point, Point target)
    {
        bool isOutPlane = target.m_position.x + 1 > GetPlaneData.getPlaneRightBoundary()
            || target.m_position.x - 1 < GetPlaneData.getPlaneLeftBoundary()
            || target.m_position.z + 1 > GetPlaneData.getPlaneLowerBoundary()
            || target.m_position.z - 1 < GetPlaneData.getPlaneUpperBoundary();

        bool isSlashPoint = System.Math.Abs(target.m_position.x - point.m_position.x)
                + System.Math.Abs(target.m_position.z - point.m_position.z) == 2;

        bool isSnakeHead = point.m_position == target.m_position;

        if (isOutPlane || isSlashPoint || isSnakeHead)
            return false;
        return true;
    }

    private static ArrayList getPath(Point target)
    {
        ArrayList path = new ArrayList();
        while (target != null)
        {
            path.Add(target);
            target = target.m_parent;
        }
        path.Reverse();
        path.RemoveAt(0);
        clearList();
        return path;
    }

    public static void clearList()
    {
        m_openList.clear();
        m_closeList.clear();
    }
}
