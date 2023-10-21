using UnityEngine;

public class Weapon : OwnedObject
{
    [field: SerializeField]
    public int Damage { get; set; } = 1;

    [field: SerializeField]
    public int HomingStrength { get; set; } = 0;

    [SerializeField]
    private Transform firePoint;

    private RtGame game;
    private RtPrefabs prefabs;

    private float nextAttack;

    [SerializeField]
    private float attackCooldown = 0.3f;

    [SerializeField]
    private float aimAngle = 45f;

    public AudioSource shootNoise;

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
            shootNoise.Play();
        }
        else if (Input.GetButtonDown("UpAttack" + ((Player)Owner).ToSuffix()))
        {
            var projectile = attack();
            projectile.transform.Rotate(0, 0, aimAngle);
            shootNoise.Play();
        }
        else if (Input.GetButtonDown("DownAttack" + ((Player)Owner).ToSuffix()))
        {
            var projectile = attack();
            projectile.transform.Rotate(0, 0, -aimAngle);
            shootNoise.Play();
        }
    }

    private Projectile attack()
    {
        var projectile = Instantiate(prefabs.Projectile, firePoint.position, firePoint.rotation, game.transform);
        projectile.Owner.Set(Owner);
        var component = projectile.GetComponent<Projectile>();
        component.Damage = Damage;
        component.HomingStrength = HomingStrength;

        nextAttack = Time.time + attackCooldown;
        return projectile;
    }
}
