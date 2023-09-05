using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ball Data", menuName = "ScriptableObject/BallData2")]
public class BallDataSO : ScriptableObject
{
    public BallData2[] ballDatas;
    public int count;
    private BallData2 ballDataClone;
    [Button]
    private void CreatBalldata()
    {
        BallData2[] ballDatasClone = new BallData2[count];
        for (int i = 0; i < count; i++)
        {
            if (i < ballDatas.Length)
            {
                ballDatasClone[i] = ballDatas[i];
            }
            else
            {
                ballDataClone = new BallData2();
                ballDataClone.index = i;
                ballDatasClone[i] = ballDataClone;
            }
        }
        ballDatas = ballDatasClone;
    }
}
