using UnityEngine;

public class TbController : MonoBehaviour
{
    [SerializeField]
    private PlayerSpriteList sprites;

    public int Column
    {
        get => column;
        set
        {
            int clamped = Mathf.Clamp(value, 0, game.Dimensions.x - 1);
            column = clamped;

            var position = transform.position;
            position.x = board.GetPiecePosition(clamped, game.Dimensions.y).x;
            transform.position = position;
        }
    }
    private int column;

    [field: SerializeField]
    public float TurnDuration { get; private set; } = 2.5f;
    
    private Board board;
    private TbGame game;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        board = GetComponentInParent<Board>();
        game = GetComponentInParent<TbGame>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        game.OnBeginTurn += onBeginTurn;
        game.OnEndTurn += onEndTurn;
    }

    private void Start()
    {
        spriteRenderer.sprite = game.CurrentPlayer.Select(sprites);
        Column = game.Dimensions.x / 2;
    }

    private void Update()
    {
        string suffix = game.CurrentPlayer.ToSuffix();

        if (Input.GetButtonDown("AxisX" + suffix))
            Column += Mathf.RoundToInt(Input.GetAxisRaw("AxisX" + suffix));
        
        if (Input.GetButtonDown("Attack" + suffix) && game.ColumnTops[Column] < game.Dimensions.y)
            game.EndTurn();
    }

    private void onBeginTurn()
    {
        spriteRenderer.sprite = game.CurrentPlayer.Select(sprites);
        Column = game.Dimensions.x / 2;
    }

    private void onEndTurn()
    {
        if (game.ColumnTops[Column] < game.Dimensions.y)
            game.Place(Column);
    }

    private void OnDestroy()
    {
        game.OnBeginTurn -= onBeginTurn;
    }
}
