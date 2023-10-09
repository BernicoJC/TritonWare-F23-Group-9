using UnityEngine;

public class PreviewController : MonoBehaviour
{
    public int Column
    {
        get => column;
        set
        {
            int clamped = Mathf.Clamp(value, 0, game.Dimensions.x - 1);
            if (column == clamped)
                return;
            
            column = clamped;
            transform.position = game.GetPiecePosition(clamped, game.Dimensions.y);
        }
    }
    private int column;

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

    private void Start()
    {
        transform.localScale = game.LocalPieceSize;

        game.OnTurnChange += onTurnChange;
        onTurnChange();
    }

    private void Update()
    {
        string suffix = game.CurrentPlayer.ToSuffix();

        if (Input.GetButtonDown("AxisX" + suffix))
            Column += Mathf.RoundToInt(Input.GetAxisRaw("AxisX" + suffix));
        
        if (Input.GetButtonDown("Attack" + suffix) && game.ColumnTops[Column] < game.Dimensions.y)
            game.Place(Column);
    }

    private void onTurnChange()
    {
        spriteRenderer.sprite = game.CurrentPlayer.Select(spriteY, spriteR);
        Column = game.Dimensions.x / 2;
    }

    private void OnDestroy()
    {
        game.OnTurnChange -= onTurnChange;
    }
}
