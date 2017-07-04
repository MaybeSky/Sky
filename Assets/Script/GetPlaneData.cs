using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlaneData : MonoBehaviour
{
    static GameObject plane = GameObject.FindGameObjectWithTag("Plane");


    public static float getPlaneSize_x(GameObject plane)
    {
        float size_x = plane.GetComponent<MeshFilter>().mesh.bounds.size.x;
        float scale_x = plane.transform.localScale.x;
        float plane_x = size_x * scale_x;
        return plane_x;
    }

    public static float getPlaneSize_z(GameObject plane)
    {
        float size_z = plane.GetComponent<MeshFilter>().mesh.bounds.size.z;
        float scale_z = plane.transform.localScale.z;
        float plane_z = size_z * scale_z;
        return plane_z;
    }

    public static int getPlaneUpperBoundary()
    {
        return (int)(plane.transform.position.z - getPlaneSize_z(plane) / 2);
    }

    public static int getPlaneLowerBoundary()
    {
        return (int)(plane.transform.position.z + getPlaneSize_z(plane) / 2);
    }

    public static int getPlaneLeftBoundary()
    {
        return (int)(plane.transform.position.x - getPlaneSize_x(plane) / 2);
    }

    public static int getPlaneRightBoundary()
    {
        return (int)(plane.transform.position.x + getPlaneSize_x(plane) / 2);
    }
}
