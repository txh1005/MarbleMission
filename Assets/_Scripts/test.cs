using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject[] prefab;
    public PathCreator path;
    public float speed;
    public float delayTime;
    public int maxBall;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(delayTime);
        for (int i = 0; i < maxBall; i++)
        {
            yield return new WaitForSeconds(delayTime);
            GameObject ball = Instantiate(prefab[Random.Range(0,prefab.Length)],path.path.GetPoint(0),Quaternion.identity);
            StartCoroutine(MoveBall(ball.transform,i));
        }
    }
    IEnumerator MoveBall(Transform ballPos,int index)
    {
        float distance=0;
        while (distance<(path.path.length-1))
        {
            if (ballPos!=null)
            {
                distance += speed * Time.deltaTime;
                ballPos.position = path.path.GetPointAtDistance(distance);
            }
            yield return null;
        }
    }
}
