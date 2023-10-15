using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Timer), typeof(TbPrefabs))]
public class TbGame : MonoBehaviour
{
    [field: SerializeField]
    public Vector2Int Dimensions { get; private set; } = new Vector2Int(7, 6);

    [field: SerializeField]
    public int WinLength { get; private set; } = 4;

    [field: SerializeField]
    public float TurnDuration { get; private set; } = 2.25f;

    [SerializeField]
    private Board board;

    public IReadOnlyList<IReadOnlyList<Piece>> BoardState => boardState;
    private Piece[][] boardState;

    public IReadOnlyList<int> ColumnTops => columnTops;
    private int[] columnTops;

    public event Action OnBeginTurn;
    public event Action OnEndTurn;
    public event Action<Player> OnWin;

    public int Turn
    {
        get => turn;

        private set
        {
            if (turn == value)
                return;
            
            OnEndTurn?.Invoke();
            turn = value;
            turnTimer.StartTimer(TurnDuration);
            OnBeginTurn?.Invoke();
        }
    }
    private int turn;

    public Player CurrentPlayer => (Player)(turn % (int)Player.Count);
    
    private Timer turnTimer;
    private TbPrefabs prefabs;

    private void Awake()
    {
        turnTimer = GetComponent<Timer>();
        prefabs = GetComponent<TbPrefabs>();

        turnTimer.OnDone += EndTurn;
    }

    private void Start()
    {
        boardState = new Piece[Dimensions.x][];
        columnTops = new int[Dimensions.x];
        for (int c = 0; c < Dimensions.x; c++)
        {

            boardState[c] = new Piece[Dimensions.y];
            for (int r = 0; r < Dimensions.y; r++)
                boardState[c][r] = null;

            columnTops[c] = 0;
        }

        turnTimer.StartTimer(TurnDuration);
        OnBeginTurn?.Invoke();
    }

    private void OnDestroy()
    {
        turnTimer.OnDone -= EndTurn;
    }

    public void EndTurn()
    {
        Turn++;
    }

    public void Place(int column)
    {
        if (columnTops[column] >= Dimensions.y)
            throw new ArgumentException($"Column {column} is full", nameof(column));
        
        var piece = board.Instantiate(prefabs.Piece, column, Dimensions.y);
        piece.Owner.Set(CurrentPlayer);

        var position = new Vector2Int(column, columnTops[column]);
        Insert(piece, position);
    }

    public void Insert(Piece piece, int c, int r) => Insert(piece, new Vector2Int(c, r));

    public void Insert(Piece piece, Vector2Int position)
    {
        while (position.y > 0 && boardState[position.x][position.y - 1] == null)
            position.y--;

        piece.Position = position;
        boardState[position.x][position.y] = piece;

        while (columnTops[position.x] < Dimensions.y && boardState[position.x][columnTops[position.x]] != null)
            columnTops[position.x]++;

        checkWin(position);
    }

    private void checkWin(Vector2Int position)
    {
        var pieces = getWinningPieces(position);
        if (pieces.Count == 0)
            return;

        foreach (var piece in pieces)
            board.Instantiate(prefabs.WinningPiece, piece.Position);

        OnWin?.Invoke(pieces.First().Owner);
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
                    var piece = boardState[leftPos.x][leftPos.y];
                    if (piece != null)
                    {
                        pieces.Remove(piece);
                        counts[(int)piece.Owner]--;
                    }
                }

                if (rightPos.x >= 0 && rightPos.y >= 0 && rightPos.x < Dimensions.x && rightPos.y < Dimensions.y)
                {
                    var piece = boardState[rightPos.x][rightPos.y];
                    if (piece != null)
                    {
                        pieces.Add(piece);
                        counts[(int)piece.Owner]++;
                        if (counts[(int)piece.Owner] >= WinLength)
                            return pieces;
                    }
                }
            }

            return new HashSet<Piece>();
        }
    }
}
