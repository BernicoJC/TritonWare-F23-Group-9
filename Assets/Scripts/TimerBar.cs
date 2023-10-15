using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TimerBar : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.maxValue = timer.TimeEnd - timer.TimeStart;
        slider.value = timer.TimeLeft;
    }
}
