using System.Linq;
using UnityEngine;

public class StrengthPowerup : Powerup
{
    [SerializeField]
    private int damageIncrease = 1;

    protected override bool IsTb => true;

    protected override void OnPickedUp()
    {
        var weapon = FindObjectsOfType<Weapon>().First(w => w.Owner == Owner);
        weapon.Damage += damageIncrease;
    }
}
