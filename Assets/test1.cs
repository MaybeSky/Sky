using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour {

    GameObject m_map;
    GameObject m_cube;

    //大地图宽度及高度
    private float m_mapWidth;
    private float m_mapHeight;

    private float m_widthCheck;
    private float m_heightCheck;

    private float m_cubeOfMapX = 0;
    private float m_cubeOfMapY = 0;

    bool m_keyUp;
    bool m_keyDown;
    bool m_keyLeft;
    bool m_keyRight;

    public Texture m_mapTexture;
    public Texture m_mapCubeTexture;

	// Use this for initialization
	void Start () {
        m_map = GameObject.Find("map");
        m_cube = GameObject.Find("cube");

        if (GameObject.Find("map") && GameObject.Find("cube"))
            Debug.Log("1");

        //大地图默认宽度高度及缩放比例
        float size_x = m_map.GetComponent<MeshFilter>().mesh.bounds.size.x;
        float scal_x = m_map.transform.localScale.x;
        float size_z = m_map.GetComponent<MeshFilter>().mesh.bounds.size.z;
        float scal_z = m_map.transform.localScale.z;

        //计算真实宽高
        m_mapWidth = size_x * scal_x;
        m_mapHeight = size_z * scal_z;

        //检测越界值
        m_widthCheck = m_mapWidth / 2;
        m_heightCheck = m_mapHeight / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        m_keyUp = GUILayout.RepeatButton("向前移动");
        m_keyLeft = GUILayout.RepeatButton("向左移动");
        m_keyDown = GUILayout.RepeatButton("向后移动");
        m_keyRight = GUILayout.RepeatButton("向右移动");

        GUI.DrawTexture(new Rect(Screen.width - m_mapTexture.width, 0, m_mapTexture.width, m_mapTexture.height), m_mapTexture);
        GUI.DrawTexture(new Rect(m_cubeOfMapX, m_cubeOfMapY, m_mapCubeTexture.width, m_mapCubeTexture.height), m_mapCubeTexture);

    }

    void FixedUpdate()
    {
        if (m_keyUp)
        {
            m_cube.transform.Translate(Vector3.forward * Time.deltaTime * 5);
            check();
        }

        if (m_keyLeft)
        {
            m_cube.transform.Translate(-Vector3.right * Time.deltaTime * 5);
            check();
        }

        if (m_keyDown)
        {
            m_cube.transform.Translate(-Vector3.forward * Time.deltaTime * 5);
            check();
        }

        if (m_keyRight)
        {
            m_cube.transform.Translate(Vector3.right * Time.deltaTime * 5);
            check();
        }
    }

    void check()
    {
        float x = m_cube.transform.position.x;
        float z = m_cube.transform.position.z;

        if (x >= m_widthCheck)
            x = m_widthCheck;

        if (x <= -m_widthCheck)
            x = -m_widthCheck;

        if (z >= m_heightCheck)
            z = m_heightCheck;

        if (z <= -m_heightCheck)
            z = -m_heightCheck;

        m_cube.transform.position = new Vector3(x, m_cube.transform.position.y, z);

        m_cubeOfMapX = (m_mapTexture.width / m_mapWidth * x) + ((m_mapTexture.width / 2) - (m_mapCubeTexture.width / 2))
            + (Screen.width - m_mapTexture.width);
        m_cubeOfMapY = m_mapTexture.height - ((m_mapTexture.height / m_mapHeight * z) + (m_mapTexture.height / 2));
    }
}
