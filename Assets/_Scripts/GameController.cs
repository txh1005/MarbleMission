using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public List<GameObject> destroyBallList;
    public int nextBallIndex;
    public int behindBallIndex;
    public SpawnBall spawnBall;
    public int count = 1;
    public List<TestBall> destroyList;
    public bool isDestroy;
    public float rad = 0.48f;
    public bool isInsert;
    public int reverseIndex = 0;
    public bool isInsertStop;
    public int indexColi;
    #region old
    public void StopBallList(int index)
    {
        for (int i = 0; i < index; i++)
        {
            BallListSpawn2.Instance.ballList[i].isMove = false;
            BallListSpawn2.Instance.ballList[i].isReverse = false;
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
            if (BallListSpawn2.Instance.ballList[nextBallIndex].color == BallListSpawn2.Instance.ballList[nextBallIndex + 1].color)
            {
                for (int i = behindBallIndex; i >= nextBallIndex; i--)
                {
                    Destroy(BallListSpawn2.Instance.ballList[i].gameObject);
                    BallListSpawn2.Instance.ballList.RemoveAt(i);
                }
            }
            else
            {
                for (int i = behindBallIndex; i > nextBallIndex; i--)
                {
                    Destroy(BallListSpawn2.Instance.ballList[i].gameObject);
                    BallListSpawn2.Instance.ballList.RemoveAt(i);
                }
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
    public void GameOver()
    {
        Ball2 ball = BallListSpawn2.Instance.ballList[0];
        ball.speed = ball.moveSpeed * 5;
        Destroy(BallListSpawn2.Instance.ballList[0].gameObject);
        BallListSpawn2.Instance.ballList.RemoveAt(0);
    }
    #endregion

    public void InsertInBallList(int index, TestBall ballShoot)
    {
        LinkedListNode<TestBall> newNode = new LinkedListNode<TestBall>(ballShoot);
        if (index == 0)
        {
            LinkedListNode<TestBall> currentNode = spawnBall.ballList.First;
            spawnBall.ballList.AddFirst(newNode);
            if (spawnBall.ballList.Count > 1)
            {
                /*newNode.Value.Next = spawnBall.ballList.First.Value.Next;
                spawnBall.ballList.First.Value.Next.PcurrentNoderev = newNode.Value;*/
                newNode.Value.Prev = currentNode.Value;
                currentNode.Value.Next = newNode.Value;
            }
            ballShoot.isReverse = false;
            GetBallInList(index).isReverse = false;
        }
        else if (index == spawnBall.ballList.Count - 1)
        {
            LinkedListNode<TestBall> currentNode = spawnBall.ballList.Last;
            spawnBall.ballList.AddLast(newNode);
            if (spawnBall.ballList.Count > 1)
            {
                newNode.Value.Next = currentNode.Value;
                currentNode.Value.Prev = newNode.Value;
            }
        }
        else
        {
            LinkedListNode<TestBall> currentNode = spawnBall.ballList.First;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
            if (currentNode.Value.Next == null || currentNode.Value.Prev == null)
            {
                return;
            }
            spawnBall.ballList.AddAfter(currentNode, newNode);
            newNode.Value.Next = currentNode.Value;
            newNode.Value.Prev = currentNode.Value.Prev;
            currentNode.Value.Prev.Next = newNode.Value;
            currentNode.Value.Prev = newNode.Value;
        }
    }
    public void InsertInList(int index, int fIndex)
    {
        List<TestBall> BallInRange = GetBallsInRange(fIndex, index);
        foreach (TestBall ball in BallInRange)
        {
            if (ball.isMove)
            {
                ball.distanceTravel += 3f * Time.fixedDeltaTime;
                ball.distanceTravel = Mathf.Min(ball.distanceTravel, ball.distanceTravel + 0.5f);
            }
        }
    }
    public void InsertInList1(int index, int fIndex)
    {
        List<TestBall> BallInRange = GetBallsInRange(fIndex, index);
        foreach (TestBall ball in BallInRange)
        {
            ball.distanceTravel += Time.fixedDeltaTime;
            ball.distanceTravel = Mathf.Min(ball.distanceTravel, ball.distanceTravel);
        }
    }
    public void RestoreSpeed(int index)
    {
        List<TestBall> BallInRange = GetBallsInRange(0, index);
        foreach (TestBall ball in BallInRange)
        {
            ball.speed = 0.9f;
        }
    }
    public List<TestBall> GetBallsInRange(int startIndex, int endIndex)
    {
        List<TestBall> ballsInRange = new List<TestBall>();
        int currentIndex = 0;
        foreach (TestBall ball in spawnBall.ballList)
        {
            if (currentIndex >= startIndex && currentIndex <= endIndex)
            {
                ballsInRange.Add(ball);
            }
            if (currentIndex > endIndex)
            {
                break;
            }
            currentIndex++;
        }
        return ballsInRange;
    }
    public int GetIndexInList(TestBall targetBall)
    {
        int index = 0;
        foreach (TestBall ball in spawnBall.ballList)
        {
            if (ball == targetBall)
            {
                return index;
            }
            index++;
        }
        return -1;
    }
    public TestBall GetBallInList(int index)
    {
        LinkedListNode<TestBall> currentNode = spawnBall.ballList.First;
        if (index < spawnBall.ballList.Count)
        {
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
        }
        return currentNode.Value;
    }
    public void SameColorNext(int index)
    {
        for (int i = index; i > 0; i--)
        {
            if (i > 0)
            {
                nextBallIndex = i - 1;
                if (GetBallInList(i).color == GetBallInList(i - 1).color)
                {
                    GetBallInList(i - 1).nextDestroy = true;
                    count++;
                }
                else
                {
                    return;
                }
            }
        }
        GetBallInList(1).nextDestroy = true;
    }
    public void SameColorPrev(int index)
    {
        for (int i = index; i < spawnBall.ballList.Count; i++)
        {
            behindBallIndex = i + 1;
            if (GetBallInList(i).color == GetBallInList(i + 1).color)
            {
                GetBallInList(i + 1).nextDestroy = true;
                count++;
            }
            else
            {
                return;
            }
        }
        GetBallInList(spawnBall.ballList.Count - 1).nextDestroy = true;
    }
    public void SameColorNext1(int index)
    {
        for (int i = index; i > 0; i--)
        {
            Strategy.Instance.nextIndex = i - 1;
            if (GetBallInList(i).color == GetBallInList(i - 1).color)
            {
                GetBallInList(i - 1).nextDestroy = true;
                count++;
            }
            else
            {
                return;
            }
        }
    }
    public void RemoveAt(int index)
    {
        if (index >= 1 && index < spawnBall.ballList.Count - 1)
        {
            LinkedListNode<TestBall> currentNode = spawnBall.ballList.First;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
            GetBallInList(index - 1).Prev = currentNode.Value.Prev;
            GetBallInList(index + 1).Next = currentNode.Value.Next;
            Destroy(currentNode.Value.gameObject);
            spawnBall.ballList.Remove(currentNode.Value);
        }
        if (index == 0)
        {
            LinkedListNode<TestBall> currentNode = spawnBall.ballList.First;
            Destroy(currentNode.Value.gameObject);
            spawnBall.ballList.Remove(currentNode.Value);
        }
    }
    public void DestroySameBall(int index)
    {
        if (destroyList.Count > 0)
        {
            destroyList.Clear();
        }
        SameColorNext(index);
        SameColorPrev(index);
        if (count >= 3)
        {
            //isDestroy = true;
            for (int i = 0; i < count; i++)
            {
                RemoveAt(nextBallIndex + 1);
            }
        }
    }
    public int DestroyIndex()
    {
        int minIndex = -1;

        for (int i = 0; i < spawnBall.ballList.Count; i++)
        {
            TestBall ball = GetBallInList(i);
            if (ball.nextDestroy)
            {
                minIndex = i;
                break;
            }
        }
        return minIndex;
    }
    public void DestroySameBall1(int index)
    {
        SameColorNext(index);
        SameColorPrev(index);

        if (count >= 3 && count >= spawnBall.ballList.Count)
        {
            for (int i = 0; i <= count + 2; i++)
            {
                Destroy(GetBallInList(0).gameObject);
                spawnBall.ballList.RemoveFirst();
            }
        }
        if (count >= 3)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveAt(DestroyIndex());
            }
        }
        else
        {
            for (int i = 0; i < spawnBall.ballList.Count; i++)
            {
                GetBallInList(i).nextDestroy = false;
            }
        }
    }
    public void DestroyBallReverse()
    {
        count = 1;
        SameColorNext1(nextBallIndex);
        /*for (int i = nextBallIndex; i > 0; i--)
        {
            if (GetBallInList(i).color == GetBallInList(i - 1).color)
            {
                GetBallInList(i - 1).nextDestroy = true;
                count++;
            }
            else
            {
                return;
            }
        }*/
        SameColorPrev(nextBallIndex);
        if (count > 3 && count >= spawnBall.ballList.Count)
        {
            for (int i = 0; i <= count + 2; i++)
            {
                Destroy(GetBallInList(0).gameObject);
                spawnBall.ballList.RemoveFirst();
            }
        }
        if (count >= 3)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveAt(Strategy.Instance.nextIndex + 1);
            }
            if (GetBallInList(Strategy.Instance.nextIndex).color != GetBallInList(Strategy.Instance.nextIndex + 1).color)
            {
                isDestroy = true;
                Strategy.Instance.StopListBall(Strategy.Instance.nextIndex);
            }
            else
            {
                count = 1;
                for (int i = 0; i <= Strategy.Instance.nextIndex; i++)
                {
                    GetBallInList(i).isReverse = true;
                    GetBallInList(i).isMove = true;
                }
            }
        }
        else
        {
            for (int i = 0; i <= nextBallIndex; i++)
            {
                GetBallInList(i).isMove = true;
                GetBallInList(i).isReverse = false;
            }
            GetBallInList(nextBallIndex + 1).nextDestroy = false;
        }
    }
}