using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Spine.Unity;
using Sirenix.OdinInspector;

public class TestBall : MonoBehaviour
{
    public Rigidbody2D rigid;
    public PathCreator path;
    public float speed = 0.9f;
    public float distanceTravel;
    public BallColor1 color;
    public BallData2 ballData;
    public SkeletonAnimation skeletonAnimation;
    public bool isMove;
    public SpawnBall spawnBall;
    [ShowInInspector] public TestBall Next { get; set; }
    [ShowInInspector] public TestBall Prev { get; set; }

    private void Start()
    {
        SetColor();
        path = FindObjectOfType<PathCreator>();
        spawnBall = FindObjectOfType<SpawnBall>();
    }
    void Update()
    {
        Move();
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
    public TestBall Head
    {
        get
        {
            TestBall ball = this;
            if (ball.Prev == null)
            {
                return ball;
            }
            return ball.Next;
        }
    }
    public TestBall Tail
    {
        get
        {
            TestBall ball = this;
            do
            {
                if (ball.Next = null)
                {
                    return ball;
                }
            } while (true);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("end"))
        {
            Destroy(gameObject);
        }
    }
}
