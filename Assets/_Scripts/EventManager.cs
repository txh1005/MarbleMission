using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    public static event Action<string> OnGameOver;

    void Start()
    {
        
    }
    public void Move()
    {
        OnGameOver?.Invoke("abc");
    }
}
