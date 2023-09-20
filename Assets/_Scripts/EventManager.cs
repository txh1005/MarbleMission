using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public static event Action<string> OnGameOver;
    public UnityAction ShowObject;
    public void Move()
    {
        OnGameOver?.Invoke("abc");
    }
}
