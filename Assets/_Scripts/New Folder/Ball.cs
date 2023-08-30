using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    public float ballSpeed;
    Vector3 direction;
    bool isFire;
    public bool isBall;
    private Vector3 initialPosition;
    [SerializeField] ballType ballType;
    Vector3 contactPoint;
    public int id;
    public float distance;
    public Ball ball1;
    int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        isFire = false;
        initialPosition = transform.position;
        //rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LaunchBall()
    {
        if (!isFire)
        {
            direction = initialPosition;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //rigid.AddForce((mousePosition - direction) * ballSpeed, ForceMode2D.Impulse);
            rigid.velocity = (mousePosition - direction) * ballSpeed;
        }
        isFire = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "destroy")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Ball coliObj = other.gameObject.GetComponent<Ball>();
        if (other.gameObject.tag == "ball" && isBall)
        {
            //Vector3 collisionNormal = other.contacts[0].point;
            int index = BallListSpawn.Instance.ballList.IndexOf(coliObj);
            distance = BallListSpawn.Instance.ballList[index].distance;
            //BallListSpawn.Instance.ballListNext[index].transform.position - BallListSpawn.Instance.ballList[index].transform.position;
            //float angle = Vector3.Angle(BallListSpawn.Instance.ballList[index].transform.position-collisionNormal, BallListSpawn.Instance.ballListNext[index].transform.position - BallListSpawn.Instance.ballList[index].transform.position);
            
            BallListSpawn.Instance.ballList.Insert(index,BowShoot.Instance.mainBall);
            //BallListSpawn.Instance.InsertList(index,BowShoot.Instance.mainBall);
            BallListSpawn.Instance.Move1(index+1);
            StartCoroutine(BallListSpawn.Instance.MoveBall(BallListSpawn.Instance.ballList.Count-1));

            SameColorBefore(index);
            Debug.Log(count);
            isBall = false;

        }
        if (other.gameObject.tag == "end" && !isBall)
        {
            Time.timeScale = 0;
        }
    } 
    void SameColorBefore(int index)
    {
        for (int i = index; i > 0; i--)
        {
            if (BallListSpawn.Instance.ballList[i].ballType == BallListSpawn.Instance.ballList[i - 1].ballType)
            {
                count++;
                i--;
            }
            else
            {
                return;
            }
        }
    }
    void SameColorAfter(int index)
    {
        for (int i = index; i > 0; i--)
        {
            if (BallListSpawn.Instance.ballList[i].ballType == BallListSpawn.Instance.ballList[i - 1].ballType)
            {
                count++;
                i--;
            }
            else
            {
                return;
            }
        }
    }
}
