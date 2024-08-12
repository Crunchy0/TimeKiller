using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// TO STRUCT
// DELTA TIME - BAD
public class Timer
{
    public event Action TimerExpired;

    public float Duration
    {
        get => _duration;
        set => _duration = value > 0 ? value : 0;
    }

    public bool IsExpired
    {
        get => _timeLeft < 1e-3f;
    }

    private float _duration;
    private float _timeLeft = 0f;

    public Timer(float duration = 0f)
    {
        _duration = duration;
    }

    public void Update(float deltaTime)
    {
        if (IsExpired) return;

        _timeLeft -= deltaTime;

        if (IsExpired) TimerExpired?.Invoke();
    }

    public void Reset()
    {
        _timeLeft = _duration;
    }
}