using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RtPrefabs), typeof(Timer))]
public class RtGame : MonoBehaviour
{
    [field: SerializeField]
    public float RoundDuration { get; private set; } = 30f;

    public event Action OnRoundChange;
    public event Action<Player> OnWin;

    private Timer roundTimer;
    private HashSet<Player> alivePlayers;

    string[] powerupArray = new string[3];
    string advSelec;


    string advantage = "1";

    private void Awake()
    {
        roundTimer = GetComponent<Timer>();

        roundTimer.OnDone += onTimerDone;
    }

    private void Start()
    {
        roundTimer.StartTimer(RoundDuration);

        alivePlayers = new HashSet<Player>();
        for (Player p = 0; p < Player.Count; p++)
            alivePlayers.Add(p);
        /*powerupArray[0] = "RemovePiece";
        powerupArray[1] = "RemoveRow";
        powerupArray[2] = "MoveToken";
        powerupArray[3] = "AddColorlessToken";*/
    }

    public void KillPlayer(Player p)
    {
        alivePlayers.Remove(p);

        if (alivePlayers.Count == 1)
            OnWin?.Invoke(alivePlayers.First());

        if (alivePlayers.Count == 0)
            OnWin?.Invoke(Player.None);
    }

    private void OnDestroy()
    {
        roundTimer.OnDone -= onTimerDone;
    }

    private void onTimerDone()
    {
        if(advantage!= null)
        {
            giveAdvantageRT(advantage);
        }
        OnRoundChange?.Invoke();
        roundTimer.StartTimer(RoundDuration);
    }

    private void giveAdvantageRT(string adv)
    {
        if(adv == "1")
        {
            advSelec = (powerupArray[UnityEngine.Random.Range(0, 3)]);
            if(advSelec == "RemovePiece")
            {

            }
            else if(advSelec == "RemoveRow")
            {

            }
            else if (advSelec == "MoveToken")
            {

            }
            else if (advSelec == "AddColorlessToken")
            {

            }

            Debug.Log(advSelec);
        }
        else if(adv == "2")
        {
            advSelec = (powerupArray[UnityEngine.Random.Range(0, 3)]);
            if (advSelec == "RemovePiece")
            {

            }
            else if (advSelec == "RemoveRow")
            {

            }
            else if (advSelec == "MoveToken")
            {

            }
            else if (advSelec == "AddColorlessToken")
            {

            }
        }
        else
        {

        }
    }


}
