using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class OrbitingFlame : OwnedObject
{
    [field: SerializeField]
    public int OrbitSpeed { get; set; } = 40;

    [field: SerializeField]
    public int Damage { get; set; } = 3;

    [field: SerializeField]
    public float OrbitRadius { get; set; } = 1f;

    private float angle;
    private RtPlayer player;
    private SpriteRenderer spriteRenderer;
    private RtGame game;

    private bool isActive = true;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        game = FindObjectOfType<RtGame>();

        game.OnRoundEnd += onRoundChange;
        angle = Random.value * 360f;
    }

    protected override void OnOwnerSet()
    {
        player = FindObjectsOfType<RtPlayer>().First(p => p.Owner == Owner);
    }

    private void FixedUpdate()
    {
        if (player.IsDestroyed())
            return;

        angle += OrbitSpeed * Time.fixedDeltaTime;
        angle %= 360f;

        float rad = angle * Mathf.Deg2Rad * Mathf.Pow(-1, (int)Owner);
        transform.position = player.transform.position + new Vector3(OrbitRadius * Mathf.Cos(rad), OrbitRadius * Mathf.Sin(rad));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
            return;

        var player = collision.GetComponent<RtPlayer>();
        if (player == null || player.Owner == Owner)
            return;

        player.Health -= Damage;

        isActive = false;
        spriteRenderer.color = Color.clear;
    }

    private void onRoundChange()
    {
        isActive = true;
        spriteRenderer.color = Color.white;
        angle = Random.value * 360f;
    }
}
