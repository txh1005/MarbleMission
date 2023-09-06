using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallColor1
{
    Blue,
    Green,
    Red,
    Violet,
    Yellow
}
public enum BallItem1
{
    Reverse,
    Stop,
    Slow
}
public enum TypeBall1
{
    Normal,
    Item
}
[System.Serializable]
public class BallData2
{
    public int index;
    public BallColor1 color1;
}