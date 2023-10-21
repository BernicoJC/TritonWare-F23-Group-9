using UnityEngine;

public class Weapon : OwnedObject
{
    [SerializeField]
    private Transform firePoint;

    private RtGame game;
    private RtPrefabs prefabs;

    private float nextAttack;

    [SerializeField]
    private float attackCooldown = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        game = GetComponentInParent<RtGame>();
        prefabs = GetComponentInParent<RtPrefabs>();
    }

    private void Update() 
    {
        if (Time.time < nextAttack)
            return;

        if (Input.GetButtonDown("Attack" + ((Player)Owner).ToSuffix()))
        {
            attack();
        }
        else if (Input.GetButtonDown("UpAttack" + ((Player)Owner).ToSuffix()))
        {
            var projectile = attack();
            projectile.transform.Rotate(0, 0, 45);
        }
        else if (Input.GetButtonDown("DownAttack" + ((Player)Owner).ToSuffix()))
        {
            var projectile = attack();
            projectile.transform.Rotate(0, 0, -45);
        }
    }

    private Projectile attack()
    {
        var projectile = Instantiate(prefabs.Projectile, firePoint.position, firePoint.rotation, game.transform);
        projectile.Owner.Set(Owner);
        nextAttack = Time.time + attackCooldown;

        return projectile;
    }
}
