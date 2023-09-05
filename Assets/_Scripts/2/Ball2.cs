using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Spine.Unity;
using Spine;

public class Ball2 : MonoBehaviour
{
    public Rigidbody2D rigid;

    public TypeBall type;
    public BallItem item;
    public BallColor color;
    public int id;
    public BallData ballData;
    public float ballSpeed = 10f;
    public bool isBall;
    public Transform[] waypoints;
    public float speed = 0.9f;
    public float stoppingDistance = 0.2f;
    public int currentWaypointIndex = 0;
    public float step;
    public bool isMove;
    public bool isReverse;
    public Ball2 nextBall;
    public Ball2 behindBall;
    public int ballIndex;
    public string skinName;
    public SkeletonAnimation skeletonAnimation;
    public bool isReload = false;
    public bool isBallAdd = false;
    public bool isBallMove = false;

    private SpriteRenderer sptRenColor;
    private bool isFire;
    private bool hasCollided = false;
    private Vector3 initialPosition;
    int count = 1;
    Vector3 targetPosition;
    Vector3 moveDirection;
    float distanceToTarget;
    int a = 0;
    float timeToReverse;
    float moveSpeed = 0.9f;

    private void Awake()
    {

    }
    private void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.OnGameOver -= OnGameOver;
    }
    void OnGameOver(string a)
    {
        Debug.Log("ball: " + a);
    }
    //test
    // Start is called before the first frame update
    void Start()
    {
        SetColor();
        SetId();
        SetType();
        SetItem();
        waypoints = MoveBall.Instance.waypoints;
        speed = moveSpeed;
        isFire = false;
        isReverse = false;
        sptRenColor = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!isBall && isMove)
        {
            if (!isReverse && !isReload)
            {
                Move();
            }
            else
                ReverseMove();
            RotateBall();
        }
        if (isBallAdd)
        {
            MoveAddBall();
        }

    }
    public void SetId()
    {
        id = BallListSpawn2.Instance.ballList.Count - 1;
    }
    public void SetType()
    {
        type = ballData.type;
        switch (type)
        {
            case TypeBall.Normal:
                break;
            case TypeBall.Item:
                break;
            default:
                break;
        }
    }
    public void SetItem()
    {
        item = ballData.item;
        switch (item)
        {
            case BallItem.Reverse:
                break;
            case BallItem.Stop:
                break;
            case BallItem.Slow:
                break;
            default:
                break;
        }
    }
    public void SetColor()
    {
        color = ballData.color;
        switch (color)
        {
            case BallColor.Blue:
                skeletonAnimation.Skeleton.SetSkin("Blue");
                break;
            case BallColor.Green:
                skeletonAnimation.Skeleton.SetSkin("Green");
                break;
            case BallColor.Red:
                skeletonAnimation.Skeleton.SetSkin("Red");
                break;
            case BallColor.Violet:
                skeletonAnimation.Skeleton.SetSkin("Violet");
                break;
            case BallColor.Yellow:
                skeletonAnimation.Skeleton.SetSkin("Yellow");
                break;
            default:
                break;
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
        if (!isBall && isMove && !isReverse)
        {
            if (waypoints.Length == 0)
                return;

            float tPosition = Vector3.Distance(waypoints[currentWaypointIndex + 1].position, waypoints[currentWaypointIndex].position);
            float mDirection = Vector3.Distance(waypoints[currentWaypointIndex + 1].position, transform.position);

            float distanceToTarget = mDirection - tPosition;
            if (distanceToTarget <= stoppingDistance)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                    currentWaypointIndex = waypoints.Length - 1;
            }
            targetPosition = waypoints[currentWaypointIndex].position;
            moveDirection.Normalize();
            //transform.position += step / distanceToTarget * moveDirection * speed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, transform.position + step / distanceToTarget * moveDirection, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            //BallListSpawn2.Instance.ballList[i].transform.position=Vector3.MoveTowards(BallListSpawn2.Instance.ballList[i].transform.position, targetPosition, speed*Time.deltaTime);

            //BallListSpawn2.Instance.ballList.RemoveAll(ball => ball == null);
            if (BallListSpawn2.Instance.ballList.Count > 0)
            {
                BallListSpawn2.Instance.ballList[^1].isMove = true;
            }
            /*ballIndex = BallListSpawn2.Instance.ballList.IndexOf(this);
            if (ballIndex > 0)
            {
                behindBall = BallListSpawn2.Instance.ballList[ballIndex - 1];
            }
            if (ballIndex < BallListSpawn2.Instance.ballList.Count - 1)
            {
                nextBall = BallListSpawn2.Instance.ballList[ballIndex + 1];
            }*/
        }
    }
    public void MoveAddBall()
    {
        transform.position = Vector3.MoveTowards(transform.position, PosBallAdd(a + 1), speed * 2.4f * Time.deltaTime);
        if (transform.position == PosBallAdd(a + 1))
        {
            isBallAdd = false;
        }
        //transform.position = PosBallAdd(a);
    }
    private void RotateBall()
    {
        Vector3 moveDirection = waypoints[currentWaypointIndex].position - transform.position;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed);
        //transform.rotation = targetRotation;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("destroy"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Ball2 coliObj = collision.gameObject.GetComponent<Ball2>();
        if (collision.gameObject.CompareTag("ball") && !isBall)
        {
            if (coliObj.isMove && !isMove)
            {
                isMove = true;
                speed = moveSpeed;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball2 coliObj = collision.gameObject.GetComponent<Ball2>();
        if (collision.gameObject.CompareTag("ball") && isBall && !hasCollided)
        {
            Vector3 collisionPosition = collision.transform.position;
            isBall = false;

            BowShoot2.Instance.mainBall.GetComponent<SpriteRenderer>().sortingOrder = 0;
            rigid.velocity = Vector2.zero;
            int index = BallListSpawn2.Instance.ballList.IndexOf(coliObj);
            hasCollided = true;
            isMove = false;
            a = index;
            isBallAdd = true;
            //StartCoroutine(WaitBall());
            BallListSpawn2.Instance.ballList.Insert(index, BowShoot2.Instance.mainBall);
            StartCoroutine(InsertBall1(index));
            currentWaypointIndex = BallListSpawn2.Instance.ballList[index + 1].currentWaypointIndex;
            //MoveAddBall();
            if (index < BallListSpawn2.Instance.ballList.Count && index >= 0)
            {
                StartCoroutine(DestroySameBall(index));
            }
            //BallListSpawn2.Instance.InsertBall(BowShoot2.Instance.mainBall,index); 
        }
        if (isReverse && !coliObj.isReverse)
        {
            for (int i = 0; i < BallListSpawn2.Instance.ballList.Count; i++)
            {
                BallListSpawn2.Instance.ballList[i].isMove = false;
                BallListSpawn2.Instance.ballList[i].isReverse = true;
            }
            for (int i = 0; i < BallListSpawn2.Instance.ballList.Count; i++)
            {
                BallListSpawn2.Instance.ballList[i].isMove = true;
                timeToReverse = 1f;
                StartCoroutine(StopReverse());
            }
            SameColorNext1(GameController.Instance.nextBallIndex);
            SameColorBehind1(GameController.Instance.nextBallIndex + 1);
            if (count < 3)
            {
                BallListSpawn2.Instance.ballList[0].isReverse = true;
                GameController.Instance.ClearList();
            }
            else
            {
                BallListSpawn2.Instance.ballList[0].isReverse = false;
                GameController.Instance.RemoveSameBall();
                if (GameController.Instance.nextBallIndex < BallListSpawn2.Instance.ballList.Count - 1)
                {
                    if (BallListSpawn2.Instance.ballList[GameController.Instance.nextBallIndex + 1].color == BallListSpawn2.Instance.ballList[GameController.Instance.nextBallIndex].color)
                    {
                        for (int i = GameController.Instance.nextBallIndex; i >= 0; i--)
                        {
                            BallListSpawn2.Instance.ballList[i].isReverse = true;
                        }
                    }
                    else
                    {
                        GameController.Instance.StopBallList(GameController.Instance.nextBallIndex + 1);
                        for (int i = GameController.Instance.nextBallIndex; i < BallListSpawn2.Instance.ballList.Count; i++)
                        {
                            BallListSpawn2.Instance.ballList[i].isReverse = false;
                        }
                    }
                }
            }
            return;
        }
        if (!isMove && !isBall)
        {
            int index = BallListSpawn2.Instance.ballList.IndexOf(coliObj);
            
            if (coliObj.isBallAdd)
            {
                coliObj.isMove = false;
                //StartCoroutine(InsertBall1(index));
            }
            //ReverseMove(index);
            else
            {
                isMove = true;
                speed = moveSpeed;
            }
        }
        if (collision.gameObject.CompareTag("end") && !isBall)
        {
            Destroy(gameObject);
            BallListSpawn2.Instance.ballList.RemoveAt(0);
        }
    }
    /*void InsertBall(int index)
    {
        for (int i = 0; i <= index; i++)
        {
            Ball2 ball = BallListSpawn2.Instance.ballList[i];
            int currentWaypointIndex = ball.currentWaypointIndex;
            float distanceToTarget = BallListSpawn2.Instance.ballList[i].distanceToTarget;
            Vector3 targetPosition = waypoints[currentWaypointIndex + 1].position;
            Vector3 moveDirection = targetPosition - ball.transform.position;
            moveDirection.Normalize();
            if (ball.currentWaypointIndex >= ball.waypoints.Length)
                ball.currentWaypointIndex = ball.waypoints.Length - 1;
            ball.transform.position = targetPosition;
        }
    }*/
    IEnumerator InsertBall(int index)
    {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i <= index; i++)
        {
            Ball2 ball = BallListSpawn2.Instance.ballList[i];
            int currentWaypointIndex = ball.currentWaypointIndex;
            float distance = Vector3.Distance(ball.waypoints[currentWaypointIndex].position, ball.transform.position);
            float distanceAB = Vector3.Distance(ball.waypoints[currentWaypointIndex + 1].position, ball.waypoints[currentWaypointIndex].position);
            Vector3 dir = ball.waypoints[currentWaypointIndex + 1].position - ball.waypoints[currentWaypointIndex].position;
            Vector3 newPos = ball.waypoints[currentWaypointIndex].position + dir.normalized * (distanceAB - distance);
            ball.transform.position = newPos;
        }
    }
    Vector3 PosBallAdd(int index)
    {
        Ball2 ball = BallListSpawn2.Instance.ballList[index];
        int currentWaypointIndex = ball.currentWaypointIndex;
        float distance = Vector3.Distance(ball.waypoints[currentWaypointIndex].position, ball.transform.position);
        float distanceAB = Vector3.Distance(ball.waypoints[currentWaypointIndex + 1].position, ball.waypoints[currentWaypointIndex].position);
        Vector3 dir = ball.waypoints[currentWaypointIndex + 1].position - ball.waypoints[currentWaypointIndex].position;
        Vector3 newPos = ball.waypoints[currentWaypointIndex].position + dir.normalized * (distanceAB - distance);
        return newPos;
    }
    public IEnumerator InsertBall1(int index)
    {
        for (int i = 0; i <= index; i++)
        {
            Ball2 ball = BallListSpawn2.Instance.ballList[i];
            ball.speed = moveSpeed * 2 + 0.01f;
        }
        yield return new WaitForSeconds(0.45f);
        for (int i = 0; i <= index; i++)
        {
            Ball2 ball = BallListSpawn2.Instance.ballList[i];
            ball.speed = moveSpeed;
            //ball.isMove = false;
        }
    }
    public void SameColorNext1(int index)
    {
        for (int i = index; i > 0; i--)
        {
            GameController.Instance.nextBallIndex = i - 1;
            if (BallListSpawn2.Instance.ballList[i].color == BallListSpawn2.Instance.ballList[i - 1].color)
            {
                count++;
            }
            else
            {
                return;
            }
        }
    }
    public void SameColorBehind1(int index)
    {
        for (int i = index; i < BallListSpawn2.Instance.ballList.Count - 1; i++)
        {
            GameController.Instance.behindBallIndex = i + 1;
            if (BallListSpawn2.Instance.ballList[i].color == BallListSpawn2.Instance.ballList[i + 1].color)
            {
                count++;
            }
            else
            {
                return;
            }
        }
    }
    void ReverseMove()
    {
        if (isReverse && !isBall)
        {
            speed = moveSpeed * 2;
            if (currentWaypointIndex < 1)
            {
                currentWaypointIndex = 1;
            }
            float tPosition = Vector3.Distance(waypoints[currentWaypointIndex - 1].position, waypoints[currentWaypointIndex].position);
            float mDirection = Vector3.Distance(waypoints[currentWaypointIndex - 1].position, transform.position);

            float distanceToTarget = mDirection - tPosition;
            if (currentWaypointIndex >= 0 && distanceToTarget <= stoppingDistance)
            {
                currentWaypointIndex--;
                /*targetPosition = waypoints[currentWaypointIndex].position;
                moveDirection = targetPosition - transform.position;
                moveDirection.Normalize();*/
            }
            targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
    IEnumerator StopReverse()
    {
        yield return new WaitForSeconds(timeToReverse);
        for (int i = BallListSpawn2.Instance.ballList.Count - 1; i >= 0; i--)
        {
            BallListSpawn2.Instance.ballList[i].speed = moveSpeed;
            BallListSpawn2.Instance.ballList[i].isReverse = false;
            //BallListSpawn2.Instance.ballList[i].isMove = false;             
        }
    }
    IEnumerator WaitBall()
    {
        isBall = true;
        yield return new WaitForSeconds(0f);
        isBall = false;
        isBallAdd = false;
        isMove = true;
    }
    IEnumerator DestroySameBall(int index)
    {
        if (isBallAdd)
        {
            yield return new WaitForSeconds(0.5f);
            isBallAdd = false;
        }
        if (!isBallAdd)
        {
            SameColorNext1(index);
            SameColorBehind1(index);
            if (count == BallListSpawn2.Instance.ballList.Count && count >= 3)
            {
                for (int i = BallListSpawn2.Instance.ballList.Count - 1; i >= 0; i--)
                {
                    Destroy(BallListSpawn2.Instance.ballList[i].gameObject);
                    BallListSpawn2.Instance.ballList.RemoveAt(i);
                }
            }
            if (count < 3)
            {
                GameController.Instance.ClearList();
            }
            else
            {
                GameController.Instance.RemoveSameBall();
                if (BallListSpawn2.Instance.ballList[GameController.Instance.nextBallIndex + 1].color == BallListSpawn2.Instance.ballList[GameController.Instance.nextBallIndex].color)
                {
                    for (int i = GameController.Instance.nextBallIndex; i >= 0; i--)
                    {
                        BallListSpawn2.Instance.ballList[i].isReverse = true;
                    }
                    /*for (int i = index-1; i < BallListSpawn2.Instance.ballList.Count; i++)
                    {
                        BallListSpawn2.Instance.ballList[i].isMove = false;
                    }*/
                }
                else
                {
                    GameController.Instance.StopBallList(GameController.Instance.nextBallIndex + 1);
                }
            }
        }

    }
}


