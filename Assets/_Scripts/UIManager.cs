using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ThunderBooster()
    {
        BowShoot2.Instance.mainBall.ballAnim.SetActive(false);
        BowShoot2.Instance.mainBall.GetComponent<SpriteRenderer>().sprite = BowShoot2.Instance.mainBall.ballThunderColor;
        BowShoot2.Instance.mainBall.isThunder = true;
        //BowShoot2.Instance.mainBall.isFire = false;
    }    
}
