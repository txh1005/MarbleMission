using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallListSpawn2 : Singleton<BallListSpawn2>
{
    public Ball2 ballPrefab;
    public BallData[] ballData1;
    public List<Ball2> ballList;
    public int maxBall;
    public Transform storeObj;

    private bool spawningEnabled = true;
    void Start()
    {
        StartCoroutine(Spawn());
    }
    private void Update()
    {
        StartCoroutine(MoveBallAfterReverse());
    }
    public IEnumerator Spawn()
    {
        for (int i = 0; i < maxBall; i++)
        {
            yield return new WaitForSeconds(0.5f);//0.45/speed
            Ball2 newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Vector3 a = newBall.transform.position;
            a.z = 0f;
            newBall.isMove = true;
            newBall.ballData = ballData1[i];
            //ballScript.SetBallData(ballPrefab.GetComponent<Ball2>());
            ballList.Add(newBall);
            newBall.transform.SetParent(storeObj);
        }
    }
    
    public void InsertBall(Ball2 _newBall, int _index)
    {
        for (int i = 0; i < _index; i++)
        {
            Ball2 ball = BallListSpawn2.Instance.ballList[i];
            _newBall.currentWaypointIndex = ball.currentWaypointIndex;
            _newBall.currentWaypointIndex += 2;
            if (ball.currentWaypointIndex >= ball.waypoints.Length)
                ball.currentWaypointIndex = ball.waypoints.Length - 1;
            ball.transform.position = ball.waypoints[_newBall.currentWaypointIndex].position;
        }
    }
    public IEnumerator MoveBallAfterReverse()
    {
        if (ballList.Count > 0 && ballList.Count <= maxBall)
        {
            for (int i = 1; i < ballList.Count-1; i++)
            {
                if (ballList[i].transform.position == MoveBall.Instance.waypoints[0].transform.position && ballList[i - 1].isReverse)
                {
                    ballList[i].isReload = true;
                }
                if (Vector3.Distance(ballList[i - 1].transform.position, MoveBall.Instance.waypoints[1].transform.position)<=0.2 && !ballList[i].isReverse)
                {
                    if (ballList[i].isReload)
                    {
                        ballList[i].isReload = false;
                        ballList[i].speed = 1.5f;
                        yield return new WaitForSeconds(0.45f);
                        ballList[i].speed = 0.9f;
                    }
                }
                if (ballList[i].isReload)
                {
                    ballList[i + 1].isReload = true;
                }
            }
        }
    }
}
