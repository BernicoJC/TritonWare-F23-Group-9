using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OwnedSprite : OwnedObject
{
    [SerializeField]
    private PlayerSpriteList sprites;

    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnOwnerSet()
    {
        spriteRenderer.sprite = ((Player)Owner).Select(sprites);
    }
}
