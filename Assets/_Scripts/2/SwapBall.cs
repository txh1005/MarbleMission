using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBall : MonoBehaviour
{
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(mousePosition, Vector2.up);
            if (Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity))
            {
                
            }
        }
    }

}
