using UnityEngine;

public class Piece : OwnedObject, INeverDisabled
{
    [field: SerializeField]
    public float Speed { get; private set; }

    public Vector2Int Position { get; set; }

    private Board board;

    protected override void Awake()
    {
        base.Awake();
        board = GetComponentInParent<Board>();
    }

    private void FixedUpdate()
    {
        var dv = board.GetPiecePosition(Position) - transform.position;
        var magnitude = Mathf.Min(Speed * Time.fixedDeltaTime, dv.magnitude);

        transform.Translate(magnitude * dv.normalized);
    }
}
