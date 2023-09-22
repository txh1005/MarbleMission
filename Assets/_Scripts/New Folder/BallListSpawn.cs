/*using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallListSpawn : Singleton<BallListSpawn>
{

    public Ball2[] ball;
    public PathCreator path;
    public float speed;
    public float delayTime;
    public int maxBall;
    public List<Ball2> ballList;
    public Transform storeBallObj;

    public Ball2[] testNextPoint;

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
            Ball2 _ball = Instantiate(ball[Random.Range(0, ball.Length)], path.path.GetPoint(0), Quaternion.identity);
            //ball.id = ballList.Count;
            ballList.Add(_ball);
            _ball.transform.SetParent(storeBallObj);
            //StartCoroutine(MoveBall(i));
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
            //ballList[i].distance += speed * delayTime;
        }
    }   
    public void InsertList(int index,Ball2 ball1)
    {
        ball1 = gameObject.GetComponent<Ball2>();
        ballList.Insert(index,ball1);
    } 
}
*/