using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    public float animationDamping = 0.15f;

    Animator animator;
    Vector3 velocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        velocity = transform.position;
    }

    void FixedUpdate()
    {
        bool move = (velocity - transform.position).magnitude > 0;
        animator.SetBool("move", move);
        velocity = transform.position;
    }
}
