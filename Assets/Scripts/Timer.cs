using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action OnDone;

    [field: SerializeField]
    public float Duration { get; set; }

    [field: SerializeField]
    public Color StartColor { get; private set; }

    [field: SerializeField]
    public Color EndColor { get; private set; }

    public float TimeElapsed => Time.time - TimeStart;

    public float TimeLeft => TimeStart + Duration - Time.time;

    public float TimeStart { get; private set; }
    
    public bool IsDone { get; private set; }

    private TextMeshPro tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    public void StartTimer() => StartTimer(Time.time);

    public void StartTimer(float time)
    {
        IsDone = false;
        TimeStart = time;
    }

    private void Update()
    {
        if (!IsDone && TimeLeft <= 0)
        {
            IsDone = true;
            OnDone?.Invoke();
        }

        tmp.text = Mathf.Max(0, TimeLeft).ToString("0.000");
        tmp.color = Color.Lerp(StartColor, EndColor, TimeElapsed / Duration);
    }
}
