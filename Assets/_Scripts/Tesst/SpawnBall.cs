using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public TestBall ballPre;
    public BallDataSO ballData2;
    public int ballIndex = 0;
    //public List<TestBall> ballList;
    public int maxBall;
    [ShowInInspector] public LinkedList<TestBall> ballList;

    void Start()
    {
        maxBall = ballData2.count;
        StartCoroutine(Spawn());
        ballList = new LinkedList<TestBall>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Spawn()
    {
        while (ballIndex < maxBall)
        {
            yield return new WaitForSeconds(0.55f);//0.45/speed=0.5f
            TestBall newBall = Instantiate(ballPre, transform.position, Quaternion.identity);
            Vector3 a = newBall.transform.position;
            a.z = 0f;
            newBall.isMove = true;
            newBall.ballData = ballData2.ballDatas[ballIndex];
            //ballScript.SetBallData(ballPrefab.GetComponent<Ball2>());
            ballList.AddLast(newBall);
            LinkedListNode<TestBall> nodeF = ballList.First;
            LinkedListNode<TestBall> nodeL = ballList.Last;
            if (ballIndex == 0)
            {
                newBall.Next = null;
                newBall.Prev = null;
            }
            if (ballIndex > 0)
            {
                for (int i = 1; i < ballIndex; i++)
                {
                    nodeF = nodeF.Next;
                }
                newBall.Next = nodeF.Value;
                newBall.Prev = null;
                newBall.Next.Prev = newBall;
            }
            ballIndex++;
        }
    }
}
