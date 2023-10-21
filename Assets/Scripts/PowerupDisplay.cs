using UnityEngine;

public class PowerupDisplay : OwnedObject
{
    [field: SerializeField]
    public bool IsTb { get; private set; }

    [SerializeField]
    private int powerupsPerRow = 5;

    Vector2Int position = Vector2Int.zero;

    public void AddPowerup(Powerup powerup)
    {
        var offset = position + new Vector2(0.5f, -0.5f);
        powerup.gameObject.transform.SetParent(transform);
        powerup.gameObject.transform.localPosition = offset;
        powerup.gameObject.transform.localRotation = Quaternion.identity;
        powerup.gameObject.transform.localScale = new Vector3(1 / 1.4f, 1 / 1.4f, 1f);

        position.x++;
        if (position.x >= powerupsPerRow)
        {
            position.x -= powerupsPerRow;
            position.y--;
        }
    }
}
