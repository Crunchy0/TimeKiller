using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class TimerManager
{
    private static event Action<float> Update;

    public static void Subscribe(Action<float> callback)
    {
        Update += callback;
    }

    public static void Unsubscribe(Action<float> callback)
    {
        Update -= callback;
    }

    public static void UpdateAllTimers(float deltaTime)
    {
        Update?.Invoke(deltaTime);
    }
}
