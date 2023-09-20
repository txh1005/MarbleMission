using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSlow : MonoBehaviour
{
    Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        currentColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball2 ball = collision.gameObject.GetComponent<Ball2>();
        if (collision.CompareTag("ball") && !ball.isBall)
        {
            StartCoroutine(Slow());
        }
    }
    public IEnumerator Slow()
    {
        for (int i = 0; i < BallListSpawn2.Instance.ballList.Count; i++)
        {
            BallListSpawn2.Instance.ballList[i].speed = 0.5f;
            BallListSpawn2.Instance.ballList[i].isSlow = true;
        }
        currentColor.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        for (int i = 0; i < BallListSpawn2.Instance.ballList.Count; i++)
        {
            BallListSpawn2.Instance.ballList[i].speed = 0.9f;
            BallListSpawn2.Instance.ballList[i].isSlow = false;
        }
    }
}
