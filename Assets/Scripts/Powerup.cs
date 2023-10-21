using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Powerup : OwnedObject, INeverDisabled
{
    protected abstract bool IsTb { get; }

    protected abstract void OnPickedUp();

    private void Start()
    {
        Owner.Set(Player.None);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<OwningPlayer>();
        if (player == Player.None)
            return;

        var displays = FindObjectsOfType<PowerupDisplay>();
        displays.First(d => d.Owner == player && d.IsTb != IsTb).AddPowerup(this);

        var piece = GetComponent<Piece>();
        if (piece != null)
            piece.IsMoving = false;

        Owner.Set(player);
        OnPickedUp();
    }
}
