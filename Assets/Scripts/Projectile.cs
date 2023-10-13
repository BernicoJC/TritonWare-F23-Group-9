using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Bow_Player2 bow_p2 = hitInfo.GetComponent<Bow_Player2>();
        Bow_Player bow_p1 = hitInfo.GetComponent<Bow_Player>();
        if (bow_p2 != null)
        {
            bow_p2.TakeDamage(1);
        }
        Destroy(gameObject);

        if (bow_p1 != null)
        {
            bow_p1.TakeDamage(1);
        }
    }


}
