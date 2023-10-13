using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_Player : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 50f;

    public int health = 7;

    float horizontalMove = 0f;

    bool jump = false;


    public Health_Bar healthbar;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        healthbar.setMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("DamageTest"))
        {
            TakeDamage(1);
        }
        
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthbar.SetHealth(health);

        if (health <= 0)
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
