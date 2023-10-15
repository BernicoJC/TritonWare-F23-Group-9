using UnityEngine;

public class Board : MonoBehaviour
{
    private TbGame game;
    private TbPrefabs prefabs;

    public Vector3 LocalPieceSize => new Vector3(1f / game.Dimensions.x, 1f / game.Dimensions.y);
    public Vector3 PieceSize => Vector3.Scale(LocalPieceSize, transform.localScale);

    public Vector3 GetPiecePosition(int c, int r) => GetPiecePosition(new Vector2Int(c, r));

    public Vector3 GetPiecePosition(Vector2Int position)
    {
        var bottomLeft = (transform.localScale - PieceSize) / 2;
        var offset = Vector3.Scale(PieceSize, new Vector3(position.x, position.y));
        return transform.position - bottomLeft + offset;
    }

    private void Awake()
    {
        game = GetComponentInParent<TbGame>();
        prefabs = GetComponentInParent<TbPrefabs>();
    }

    private void Start()
    {
        for (int r = 0; r < game.Dimensions.y; r++)
        {
            for (int c = 0; c < game.Dimensions.x; c++)
            {
                var piece = Instantiate(prefabs.Board, c, r);
                piece.transform.localScale = LocalPieceSize;
            }
        }
    }

    public T Instantiate<T>(T original, int c, int r) where T : MonoBehaviour 
        => Instantiate(original, new Vector2Int(c, r));

    public T Instantiate<T>(T original, Vector2Int position) where T : MonoBehaviour
    {
        var obj = Instantiate(original, transform);
        obj.transform.localScale = LocalPieceSize;
        obj.transform.position = GetPiecePosition(position);
        return obj;
    }

    public GameObject Instantiate(GameObject original, int c, int r)
        => Instantiate(original, new Vector2Int(c, r));

    public GameObject Instantiate(GameObject original, Vector2Int position)
    {
        var obj = Instantiate(original, transform);
        obj.transform.localScale = LocalPieceSize;
        obj.transform.position = GetPiecePosition(position);
        return obj;
    }

    private void OnDrawGizmosSelected()
    {
        game ??= GetComponentInParent<TbGame>();
        var max = game.Dimensions.x + game.Dimensions.y - 2;

        for (int r = 0; r < game.Dimensions.y; r++)
        {
            for (int c = 0; c < game.Dimensions.x; c++)
            {
                var color = (Color.white * (r + c) / max).linear;
                color.a = 1f;

                Gizmos.color = color;
                Gizmos.DrawCube(GetPiecePosition(c, r), PieceSize);
            }
        }
    }
}
