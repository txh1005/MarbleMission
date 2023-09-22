using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallListSpawn2 : Singleton<BallListSpawn2>
{
    public Ball2 ballPrefab;
    //public BallData[] ballData1;
    public BallDataSO ballData2;
    public PathSO pathSO;
    public List<Ball2> ballList;
    public int maxBall;
    public Transform storeObj;
    public Coroutine spawnCoroutine;
    int ballIndex = 0;

    private bool isSpawnRun;
    void Start()
    {
        spawnCoroutine = StartCoroutine(Spawn());
    }
    private void Update()
    {
        StartCoroutine(MoveBallAfterReverse());
        if (Input.GetKeyDown(KeyCode.Space) && !isSpawnRun)
        {
            spawnCoroutine = StartCoroutine(Spawn());
        }
    }
    /*public IEnumerator Spawn()
    {
        isSpawnRun = true;
        for (int i = 0; i < maxBall; i++)
        {
            yield return new WaitForSeconds(0.55f);//0.45/speed=0.5f
            Ball2 newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Vector3 a = newBall.transform.position;
            a.z = 0f;
            newBall.isMove = true;
            newBall.ballData = ballData2.ballDatas[i];
            //ballScript.SetBallData(ballPrefab.GetComponent<Ball2>());
            ballList.Add(newBall);
            newBall.transform.SetParent(storeObj);
        }
        isSpawnRun = false;
    }*/
    public IEnumerator Spawn()
    {
        isSpawnRun = true;
        while(ballIndex < maxBall){
            yield return new WaitForSeconds(0.55f);//0.45/speed=0.5f
            Ball2 newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Vector3 a = newBall.transform.position;
            a.z = 0f;
            newBall.isMove = true;
            newBall.ballData = ballData2.ballDatas[ballIndex];
            //ballScript.SetBallData(ballPrefab.GetComponent<Ball2>());
            ballList.Add(newBall);
            newBall.transform.SetParent(storeObj);
            ballIndex++;
        }
        isSpawnRun = false;
    }
    public IEnumerator ContinueSpawn()
    {
        isSpawnRun = true;
        while (ballIndex < maxBall)
        {
            yield return new WaitForSeconds(0.55f);//0.45/speed=0.5f
            Ball2 newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Vector3 a = newBall.transform.position;
            a.z = 0f;
            newBall.isMove = true;
            newBall.ballData = ballData2.ballDatas[ballIndex];
            //ballScript.SetBallData(ballPrefab.GetComponent<Ball2>());
            ballList.Add(newBall);
            newBall.transform.SetParent(storeObj);
            ballIndex++;
        }
        isSpawnRun = false;
    }
    /*public void InsertBall(Ball2 _newBall, int _index)
    {
        for (int i = 0; i < _index; i++)
        {
            Ball2 ball = ballList[i];
            _newBall.currentWaypointIndex = ball.currentWaypointIndex;
            _newBall.currentWaypointIndex += 2;
            if (ball.currentWaypointIndex >= ball.waypoints.Length)
                ball.currentWaypointIndex = ball.waypoints.Length - 1;
            ball.transform.position = ball.waypoints[_newBall.currentWaypointIndex].point;
        }
    }*/
    public IEnumerator MoveBallAfterReverse()
    {
        if (ballList.Count > 0 && ballList.Count <= maxBall)
        {
            for (int i = 1; i < ballList.Count - 1; i++)
            {
                if (ballList[i].transform.position == pathSO.pathDatas[0].point && ballList[i - 1].isReverse)
                {
                    ballList[i].isReload = true;
                }
                if (Vector3.Distance(ballList[i - 1].transform.position, pathSO.pathDatas[1].point) <= 0.2 && !ballList[i].isReverse)
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
