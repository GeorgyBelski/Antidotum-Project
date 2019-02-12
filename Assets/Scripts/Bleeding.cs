using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    ParticleSystem hitParticles;
    ParticleSystem.VelocityOverLifetimeModule velocity;

    void Start()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void BleedByHit(Vector3 position, Vector3 direction)
    {
        if (hitParticles) {
            hitParticles.transform.position = position;

            velocity = hitParticles.velocityOverLifetime;
            velocity.x = direction.x*10f;
            velocity.z = direction.z*10f;
            hitParticles.Play();

        }
    }


}
