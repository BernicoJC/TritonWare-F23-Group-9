using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour, INeverDisabled
{
    [SerializeField]
    private RtPlayer rtPlayer;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.maxValue = rtPlayer.MaxHealth;
        slider.value = rtPlayer.Health;
    }
}
