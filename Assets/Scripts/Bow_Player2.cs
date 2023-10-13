using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bow_Player2 : MonoBehaviour
{

    public CharacterController2DPlayer2 controller2;

    public float runSpeed2 = 50f;

    public int health = 7;


    float horizontalMove2 = 0f;

    bool jump2 = false;

    public Health_Bar healthbar2;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        healthbar2.setMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove2 = Input.GetAxisRaw("Horizontal2") * runSpeed2;
        if (Input.GetButtonDown("Jump2"))
        {
            jump2 = true;
        }

        if (Input.GetButtonDown("DamageTest"))
        {
            TakeDamage(1);
        }

    }

    void FixedUpdate()
    {
        controller2.Move(horizontalMove2 * Time.fixedDeltaTime, false, jump2);
        jump2 = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthbar2.SetHealth(health);

        if (health <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

}
