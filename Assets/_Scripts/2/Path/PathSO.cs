using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New Path", menuName = "ScriptableObject/Path")]
public class PathSO : ScriptableObject
{
    public PathData[] pathDatas;
    public int count;
    private PathData pathDataClone;
    [Button]
    private void CreatPathdata()
    {
        PathData[] pathDatasClone = new PathData[count];
        for (int i = 0; i < count; i++)
        {
            if (i < pathDatas.Length)
            {
                pathDatasClone[i] = pathDatas[i];
            }
            else
            {
                pathDataClone = new PathData();
                pathDataClone.index = i;
                pathDatasClone[i] = pathDataClone;
            }
        }
        pathDatas = pathDatasClone;
    }
}
