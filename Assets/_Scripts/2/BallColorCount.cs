using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BallColorCount : MonoBehaviour
{
    [ShowInInspector]
    public Dictionary<string, int> BallColorDic = new Dictionary<string, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        //BallColorDic = new Dictionary<string, int>();
        CountColorsInBallData1();
    }  
    void CountColorsInBallData1()
    {
        foreach (BallData data in BallListSpawn2.Instance.ballData1)
        {
            string colorName = data.color.ToString();          
            if (BallColorDic.ContainsKey(colorName))
            {
                BallColorDic[colorName]++;
            }
            else
            {
                BallColorDic.Add(colorName, 1);
            }
        }
    }
}
