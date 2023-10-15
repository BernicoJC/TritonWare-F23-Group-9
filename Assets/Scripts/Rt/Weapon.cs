using UnityEngine;

public class Weapon : OwnedObject
{
    [SerializeField]
    private Transform firePoint;

    private RtGame game;
    private RtPrefabs prefabs;

    protected override void Awake()
    {
        base.Awake();
        game = GetComponentInParent<RtGame>();
        prefabs = GetComponentInParent<RtPrefabs>();
    }

    private void Update() 
    {
        if (Input.GetButtonDown("Attack" + ((Player)Owner).ToSuffix()))
            Shoot();
    }

    private void Shoot()
    {
        var projectile = Instantiate(prefabs.Projectile, firePoint.position, firePoint.rotation, game.transform);
        projectile.Owner.Set(Owner);
    }
}
