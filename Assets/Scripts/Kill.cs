using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(hitInfo.gameObject);
    }
}
