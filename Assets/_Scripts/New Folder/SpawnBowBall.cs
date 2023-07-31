using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBowBall : MonoBehaviour
{
    [SerializeField]private GameObject[] ballPrefab;
    [SerializeField] private Transform mainBallPos;
    [SerializeField] private Transform extraBallPos;
    [SerializeField] private Transform parentObj;
    private float speedShoting=10f;

    private GameObject ExtraBall;
    private GameObject mainBall;
    private float speedExtraUpgrad=2f;
    private GameObject ballCanShot;
    
    bool isExtraEmpty;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
        ballCanShot = mainBall;
        isExtraEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        ShootMainBall();
        ExtraBallUpgradeToMainBall();
    }
    void SpawnBall()
    {
        int randomMainIndex = Random.Range(0, ballPrefab.Length);
        int randomExtraIndex = Random.Range(0, ballPrefab.Length);
        mainBall = Instantiate(ballPrefab[randomMainIndex], mainBallPos.position, Quaternion.identity);
        ExtraBall = Instantiate(ballPrefab[randomExtraIndex], extraBallPos.position, Quaternion.identity);
        mainBall.transform.SetParent(parentObj);
        ExtraBall.transform.SetParent(parentObj);
        ExtraBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
    }
    void SpawnExtraBall()
    {
        int randomExtraIndex = Random.Range(0, ballPrefab.Length);
        GameObject newExtraBall = Instantiate(ballPrefab[randomExtraIndex], extraBallPos.position, Quaternion.identity);
        newExtraBall.transform.SetParent(parentObj);
        newExtraBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
        isExtraEmpty = false;
    }    
    void ShootMainBall()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDirection = (mousePosition - (Vector2)mainBallPos.position).normalized;
            ballCanShot.GetComponent<Rigidbody2D>().velocity = shootDirection * speedShoting;
        }
    }
    void ExtraBallUpgradeToMainBall()
    {
        if (ballCanShot.transform.position!= mainBallPos.position)
        {
            ExtraBall.transform.position = Vector3.MoveTowards(ExtraBall.transform.position, mainBallPos.position, speedExtraUpgrad * Time.deltaTime);
            ExtraBall.transform.localScale = Vector3.Lerp(ExtraBall.transform.localScale, Vector3.one, speedExtraUpgrad);
            ballCanShot = ExtraBall;
            if (isExtraEmpty&&ExtraBall.transform.position != extraBallPos.position)
            {
                SpawnExtraBall();
            }
        }
    }   
}
