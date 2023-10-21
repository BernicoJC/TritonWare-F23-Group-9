using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Piece : OwnedObject, INeverDisabled
{
    [field: SerializeField]
    public float Speed { get; private set; } = 32;

    public Vector2Int Position { get; set; }

    public bool IsMoving { get; set; } = true;

    private Board board;
    private new Collider2D collider;

    protected override void Awake()
    {
        base.Awake();
        board = GetComponentInParent<Board>();
        collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!IsMoving)
        {
            collider.enabled = false;
            return;
        }

        var dv = board.GetPiecePosition(Position) - transform.position;
        var magnitude = Mathf.Min(Speed * Time.fixedDeltaTime, dv.magnitude);

        if (magnitude < 0.01)
            collider.enabled = true;
        else
            collider.enabled = false;

        transform.Translate(magnitude * dv.normalized);
    }
}
