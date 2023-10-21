using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl;									// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

	public bool IsGrounded => m_Grounded;

	const float k_GroundedRadius = 0.2f;                                        // Radius of the overlap circle to determine if grounded

	private bool m_Grounded;													// Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private Collider2D m_Collider2D;
	private Vector3 m_DampingVelocity = Vector3.zero;

	private HashSet<Collider2D> m_TouchingColliders = new HashSet<Collider2D>();
	private HashSet<Collider2D> m_IgnoreColliders = new HashSet<Collider2D>();
	private bool m_IsDropping;
    public AudioSource jumpNoise;

    [Header("Events")] 
	[Space]

    public UnityEvent OnLandEvent;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Collider2D = GetComponent<Collider2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		bool wasDropping = m_IsDropping;
		m_IsDropping = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		var colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

		if (colliders.Length > 0)
		{
			var highestCollider = colliders[0];
			foreach (var collider in colliders)
			{
				if (!m_TouchingColliders.Contains(collider) && m_IgnoreColliders.Contains(collider))
				{
					Physics2D.IgnoreCollision(m_Collider2D, collider, false);
					m_IgnoreColliders.Remove(collider);
				}

				if (collider.bounds.max.y > highestCollider.bounds.max.y && !m_IgnoreColliders.Contains(collider))
					highestCollider = collider;
			}

			if (wasDropping && highestCollider.tag == "Platform")
			{
				Physics2D.IgnoreCollision(m_Collider2D, highestCollider, true);
				m_IgnoreColliders.Add(highestCollider);
			}

			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
				{
					m_Grounded = true;

                    if (!wasGrounded)
						OnLandEvent.Invoke();
						
                }
			}
		}

		m_TouchingColliders.Clear();
		m_TouchingColliders.AddRange(colliders);
	}

	public void Move(float move, bool drop, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
			{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_DampingVelocity, m_MovementSmoothing);

			if (move != 0)
				transform.rotation = Quaternion.Euler(0, move > 0 ? 0 : 180, 0);
		}

		// If the player should jump...
		if (m_Grounded && jump)
		{

			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			jumpNoise.Play();
		}

		if (m_Grounded && drop)
			m_IsDropping = true;
			
	}
}
