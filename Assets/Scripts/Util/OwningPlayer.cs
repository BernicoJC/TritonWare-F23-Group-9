using System;
using UnityEngine;

public class OwningPlayer : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public event Action OnSet;

    public static implicit operator Player(OwningPlayer op) => op?.player ?? Player.None;

    public static explicit operator int(OwningPlayer op) => (int)(op?.player ?? Player.None);

    public static bool operator ==(OwningPlayer left, OwningPlayer right) => (Player)left== (Player)right;

    public static bool operator !=(OwningPlayer left, OwningPlayer right) => !(left == right);

    private void Start()
    {
        OnSet?.Invoke();
    }

    private void OnValidate()
    {
        OnSet?.Invoke();
    }

    public void Set(Player player)
    {
        this.player = player;
        OnSet?.Invoke();
    }

    public override bool Equals(object obj)
    {
        return obj is OwningPlayer player &&
               base.Equals(obj) &&
               this.player == player.player;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), player);
    }
}
