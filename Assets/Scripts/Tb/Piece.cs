using UnityEngine;

public class Piece : MonoBehaviour
{
    public Vector2Int Position { get; set; }

    [field: SerializeField]
    public float Speed { get; private set; }

    public Player Player
    {
        get => player;
        set
        {
            if (player == value)
                return;

            player = value;
            spriteRenderer.sprite = player.Select(spriteY, spriteR);
        }
    }
    private Player player;

    [SerializeField]
    private Sprite spriteY;

    [SerializeField]
    private Sprite spriteR;

    private TbGame game;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        game = GetComponentInParent<TbGame>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        var dv = game.GetPiecePosition(Position) - transform.position;
        var magnitude = Mathf.Min(Speed * Time.fixedDeltaTime, dv.magnitude);

        transform.Translate(magnitude * dv.normalized);
    }
}
