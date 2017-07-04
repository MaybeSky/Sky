using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFood : MonoBehaviour {
    public GameObject FoodPrefab;

    void Awake()
    {
        createFood();
    }    

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("FoodPrefab"))
            createFood();
    }

    public void createFood()
    {
        int food_x = Random.Range(GetPlaneData.getPlaneLeftBoundary() + 1,
            GetPlaneData.getPlaneRightBoundary() - 1);

        int food_z = Random.Range(GetPlaneData.getPlaneUpperBoundary() + 1,
            GetPlaneData.getPlaneLowerBoundary() - 1);

        Vector3 foodPosition = new Vector3(food_x, 0, food_z);
        Instantiate(FoodPrefab, new Vector3(food_x, 0, food_z), Quaternion.identity);
    }
}
