using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BallColorCount : Singleton<BallColorCount>
{
    public BallDataSO dataSO;
    [ShowInInspector]
    public Dictionary<string, int> BallColorDic = new Dictionary<string, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        //BallColorDic = new Dictionary<string, int>();
        CountColorsInBallData();
    }
    public void CountColorsInBallData()
    {
        foreach (BallData2 data in dataSO.ballDatas)
        {
            string colorName = data.color1.ToString();          
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
    public void AddValueDic(Ball2 ball,int i)
    {
        string colorName = ball.ballData.color1.ToString();
        if (BallColorDic.ContainsKey(colorName))
        {
            BallColorDic[colorName] += i;
        }
        if (BallColorDic[colorName]<=0)
        {
            BallColorDic.Remove(colorName);
        }
    }    
}
