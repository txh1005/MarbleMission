using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallListSpawn : MonoBehaviour
{
    private static BallListSpawn instance;
    public static BallListSpawn Instance { get => instance; }

    public Ball[] prefab;
    public PathCreator path;
    public float speed;
    public float delayTime;
    public int maxBall;
    public List<Ball> ballList;
    public Transform storeBallObj;

    public Ball[] testNextPoint;
    private void Awake()
    {
        if (BallListSpawn.instance != null) Debug.LogError("Only 1 BallListSpawn allow to exist");
        BallListSpawn.instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(delayTime);
        for (int i = 0; i < maxBall; i++)
        {
            yield return new WaitForSeconds(delayTime);
            Ball ball = Instantiate(prefab[Random.Range(0, prefab.Length)], path.path.GetPoint(0), Quaternion.identity);
            ball.id = ballList.Count;
            ballList.Add(ball);
            ball.transform.SetParent(storeBallObj);
            StartCoroutine(MoveBall(i));
        }
    }
    public IEnumerator MoveBall(int i)
    {
        //float distance = ;
        while (ballList[i].distance < (path.path.length - 1))
        {
            //Debug.Log(distance);
            ballList[i].distance += speed * Time.deltaTime;
            ballList[i].transform.position = path.path.GetPointAtDistance(ballList[i].distance);    
            yield return null;
        }
    }
    public void Move1(int index)
    {
        for(int i=0; i<index;i++)
        {
            ballList[i].distance += speed * delayTime;
        }
    }   
    public void InsertList(int index,Ball ball1)
    {
        ball1 = gameObject.GetComponent<Ball>();
        ballList.Insert(index,ball1);
    }    
}
