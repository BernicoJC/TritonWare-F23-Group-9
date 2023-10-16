using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(hitInfo.gameObject);
    }
}
