using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TurnText : MonoBehaviour
{
    // TODO: this dependency feels a little bad, maybe fix it somehow?
    [SerializeField]
    private RoundManager roundManager;

    private TbGame game;

    private TextMeshProUGUI tmp;

    private void Awake()
    {
        game = GetComponentInParent<TbGame>();
        tmp = GetComponent<TextMeshProUGUI>();
        game.OnBeginTurn += onBeginTurn;
    }

    private void onBeginTurn()
    {
        int round = game.Turn / (int)Player.Count;
        tmp.text = $"{ round % roundManager.TbRounds + 1} / { roundManager.TbRounds }";
    }
}
