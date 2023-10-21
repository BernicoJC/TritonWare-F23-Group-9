using System.Linq;

public class OrbitPowerup : Powerup
{
    protected override bool IsTb => true;

    protected override void OnPickedUp()
    {
        var prefabs = GetComponentInParent<RtPrefabs>();

        var player = FindObjectsOfType<RtPlayer>().First(p => p.Owner == Owner);
        var flame = Instantiate(prefabs.OrbitingFlame);
        flame.Owner.Set(Owner);
    }
}
