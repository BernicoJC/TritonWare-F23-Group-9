using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RtPrefabs), typeof(Timer))]
public class RtGame : MonoBehaviour
{
    [field: SerializeField]
    public float RoundDuration { get; private set; } = 30f;

    public event Action OnRoundEnd;
    public event Action<Player> OnWin;

    private Timer roundTimer;
    private HashSet<Player> alivePlayers;

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
        OnRoundEnd?.Invoke();
        roundTimer.StartTimer(RoundDuration);
    }
}
