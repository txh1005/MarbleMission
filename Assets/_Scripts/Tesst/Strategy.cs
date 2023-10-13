using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strategy : Singleton<Strategy>
{
    public SpawnBall spawnBall;
    public int nextIndex = 0;
    public bool isBallStop;

    public void StopListBall(int index)
    {
        if (GameController.Instance.isDestroy && GameController.Instance.count >= 3)
        {
            for (int i = 0; i <= index; i++)
            {
                GameController.Instance.GetBallInList(i).isMove = false;
                GameController.Instance.GetBallInList(i).isReverse = false;
            }
            //GameController.Instance.isDestroy = false;
        }
    }
    public void ReverseBall(int index)
    {
        if (index == 0)
        {
            GameController.Instance.GetBallInList(index).isReverse = false;
            GameController.Instance.GetBallInList(index).isMove = true;
        }
        if (index != 0 && GameController.Instance.GetBallInList(GameController.Instance.nextBallIndex).color == GameController.Instance.GetBallInList(GameController.Instance.nextBallIndex + 1).color)
        {
            for (int i = 0; i < GameController.Instance.nextBallIndex + 1; i++)
            {
                GameController.Instance.GetBallInList(i).isReverse = true;
                GameController.Instance.GetBallInList(i).isMove = true;
            }
        }
    }
    public void CheckBall(TestBall ball)
    {
        if (GameController.Instance.nextBallIndex > 0 && GameController.Instance.GetBallInList(GameController.Instance.nextBallIndex).distanceTravel - GameController.Instance.GetBallInList(GameController.Instance.nextBallIndex + 1).distanceTravel <= GameController.Instance.rad)
        {
            ball.isMove = true;
            //ball.isReverse = false;
        }
    }
    public IEnumerator AfterInsert(int index)
    {
        yield return new WaitForSeconds(0.2f);
        int stopIndex = GameController.Instance.DestroyIndex();
        GameController.Instance.DestroySameBall1(index + 1);
        StopListBall(GameController.Instance.nextBallIndex);
        ReverseBall(index);
    }
    public IEnumerator InsertBallStop(int index)
    {
        yield return new WaitForSeconds(0.37f);
        for (int i = index; i >= 0; i--)
        {
            GameController.Instance.GetBallInList(i).isMove = false;
        }
    }
}