using System.Linq;
using UnityEngine;

public class SpeedPowerup : Powerup
{
    [SerializeField]
    private int speedIncrease = 15;

    protected override bool IsTb => true;

    protected override void OnPickedUp()
    {
        var rtPlayer = FindObjectsOfType<RtPlayer>().First(p => p.Owner == Owner);
        rtPlayer.Speed += speedIncrease;
    }
}
