using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerText : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private string format = "00.00";

    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        tmp.text = timer.TimeLeft.ToString(format);
    }
}
