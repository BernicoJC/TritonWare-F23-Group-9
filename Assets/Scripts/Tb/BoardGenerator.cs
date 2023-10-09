using Unity.VisualScripting;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    private TbGame game;
    private TbPrefabs prefabs;

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
                var piece = Instantiate(prefabs.Board, game.GetPiecePosition(c, r), Quaternion.identity, game.transform);
                piece.transform.localScale = game.LocalPieceSize;
            }
        }
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
                Gizmos.DrawCube(game.GetPiecePosition(c, r), game.PieceSize);
            }
        }
    }
}
