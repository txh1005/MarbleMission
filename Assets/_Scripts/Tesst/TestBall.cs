using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Spine.Unity;
using Sirenix.OdinInspector;
using System;

public class TestBall : MonoBehaviour
{
    [ShowInInspector] public TestBall Next { get; set; }
    [ShowInInspector] public TestBall Prev { get; set; }
    public Rigidbody2D rigid;
    public PathCreator path;
    public float speed = 0.9f;
    public float distanceTravel;
    public BallColor1 color;
    public BallData2 ballData;
    public SkeletonAnimation skeletonAnimation;
    public bool isMove;
    public SpawnBall spawnBall;
    public bool isFire;
    public float ballSpeed;
    public bool isBall;
    public bool nextDestroy = false;
    public bool isReverse;

    bool isBallAdd = false;
    bool isColi = false;
    bool hasColision = false;

    private void Start()
    {
        SetColor();
        path = FindObjectOfType<PathCreator>();
        spawnBall = FindObjectOfType<SpawnBall>();
    }
    void Update()
    {
        if (!isReverse && !isBall)
        {
            Move();
        }
        if (isReverse && !isBall)
        {
            Reverse();
        }
        if (isBallAdd)
        {
            if (GameController.Instance.GetIndexInList(this) < spawnBall.ballList.Count - 1)
            {
                TestBall ball = GameController.Instance.GetBallInList(GameController.Instance.GetIndexInList(this) + 1);
                Vector3 ball1 = path.path.GetPointAtDistance(ball.distanceTravel + GameController.Instance.rad);
                transform.position = Vector3.MoveTowards(transform.position, ball1, 5f * Time.deltaTime);
                distanceTravel = ball.distanceTravel + GameController.Instance.rad;
                if (transform.position == ball1)
                {
                    isMove = true;
                    isBallAdd = false;
                }
                /*if (transform.position == ball1)
                {
                    isMove = true;
                    isBallAdd = false;
                    TestBall nextBall = GameController.Instance.GetBallInList(GameController.Instance.GetIndexInList(this) - 1);
                    if (nextBall.distanceTravel - this.distanceTravel <= GameController.Instance.rad)
                    {
                        for (int i = 0; i <= GameController.Instance.nextBallIndex; i++)
                        {
                            GameController.Instance.GetBallInList(i).isMove = false;
                        }
                    }
                }*/
            }
            else if (GameController.Instance.GetIndexInList(this) == spawnBall.ballList.Count - 1)
            {
                TestBall ball = GameController.Instance.GetBallInList(spawnBall.ballList.Count - 2);
                Vector3 ball1 = path.path.GetPointAtDistance(ball.distanceTravel);
                transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, 5f * Time.deltaTime);
                distanceTravel = ball.distanceTravel;
                if (transform.position == ball.transform.position)
                {
                    isMove = true;
                    isBallAdd = false;
                }
            }
            else
            {
                TestBall ball = GameController.Instance.GetBallInList(GameController.Instance.GetIndexInList(this));
                Vector3 ball1 = path.path.GetPointAtDistance(ball.distanceTravel + GameController.Instance.rad);
                transform.position = Vector3.MoveTowards(transform.position, ball1, 5f * Time.deltaTime);
                distanceTravel = ball.distanceTravel + GameController.Instance.rad;
                if (transform.position == ball1)
                {
                    isMove = true;
                    isBallAdd = false;
                }
            }
        }
        Strategy.Instance.CheckBall(this);
    }
    private void FixedUpdate()
    {
        if (hasColision)
        {
            TestBall nextBall = GameController.Instance.GetBallInList(GameController.Instance.GetIndexInList(this) - 1);
            TestBall prevBall = GameController.Instance.GetBallInList(GameController.Instance.GetIndexInList(this) + 1);
            if (GameController.Instance.GetIndexInList(this) <= spawnBall.ballList.Count - 1)
            {
                if (nextBall.distanceTravel - this.distanceTravel <= GameController.Instance.rad)
                {
                    GameController.Instance.InsertInList(GameController.Instance.GetIndexInList(this) - 1, 0);
                }
            }
            /*else
            {
                TestBall nextBall1 = GameController.Instance.GetBallInList(GameController.Instance.GetIndexInList(this));
                if (nextBall.distanceTravel - nextBall1.distanceTravel <= GameController.Instance.rad)
                {
                    GameController.Instance.InsertInList(GameController.Instance.GetIndexInList(this), 0);
                }
            }*/
        }
        if (Strategy.Instance.isBallStop)
        {
            TestBall nextBall = GameController.Instance.GetBallInList(GameController.Instance.GetIndexInList(this) - 1);
            if (GameController.Instance.GetIndexInList(this) < spawnBall.ballList.Count - 1)
            {
                if (nextBall.distanceTravel - this.distanceTravel <= GameController.Instance.rad)
                {
                    foreach (TestBall ball in GameController.Instance.GetBallsInRange(0, GameController.Instance.nextBallIndex + 1))
                    {
                        ball.isMove = false;
                    }
                    GameController.Instance.InsertInList(GameController.Instance.GetIndexInList(this) - 1, 0);
                }
                if (GameController.Instance.GetBallInList(GameController.Instance.indexColi).distanceTravel - GameController.Instance.GetBallInList(GameController.Instance.indexColi + 2).distanceTravel > 0.47f)
                {
                    StartCoroutine(Strategy.Instance.InsertBallStop(GameController.Instance.nextBallIndex + 1));
                }
            }
            //Strategy.Instance.isBallStop = false;
        }
    }
    public void LaunchBall()
    {
        if (!isFire)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //rigid.AddForce((mousePosition - direction) * ballSpeed, ForceMode2D.Impulse);
            Vector2 shotDir = (mousePosition - transform.position);
            rigid.velocity = shotDir.normalized * ballSpeed;
        }
        isFire = true;
    }
    public void Move()
    {
        if (isMove)
        {
            distanceTravel += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravel);
            Quaternion a = path.path.GetRotationAtDistance(distanceTravel);
            transform.rotation = new Quaternion(-a.x, -a.y, 0, 0);
        }
    }
    public void Reverse()
    {
        if (isMove)
        {
            distanceTravel -= 3 * speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravel);
            Quaternion a = path.path.GetRotationAtDistance(distanceTravel);
            transform.rotation = new Quaternion(a.x, a.y, 0, 0);
        }
    }
    public void SetColor()
    {
        color = ballData.color1;
        switch (color)
        {
            case BallColor1.Blue:
                skeletonAnimation.Skeleton.SetSkin("Blue");
                break;
            case BallColor1.Green:
                skeletonAnimation.Skeleton.SetSkin("Green");
                break;
            case BallColor1.Red:
                skeletonAnimation.Skeleton.SetSkin("Red");
                break;
            case BallColor1.Violet:
                skeletonAnimation.Skeleton.SetSkin("Violet");
                break;
            case BallColor1.Yellow:
                skeletonAnimation.Skeleton.SetSkin("Yellow");
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TestBall ball = collision.gameObject.GetComponent<TestBall>();
        if (collision.CompareTag("ball") && isBall && !isColi)
        {
            if (spawnBall.ballIndex < spawnBall.maxBall)
            {
                spawnBall.ballIndex++;
            }
            hasColision = true;
            isColi = true;
            this.isMove = false;
            this.isBallAdd = true;
            this.isBall = false;
            //this.distanceTravel = ball.distanceTravel;
            this.rigid.velocity = Vector2.zero;
            this.nextDestroy = true;
            int index = GameController.Instance.GetIndexInList(ball);
            GameController.Instance.indexColi = index;

            //GameController.Instance.InsertInList(index);
            GameController.Instance.count = 1;
            GameController.Instance.InsertInBallList(index, this);
            GameController.Instance.destroyList.Add(this);
            GameController.Instance.isDestroy = true;
            StartCoroutine(Strategy.Instance.AfterInsert(index));
            if (isFire && !isMove && !ball.isMove)
            {
                Strategy.Instance.isBallStop = true;
            }

            /*if (ball.isMove)
            {
                GameController.Instance.isInsertStop = false;
                StartCoroutine(Strategy.Instance.AfterInsert(index));
            }
            else
            {
                GameController.Instance.isInsertStop = false;
                StartCoroutine(Strategy.Instance.AfterInsert(index));
            }*/
            /*int stopIndex = GameController.Instance.DestroyIndex();
            GameController.Instance.DestroySameBall1(index + 1);
            GameController.Instance.isDestroy = true;
            Strategy.Instance.StopListBall(GameController.Instance.nextBallIndex);
            Strategy.Instance.ReverseBall();*/
        }

        if (collision.CompareTag("ball") && !isBall && isReverse && !ball.isReverse)
        {
            Debug.Log("re");
            GameController.Instance.DestroyBallReverse();
            GameController.Instance.nextBallIndex = Strategy.Instance.nextIndex;
        }
        if (collision.CompareTag("end") && !isBall)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TestBall coliObj = collision.gameObject.GetComponent<TestBall>();
        if (collision.gameObject.CompareTag("ball") && !isBall)
        {
            if (coliObj.isMove && !isMove)
            {
                Strategy.Instance.isBallStop = false;
                isMove = true;
                isReverse = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("destroy") && isBall)
        {
            Destroy(gameObject);
        }
    }
}