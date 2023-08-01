using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    public Transform[] routes;
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
    }
    private void Update()
    {
        if (coroutineAllow && isMoving)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
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
            ballPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
            transform.position = ballPos;
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
