using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Move : MonoBehaviour {

    public GameObject tailPrefab;
    private GameObject snakeHead, foodPrefab;
    private List<Transform> m_snakeTail = new List<Transform>();
    private ArrayList path;
    private enum Direciton {FORWARD, BACK, LEFT, RIGHT}
    private Direciton m_snakeDir = Direciton.FORWARD;
    private Vector3 m_snakeDirection = Vector3.forward;
    public static bool isEat = false;
    Point current, foodPoint;
    private List<Vector3> snakePosition = new List<Vector3>();

    void Awake()
    {
        initSnake();
    }

	// Use this for initialization
	void Start () {
        
        snakeHead = GameObject.FindGameObjectWithTag("SnakeHead");
        foodPrefab = GameObject.FindGameObjectWithTag("FoodPrefab");
        findFood();
        InvokeRepeating("move", 0, 0.1f);
        move();
    }

    void Update () {           
        if (isEat)
        {
            //CreateFood.createFood();
            snakeHead = GameObject.FindGameObjectWithTag("SnakeHead");
            foodPrefab = GameObject.FindGameObjectWithTag("FoodPrefab");
            findFood();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("FoodPrefab"))
        {
            isEat = true;
            Destroy(other.gameObject);
        }
    }

    void initSnake()
    {
        Vector3 firstTailPosition = new Vector3(this.transform.position.x, 0, this.transform.position.z - 1);
        GameObject firstTail = Instantiate(tailPrefab, firstTailPosition, Quaternion.identity);
        m_snakeTail.Insert(m_snakeTail.Count, firstTail.transform);
    }

    void findFood()
    {
        current = new Point(snakeHead.transform.position);
        foodPoint = new Point(foodPrefab.transform.position);
        path = AStar.findPath(current, foodPoint);               
    }

    void move()
    {
        Point p = (Point)path[0];
        getNextDirections(this.transform.position, p.m_position);
        path.RemoveAt(0);
        
        Vector3 newTailPositon = this.transform.position;
        this.transform.Translate(m_snakeDirection);
        if (isEat)
        {
            GameObject newTail = Instantiate(tailPrefab, newTailPositon, Quaternion.identity);
            m_snakeTail.Insert(m_snakeTail.Count, newTail.transform);
            isEat = false;
        }

        if(m_snakeTail.Count > 0)
        {
            m_snakeTail.Last().position = newTailPositon;
            m_snakeTail.Insert(0, m_snakeTail.Last());
            m_snakeTail.RemoveAt(m_snakeTail.Count - 1);
        }
    }

    Vector3 getNextDirections(Vector3 currentPosition, Vector3 targetPosition)
    {
        
        if(targetPosition - currentPosition == Vector3.forward)
        {
            m_snakeDir = Direciton.FORWARD;
            m_snakeDirection = Vector3.forward;
        }

        if(targetPosition - currentPosition == Vector3.back)
        {
            m_snakeDir = Direciton.BACK;
            m_snakeDirection = Vector3.back;
        }

        if(targetPosition - currentPosition == Vector3.left)
        {
            m_snakeDir = Direciton.LEFT;
            m_snakeDirection = Vector3.left;
        }

        if(targetPosition - currentPosition == Vector3.right)
        {
            m_snakeDir = Direciton.RIGHT;
            m_snakeDirection = Vector3.right;
        }
        return m_snakeDirection;
    }
}


