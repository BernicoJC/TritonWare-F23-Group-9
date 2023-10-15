using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action OnDone;
    
    public bool IsDone { get; private set; }

    public float TimeStart { get; private set; }

    public float TimeEnd { get; private set; }

    public float TimeElapsed => Time.time - TimeStart;

    public float TimeLeft => TimeEnd - Time.time;

    private float disabledTime = -1;

    private void Awake()
    {
        IsDone = true;
    }

    public void StartTimer(float duration)
    {
        IsDone = false;
        TimeStart = Time.time;
        TimeEnd = TimeStart + duration;
    }

    public void AddTimer(float duration)
    {
        TimeEnd += duration;
    }

    public void EndTimer()
    {
        IsDone = true;
        OnDone?.Invoke();
    }

    private void Update()
    {
        if (!IsDone && TimeLeft <= 0)
            EndTimer();
    }

    private void OnEnable()
    {
        if (disabledTime < 0)
            return;

        float elapsed = disabledTime - TimeStart;
        float duration = TimeEnd - TimeStart;
        TimeStart = Time.time - elapsed;
        TimeEnd = TimeStart + duration;

        disabledTime = -1;
    }

    private void OnDisable()
    {
        disabledTime = Time.time;
    }
}
