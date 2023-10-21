using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : OwnedObject
{
    [field: SerializeField]
    public float Speed { get; set; } = 20f;

    [field: SerializeField]
    public int Damage { get; set; } = 1;

    [field: SerializeField]
    public int HomingStrength { get; set; } = 0;

    private Rigidbody2D rb;
    private RtPlayer target;

    private AudioSource hitSound;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        hitSound = FindObjectsOfType<AudioSource>().First(a => a.name == "hitNoise");
    }

    private void Start()
    {
        target = FindObjectsOfType<RtPlayer>().First(p => p.Owner != Owner);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * Speed;

        if (HomingStrength == 0)
            return;

        var direction = (Vector2)(target.transform.position - transform.position);
        var angleDiff = Vector2.SignedAngle(rb.velocity, direction);
        var clampedAngleDiff = Mathf.Min(Mathf.Abs(angleDiff), HomingStrength * Time.fixedDeltaTime);

        if (transform.eulerAngles.y > 90)
            clampedAngleDiff *= -1;

        transform.Rotate(Vector3.forward, Mathf.Sign(angleDiff) * clampedAngleDiff);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var player = hitInfo.GetComponent<RtPlayer>();
        if (player == null || player.Owner == Owner)
            return;

        player.Health -= Damage;
        hitSound.Play();
        Destroy(gameObject);
    }
}
