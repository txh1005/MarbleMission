using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowShootTest : Singleton<BowShootTest>
{
    [SerializeField] private TestBall ballPrefab;
    [SerializeField] private Transform mainBallPos;
    [SerializeField] private Transform parentObj;
    [SerializeField] private Transform storeBallObj;
    [SerializeField] private Transform extraBallPos;
    [SerializeField] private float speedExtraUpgrad;

    public TestBall mainBall;
    public List<BallData2> ballDataList;
    public BallColorCount ballColorCount;
    public List<string> availableColors;
    public TestBall extraBall;
    public BallSkill ballSkill;
    public Button thunderButton;

    private bool isSpawn = false;


    private void Start()
    {
        StartCoroutine(SpawnBall());
        Button btnThunder = thunderButton.GetComponent<Button>();
        btnThunder.onClick.AddListener(ClickBtnThunder);

    }
    void ClickBtnThunder()
    {
        Debug.Log("a");
    }
    private void Update()
    {
        availableColors = new List<string>(ballColorCount.BallColorDic.Keys);

        if (Input.GetMouseButtonUp(0))
        {
            if (mainBall == null || extraBall == null)
            {
                return;
            }
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
                ExtraBallUpgradeToMainBallTest();
            }
            else
            {
                extraBall.transform.position = Vector3.MoveTowards(extraBall.transform.position, mainBallPos.position, speedExtraUpgrad * Time.deltaTime);
                extraBall.transform.localScale = Vector3.Lerp(extraBall.transform.localScale, new Vector3(1.1f, 1.1f), speedExtraUpgrad);
            }
        }
        SwapBall();
    }
    IEnumerator SpawnBall()
    {
        /*int randomMainIndex = Random.Range(0, ballDataList.Count);
        BallData2 randomMainBallData = ballDataList[randomMainIndex];*/
        yield return new WaitForSeconds(1f);
        int randomMainIndex = Random.Range(0, availableColors.Count);
        string nameColor = availableColors[randomMainIndex];
        BallColor1 colorMainBall = (BallColor1)System.Enum.Parse(typeof(BallColor1), nameColor);

        mainBall = Instantiate(ballPrefab, mainBallPos.position, Quaternion.identity, mainBallPos);
        mainBall.GetComponent<SpriteRenderer>().sortingOrder = 2;
        mainBall.ballData.color1 = colorMainBall;
        /*int randomExtraIndex = Random.Range(0, ballDataList.Count);
        BallData2 randomExtraBallData = ballDataList[randomExtraIndex];*/
        int randomExtraIndex = Random.Range(0, availableColors.Count);
        string nameExtraColor = availableColors[randomExtraIndex];
        BallColor1 colorExtraBall = (BallColor1)System.Enum.Parse(typeof(BallColor1), nameExtraColor);

        extraBall = Instantiate(ballPrefab, extraBallPos.position, Quaternion.identity, extraBallPos);
        extraBall.GetComponent<SpriteRenderer>().sortingOrder = 2;
        extraBall.ballData.color1 = colorExtraBall;
        //mainBall.transform.colorExtraBall(parentObj);
        //extraBall.transform.SetParent(parentObj);
        if (mainBall.transform.position != mainBallPos.position)
        {
            mainBall.transform.SetParent(storeBallObj);
        }
        extraBall.transform.localScale = new Vector3(0.6f, 0.6f, 0f);

        mainBall.isBall = true;
        extraBall.isBall = true;
    }
    IEnumerator ExtraBallUpgradeToMainBall()
    {
        yield return new WaitForSeconds(0.1f);
        /*int randomMainIndex1 = Random.Range(0, availableColors.Count);
        string nameColor = availableColors[randomMainIndex1];
        BallColor1 mainColor = (BallColor1)System.Enum.Parse(typeof(BallColor1), nameColor);*/

        int randomExtraIndex1 = Random.Range(0, availableColors.Count);
        string nameColor1 = availableColors[randomExtraIndex1];
        BallColor1 extraColor = (BallColor1)System.Enum.Parse(typeof(BallColor1), nameColor1);

        //mainBall.ballData.color1 = mainColor;
        mainBall = extraBall;

        /*int randomExtraIndex = Random.Range(0, ballDataList.Count);
        BallData2 randomExtraBallData = ballDataList[randomExtraIndex];*/

        extraBall = Instantiate(ballPrefab, extraBallPos.position, Quaternion.identity, extraBallPos);
        extraBall.GetComponent<SpriteRenderer>().sortingOrder = 2;
        //extraBall.ballData = randomExtraBallData;

        extraBall.ballData.color1 = extraColor;
        extraBall.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
        mainBall.isBall = true;
        extraBall.isBall = true;
    }
    void ExtraBallUpgradeToMainBallTest()
    {
        /*int randomMainIndex1 = Random.Range(0, availableColors.Count);
        string nameColor = availableColors[randomMainIndex1];
        BallColor1 mainColor = (BallColor1)System.Enum.Parse(typeof(BallColor1), nameColor);*/

        int randomExtraIndex1 = Random.Range(0, availableColors.Count);
        string nameColor1 = availableColors[randomExtraIndex1];
        BallColor1 extraColor = (BallColor1)System.Enum.Parse(typeof(BallColor1), nameColor1);

        //mainBall.ballData.color1 = mainColor;
        mainBall = extraBall;

        /*int randomExtraIndex = Random.Range(0, ballDataList.Count);
        BallData2 randomExtraBallData = ballDataList[randomExtraIndex];*/

        extraBall = Instantiate(ballPrefab, extraBallPos.position, Quaternion.identity, extraBallPos);
        extraBall.GetComponent<SpriteRenderer>().sortingOrder = 2;
        //extraBall.ballData = randomExtraBallData;

        extraBall.ballData.color1 = extraColor;
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