using System.Linq;
using UnityEngine;

public class HomingPowerup : Powerup
{
    [SerializeField]
    private int homingStrengthIncrease = 20;

    protected override bool IsTb => true;

    protected override void OnPickedUp()
    {
        var weapon = FindObjectsOfType<Weapon>().First(w => w.Owner == Owner);
        weapon.HomingStrength += homingStrengthIncrease;
    }
}
