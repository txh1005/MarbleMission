using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBall
{
    Normal,
    Item
}
public enum BallColor
{
    Blue,
    Green,
    Red,
    Violet,
    Yellow
}
public enum BallItem
{
    Reverse,
    Stop,
    Slow
}
[CreateAssetMenu(fileName = "New Ball Data", menuName = "ScriptableObject/BallData")]
public class BallData : ScriptableObject
{
    public int id;
    public TypeBall type;
    public BallColor color;
    [HideInInspector]
    public BallItem item;
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(BallData))]
    public class BallDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            BallData ballData = (BallData)target;
            base.OnInspectorGUI();
            if (ballData.type == TypeBall.Item)
            {
                ballData.item = (BallItem)UnityEditor.EditorGUILayout.EnumPopup("Item Type", ballData.item);
            }
        }
    }
#endif
}
