using System.Collections.Generic;
using UnityEngine;

public class TbPrefabs : MonoBehaviour
{
    [field: SerializeField]
    public GameObject Board { get; private set; }

    [field: SerializeField]
    public Piece Piece { get; private set; }

    [field: SerializeField]
    public GameObject WinningPiece { get; private set; }

    public IReadOnlyList<Powerup> Powerups => powerups;

    [SerializeField]
    private List<Powerup> powerups;
}
