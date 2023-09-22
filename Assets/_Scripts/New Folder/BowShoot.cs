/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BowShoot : MonoBehaviour
{
    private static BowShoot instance;
    public static BowShoot Instance { get => instance; }
    [SerializeField] private Ball[] ballPrefab;
    public Ball mainBall;
    private Ball extraBall;
    [SerializeField] private Transform mainBallPos;
    [SerializeField] private Transform parentObj;
    [SerializeField] private Transform storeBallObj;
    [SerializeField] private Transform extraBallPos;
    private bool isSpawn = false;
    private float speedExtraUpgrad = 4f;
    private void Awake()
    {
        if (BowShoot.instance != null) Debug.LogError("Only 1 BowShoot allow to exist");
        BowShoot.instance = this;
    }
    private void Start()
    {
        SpawnBall();
        //ball = mainBall.GetComponent<Ball>();

    }


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mainBall.LaunchBall();
            mainBall.transform.SetParent(storeBallObj);
            extraBall.transform.SetParent(mainBallPos);
            isSpawn = true;
        }
        if (isSpawn)
        {
            if (extraBall.transform.position == mainBallPos.position)
            {
                isSpawn = false;
                ExtraBallUpgradeToMainBall();
            }
            else
            {
                extraBall.transform.position = Vector3.MoveTowards(extraBall.transform.position, mainBallPos.position, speedExtraUpgrad * Time.deltaTime);
                extraBall.transform.localScale = Vector3.Lerp(extraBall.transform.localScale, Vector3.one, speedExtraUpgrad);
            }
        }
        swapBall();
    }
    void SpawnBall()
    {
        int randomMainIndex = Random.Range(0, ballPrefab.Length);
        int randomExtraIndex = Random.Range(0, ballPrefab.Length);
        mainBall = Instantiate(ballPrefab[randomMainIndex], mainBallPos.position, Quaternion.identity, mainBallPos);
        extraBall = Instantiate(ballPrefab[randomExtraIndex], extraBallPos.position, Quaternion.identity, extraBallPos);
        //mainBall.transform.SetParent(parentObj);
        //extraBall.transform.SetParent(parentObj);
        if (mainBall.transform.position != mainBallPos.position)
        {
            mainBall.transform.SetParent(storeBallObj);
        }
        extraBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);

        mainBall.isBall = true;
    }
    void ExtraBallUpgradeToMainBall()
    {
        int randomExtraIndex = Random.Range(0, ballPrefab.Length);
        mainBall = extraBall;
        extraBall = Instantiate(ballPrefab[randomExtraIndex], extraBallPos.position, Quaternion.identity, extraBallPos);
        extraBall.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
        mainBall.isBall = true;
    }
    void swapBall()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward);
        if (hitInfo.collider != null && hitInfo.collider.CompareTag("stone"))
        {
            if (Input.GetMouseButtonUp(0))
            {
                mainBall.gameObject.SetActive(false);
            }
        }
    }
}
*/