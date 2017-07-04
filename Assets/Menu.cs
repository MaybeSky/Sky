using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        GUI.Button(new Rect(230, 200, 100, 30), "开始游戏");
        GUI.Button(new Rect(230, 240, 100, 30), "排行榜");
        GUI.Button(new Rect(230, 240, 100, 30), "退出");
    }
}
