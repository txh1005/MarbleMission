using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int Data { get; set; }
    public Ball Next { get; set; }
    public Ball Prev { get; set; }
}

