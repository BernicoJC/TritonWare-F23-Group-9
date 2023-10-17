using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem m_ParticleSystem;

    [Range(0, 100)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float FormationPeriod;

    [SerializeField] Rigidbody2D playerRb;

    [SerializeField] ParticleSystem LandingParticleL;
    [SerializeField] ParticleSystem LandingParticleC;
    [SerializeField] ParticleSystem LandingParticleR;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        counter += Time.deltaTime;
        if(Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > FormationPeriod)
            {
                m_ParticleSystem.Play();
                counter = 0;

            }
        }
    }
}
