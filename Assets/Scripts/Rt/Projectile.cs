using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : OwnedObject
{
    [field: SerializeField]
    public float Speed { get; private set; } = 20f;

    private Rigidbody2D rb;
    Vector3 upRotation = new Vector3(0, 0, 45);
    Vector3 downRotation = new Vector3(0, 0, -45);

    public AudioSource hitSound;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (Input.GetButtonDown("UpAttack" + ((Player)Owner).ToSuffix()))
        {
            //rb.velocity = new Vector2(10, 10);
            rb.velocity = (transform.right + upRotation) * Speed;
        }
        if (Input.GetButtonDown("DownAttack" + ((Player)Owner).ToSuffix()))
        {
            //rb.velocity = new Vector2(10, 10);
            rb.velocity = (transform.right + downRotation) * Speed;
        }
        if (Input.GetButtonDown("Attack" + ((Player)Owner).ToSuffix()))
        {
            rb.velocity = transform.right * Speed;
            //print(rb.velocity);
        }

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var player = hitInfo.GetComponent<RtPlayer>();
        if (player == null || player.Owner == Owner)
            return;

        player.Health--;
        //hitSound.Play();
        Destroy(gameObject);
    }
}
