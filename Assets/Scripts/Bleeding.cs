using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    ParticleSystem hitPartocles;

    void Start()
    {
        hitPartocles =GetComponentInChildren<ParticleSystem>();
    }

    public void BleedByHit(Vector3 position, Vector3 direction)
    {
        if (hitPartocles) {
            hitPartocles.transform.position = position;
            hitPartocles.Play();
        }
    }
}
