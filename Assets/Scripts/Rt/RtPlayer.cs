using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterController2D))]
public class RtPlayer : OwnedObject
{
    [field: SerializeField]
    public float Speed { get; private set; } = 50f;

    [field: SerializeField]
    public int MaxHealth { get; private set; } = 14;

    public ParticleSystem landDustL;
    public ParticleSystem landDustC;
    public ParticleSystem landDustR;

    private float playDuration = 5.0f;

    

    public int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
                Die();
        }
    }
    private int health;

    // [SerializeField]
    // private GameObject deathEffect;
    private CharacterController2D controller;

    private float moveX;
    private bool isJumping;
    private bool isDropping;


    private RtGame game;



    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController2D>();
        game = GetComponentInParent<RtGame>();
    }

    private void Start()
    {
        Health = MaxHealth;
        
    }

    private void Update()
    {
        string suffix = ((Player)Owner).ToSuffix();
        moveX = Input.GetAxisRaw("AxisX" + suffix) * Speed;

        
        if (Input.GetAxisRaw("AxisY" + suffix) < 0)
        {
            isDropping = true;
            isJumping = false;
        }
        else if (Input.GetButtonDown("AxisY" + suffix) && Input.GetAxisRaw("AxisY" + suffix) > 0)
        {
            isJumping = true;
            isDropping = false;
        }
    }

    private void FixedUpdate()
    {

        controller.Move(moveX * Time.fixedDeltaTime, isDropping, isJumping);
        isJumping = false;
        isDropping = false;

    }

    public void Die()
    {
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        game.KillPlayer(Owner);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


            StartCoroutine(PlayLandDust());
        
    }


    IEnumerator PlayLandDust()
    {
      //  Instantiate(landDustL);
    //    Instantiate(landDustC);
    //    Instantiate(landDustR);
        landDustL.Play();
        landDustC.Play();
        landDustR.Play();
        yield return new WaitForSeconds(playDuration);
       // Destroy(landDustL);
       // Destroy(landDustC);
      //  Destroy(landDustR);
    }



}
