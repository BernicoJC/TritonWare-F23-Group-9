using System;
using System.Collections.Generic;
using UnityEngine;

public class TbGame : MonoBehaviour
{
    [field: SerializeField]
    public Vector2Int Dimensions { get; private set; } = new Vector2Int(7, 6);

    [field: SerializeField]
    public int WinLength { get; private set; } = 4;

    public IReadOnlyList<IReadOnlyList<Piece>> Board => board;
    private Piece[][] board;

    public IReadOnlyList<int> ColumnTops => columnTops;
    private int[] columnTops;

    public event Action OnTurnChange;

    public int Turn
    {
        get => turn;

        private set
        {
            if (turn == value)
                return;

            turn = value;
            OnTurnChange?.Invoke();
            turnTimer.StartTimer();
        }
    }
    private int turn;

    public Player CurrentPlayer => (Player)(turn % (int)Player.Count);

    public Vector3 LocalPieceSize => new Vector3(1f / Dimensions.x, 1f / Dimensions.y);
    public Vector3 PieceSize => Vector3.Scale(LocalPieceSize, transform.localScale);

    [SerializeField]
    private Timer turnTimer;
    private TbPrefabs prefabs;

    private void Awake()
    {
        prefabs = GetComponentInParent<TbPrefabs>();
    }

    private void Start()
    {
        Turn = 0;
        turnTimer.OnDone += onTurnTimerDone;
        turnTimer.StartTimer();

        board = new Piece[Dimensions.x][];
        columnTops = new int[Dimensions.x];
        for (int c = 0; c < Dimensions.x; c++)
        {

            board[c] = new Piece[Dimensions.y];
            for (int r = 0; r < Dimensions.y; r++)
                board[c][r] = null;

            columnTops[c] = 0;
        }
    }

    public Vector3 GetPiecePosition(int c, int r) => GetPiecePosition(new Vector2Int(c, r));

    public Vector3 GetPiecePosition(Vector2Int position)
    {
        var bottomLeft = (transform.localScale - PieceSize) / 2;
        var offset = Vector3.Scale(PieceSize, new Vector3(position.x, position.y));
        return transform.position - bottomLeft + offset;
    }

    public void Place(int column)
    {
        if (columnTops[column] >= Dimensions.y)
            throw new ArgumentException($"Column {column} is full", nameof(column));
        
        var piece = Instantiate(prefabs.Piece, transform);
        piece.transform.localScale = LocalPieceSize;
        piece.transform.position = GetPiecePosition(column, Dimensions.y);
        piece.Player = CurrentPlayer;

        var position = new Vector2Int(column, columnTops[column]);
        Insert(piece, position);

        Turn++;
    }

    public void Insert(Piece piece, int c, int r) => Insert(piece, new Vector2Int(c, r));

    public void Insert(Piece piece, Vector2Int position)
    {
        while (position.y > 0 && board[position.x][position.y - 1] == null)
            position.y--;

        piece.Position = position;
        board[position.x][position.y] = piece;

        while (columnTops[position.x] < Dimensions.y && board[position.x][columnTops[position.x]] != null)
            columnTops[position.x]++;

        checkWin(position);
    }

    private void checkWin(Vector2Int position)
    {
        var pieces = getWinningPieces(position);
        if (pieces.Count == 0)
            return;

        foreach (var piece in pieces)
        {
            var wp = Instantiate(prefabs.WinningPiece, transform);
            wp.transform.position = GetPiecePosition(piece.Position);
            wp.transform.localScale = LocalPieceSize;
        }
    }

    private HashSet<Piece> getWinningPieces(Vector2Int position)
    {
        var pieces = get(Vector2Int.up);
        if (pieces.Count > 0)
            return pieces;

        pieces = get(Vector2Int.right);
        if (pieces.Count > 0)
            return pieces;

        pieces = get(Vector2Int.one);
        if (pieces.Count > 0)
            return pieces;

        return get(new Vector2Int(1, -1));

        HashSet<Piece> get(Vector2Int dv)
        {
            int[] counts = new int[(int)Player.Count];
            var pieces = new HashSet<Piece>();

            for (int left = -2 * WinLength + 1; left < 0; left++)
            {
                var leftPos = position + left * dv;
                var rightPos = leftPos + WinLength * dv;

                if (left > -WinLength && leftPos.x >= 0 && leftPos.y >= 0 && leftPos.x < Dimensions.x && leftPos.y < Dimensions.y)
                {
                    var piece = board[leftPos.x][leftPos.y];
                    if (piece != null)
                    {
                        pieces.Remove(piece);
                        counts[(int)piece.Player]--;
                    }
                }

                if (rightPos.x >= 0 && rightPos.y >= 0 && rightPos.x < Dimensions.x && rightPos.y < Dimensions.y)
                {
                    var piece = board[rightPos.x][rightPos.y];
                    if (piece != null)
                    {
                        pieces.Add(piece);
                        counts[(int)piece.Player]++;
                        if (counts[(int)piece.Player] >= WinLength)
                            return pieces;
                    }
                }
            }

            return new HashSet<Piece>();
        }
    }

    private void onTurnTimerDone() => Turn++;
}
