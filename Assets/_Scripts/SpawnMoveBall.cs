using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoveBall : MonoBehaviour
{
    public GameObject[] prefabBall;
    public int maxBall;
    public float distance;
    public float timeBtwBall=1.5f;
    private List<GameObject> ballList = new List<GameObject>();

    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 ballPos;
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
        StartCoroutine(spawnBall());
    }
    private void Update()
    {
        //spawnBall();
        if (coroutineAllow && isMoving)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }
    public void spawnBall1()
    {
        for (int i = 0; i < maxBall; i++)
        {
            //int a = Random.Range(0,prefabBall.Length-1);
            Vector3 pos = new Vector3(i* distance, 0f,0f);
            GameObject ball = Instantiate(prefabBall[i], pos,Quaternion.identity);
            ballList.Add(ball);
        }
    } 
    IEnumerator spawnBall()
    {
        yield return new WaitForSeconds(timeBtwBall);
        for (int i = 0; i < maxBall; i++)
        {
            yield return new WaitForSeconds(timeBtwBall);
            //int a = Random.Range(0,prefabBall.Length-1);
            Vector3 pos = new Vector3(i * distance, 0f, 0f);
            GameObject ball = Instantiate(prefabBall[Random.Range(0, prefabBall.Length)], pos, Quaternion.identity);
            ballList.Add(ball);
        }
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator GoByTheRoute(int routesNumber)
    {
        coroutineAllow = false;
        Vector2 p0 = routes[routesNumber].GetChild(0).position;
        Vector2 p1 = routes[routesNumber].GetChild(1).position;
        Vector2 p2 = routes[routesNumber].GetChild(2).position;
        Vector2 p3 = routes[routesNumber].GetChild(3).position;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;
            foreach (GameObject ball in ballList)
            {
              
                ballPos = Mathf.Pow(1 - tParam, 3) * p0 +
                        3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                        3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                        Mathf.Pow(tParam, 3) * p3;
                ball.transform.position = ballPos;
             
            }
            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;
        routeToGo += 1;
        if (routeToGo > routes.Length - 1)
        {   
            isMoving = false;
        }
        coroutineAllow = true;
    }
}
