using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TbGame))]
public class TbPowerupManager : MonoBehaviour
{
    private TbGame game;
    private TbPrefabs prefabs;

    [SerializeField]
    private List<bool> spawnPattern = new List<bool>();
    private int spawnPatternIndex;

    [SerializeField]
    private Board board;

    private List<int> columns = new List<int>();

    private void Awake()
    {
        game = GetComponent<TbGame>();
        prefabs = GetComponentInParent<TbPrefabs>();

        game.OnBeginTurn += onBeginTurn;
    }

    private void onBeginTurn()
    {
        if (!spawnPattern[spawnPatternIndex % spawnPattern.Count])
        {
            spawnPatternIndex++;
            return;
        }

        if (columns.Count == 0)
        {
            for (int c = 0; c < game.Dimensions.x; c++)
                columns.Add(c);
        }

        int index = (int)(Random.value * columns.Count);
        int column = columns[index];
        columns.RemoveAt(index);
        int row = game.ColumnTops[column] + 1;

        if (row < game.Dimensions.y)
        {
            var powerup = prefabs.Powerups[(int)(Random.value * prefabs.Powerups.Count)];
            board.Instantiate(powerup, column, row);
        }

        spawnPatternIndex++;
    }
}
