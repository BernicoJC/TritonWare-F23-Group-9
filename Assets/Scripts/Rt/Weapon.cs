using UnityEngine;

public class Weapon : OwnedObject
{
    [SerializeField]
    private Transform firePoint;

    private RtGame game;
    private RtPrefabs prefabs;
    Quaternion upRotation = Quaternion.Euler(0,0,45);
    Quaternion downRotation = Quaternion.Euler(0, 0, -45);
    private float nextFire = 1.3f;
    private float fireRate = 0.3f;
    protected override void Awake()
    {
        base.Awake();
        game = GetComponentInParent<RtGame>();
        prefabs = GetComponentInParent<RtPrefabs>();
    }

    private void Update() 
    {
        if (Input.GetButtonDown("Attack" + ((Player)Owner).ToSuffix()) && Time.time > nextFire)
            Shoot();
        if (Input.GetButtonDown("UpAttack" + ((Player)Owner).ToSuffix()) && Time.time > nextFire)
        {
            var projectile = Instantiate(prefabs.Projectile, firePoint.position, firePoint.rotation * upRotation, game.transform);
            projectile.Owner.Set(Owner);
            nextFire = Time.time + fireRate;
        }
        if (Input.GetButtonDown("DownAttack" + ((Player)Owner).ToSuffix()) && Time.time > nextFire)
        {
            var projectile = Instantiate(prefabs.Projectile, firePoint.position, firePoint.rotation * downRotation, game.transform);
            projectile.Owner.Set(Owner);
            nextFire = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        var projectile = Instantiate(prefabs.Projectile, firePoint.position, firePoint.rotation, game.transform);
        projectile.Owner.Set(Owner);
        nextFire = Time.time + fireRate;
    }
}
