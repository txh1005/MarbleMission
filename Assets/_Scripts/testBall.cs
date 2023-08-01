using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBall : MonoBehaviour
{
    public GameObject prefabBall;
    public int maxBall;
    public float distance;
    public float timeBtwBall = 1.5f;
    private List<GameObject> ballList = new List<GameObject>();

    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private float speed;
    private bool coroutineAllow;
    private bool isMoving;

    private void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speed = 0.5f;
        coroutineAllow = true;
        isMoving = true;
        StartCoroutine(SpawnAndMoveBalls());
    }

    private IEnumerator SpawnAndMoveBalls()
    {
        // Spawn the initial set of balls
        SpawnBalls();

        while (isMoving)
        {
            if (coroutineAllow)
            {
                StartCoroutine(MoveBallsOnRoute(routeToGo));
            }

            // Wait for all balls to reach the end of the route before spawning the next set
            bool allBallsReachedEnd = true;

            if (allBallsReachedEnd)
            {
                yield return new WaitForSeconds(timeBtwBall);
                SpawnBalls();
            }

            yield return null;
        }
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < maxBall; i++)
        {
            Vector3 pos = new Vector3(i * distance, 0f, 0f);
            GameObject ball = Instantiate(prefabBall, pos, Quaternion.identity);
            ballList.Add(ball);
        }
    }

    private IEnumerator MoveBallsOnRoute(int routeIndex)
    {
        coroutineAllow = false;

        Vector2 p0 = routes[routeIndex].GetChild(0).position;
        Vector2 p1 = routes[routeIndex].GetChild(1).position;
        Vector2 p2 = routes[routeIndex].GetChild(2).position;
        Vector2 p3 = routes[routeIndex].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;

            foreach (GameObject ball in ballList)
            {
                Vector2 ballPos = Mathf.Pow(1 - tParam, 3) * p0 +
                                  3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                                  3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                                  Mathf.Pow(tParam, 3) * p3;
                ball.transform.position = ballPos;
            }

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo++;
        if (routeToGo > routes.Length - 1)
        {
            isMoving = false;
        }
        coroutineAllow = true;
    }
}
