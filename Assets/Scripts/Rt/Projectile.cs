using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : OwnedObject
{
    [field: SerializeField]
    public float Speed { get; private set; } = 20f;

    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var player = hitInfo.GetComponent<RtPlayer>();
        if (player == null || player.Owner == Owner)
            return;

        player.Health--;
        Destroy(gameObject);
    }
}
