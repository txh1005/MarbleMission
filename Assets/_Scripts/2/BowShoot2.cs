using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BowShoot2 : Singleton<BowShoot2>
{
    [SerializeField] private Ball2 ballPrefab;
    [SerializeField] private Transform mainBallPos;
    [SerializeField] private Transform parentObj;
    [SerializeField] private Transform storeBallObj;
    [SerializeField] private Transform extraBallPos;

    public Ball2 mainBall;
    public List<BallData> ballDataList;

    private Ball2 extraBall;
    private bool isSpawn = false;
    private float speedExtraUpgrad = 2f;

    private void Start()
    {
        SpawnBall();
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
        SwapBall();
    }
    void SpawnBall()
    {
        int randomMainIndex = Random.Range(0, ballDataList.Count);
        BallData randomMainBallData = ballDataList[randomMainIndex];
        mainBall = Instantiate(ballPrefab, mainBallPos.position, Quaternion.identity, mainBallPos);
        mainBall.GetComponent<SpriteRenderer>().sortingOrder=2;
        mainBall.ballData = randomMainBallData;
        int randomExtraIndex = Random.Range(0, ballDataList.Count);
        BallData randomExtraBallData = ballDataList[randomExtraIndex];
        extraBall = Instantiate(ballPrefab, extraBallPos.position, Quaternion.identity, extraBallPos);
        extraBall.GetComponent<SpriteRenderer>().sortingOrder=2;
        extraBall.ballData = randomExtraBallData;
        //mainBall.transform.SetParent(parentObj);
        //extraBall.transform.SetParent(parentObj);
        if (mainBall.transform.position != mainBallPos.position)
        {
            mainBall.transform.SetParent(storeBallObj);
        }
        extraBall.transform.localScale = new Vector3(0.6f, 0.6f, 0f);

        mainBall.isBall = true;
        extraBall.isBall = true;
    }
    void ExtraBallUpgradeToMainBall()
    {
        mainBall = extraBall;
        int randomExtraIndex = Random.Range(0, ballDataList.Count);
        BallData randomExtraBallData = ballDataList[randomExtraIndex];
        extraBall = Instantiate(ballPrefab, extraBallPos.position, Quaternion.identity, extraBallPos);
        extraBall.GetComponent<SpriteRenderer>().sortingOrder=2;
        extraBall.ballData = randomExtraBallData;
        extraBall.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
        mainBall.isBall = true;
        extraBall.isBall = true;
    }
    void SwapBall()
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
