using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public List<GameObject> destroyBallList;
    public int nextBallIndex;
    public int behindBallIndex;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void StopBallList(int index)
    {
        for (int i = 0; i < index; i++)
        {
            BallListSpawn2.Instance.ballList[i].isMove = false;
            //BallListSpawn2.Instance.ballList[i].skeletonAnimation.AnimationState.SetAnimation(0, "Run", false);
        }

        /*if (BallListSpawn2.Instance.ballList.Count >= 0)
            BallListSpawn2.Instance.ballList[^1].isMove = true;*/
    }
    public void MoveBallList(int index)
    {
        for (int i = 0; i < index; i++)
        {
            BallListSpawn2.Instance.ballList[i].isMove = true;
            BowShoot2.Instance.mainBall.isMove = true;
        }
    }
    public void ClearList()
    {
        destroyBallList.Clear();
        //destroyBallList.RemoveAll(ball=>ball == null);
    }  
    public void RemoveSameBall()
    {
        /*if (destroyBallList!=null)
        {
            foreach (GameObject ball in destroyBallList)
            {
                Destroy(ball);
            }
        }*/
        if (behindBallIndex == BallListSpawn2.Instance.ballList.Count - 1 && BallListSpawn2.Instance.ballList[^1].color == BallListSpawn2.Instance.ballList[^2].color)
        {
            for (int i = behindBallIndex; i > nextBallIndex; i--)
            {
                Destroy(BallListSpawn2.Instance.ballList[i].gameObject);
                BallListSpawn2.Instance.ballList.RemoveAt(i);
            }
            for (int i = BallListSpawn2.Instance.ballList.Count - 1; i >= 0; i--)
            {
                BallListSpawn2.Instance.ballList[i].speed = 0.9f;
            }
        }
        else if (nextBallIndex == 0 && BallListSpawn2.Instance.ballList[0].color == BallListSpawn2.Instance.ballList[1].color)
        {
            for (int i = behindBallIndex - 1; i >= nextBallIndex; i--)
            {
                Destroy(BallListSpawn2.Instance.ballList[i].gameObject);
                BallListSpawn2.Instance.ballList.RemoveAt(i);
            }
        }
        else
        {
            for (int i = behindBallIndex - 1; i > nextBallIndex; i--)
            {
                Destroy(BallListSpawn2.Instance.ballList[i].gameObject);
                BallListSpawn2.Instance.ballList.RemoveAt(i);
            }
        }

        //ClearList();
    }
}
