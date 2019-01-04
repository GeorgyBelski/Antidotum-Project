using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    public float zSpeed;



    Animator animator;
    Vector3 velocity;
    Vector3 rootPosition;
    Zombie_script1 zScript;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("speedMultiplier", Random.RandomRange(1f,2f));
        velocity = transform.position;
        zScript = GetComponent<Zombie_script1>();
    }

    void FixedUpdate()
    {
        
      //  Vector3 rootPosition = animator.rootPosition;
        //   position.y = agent.nextPosition.y;
    }

    void OnAnimatorMove()
    {
        zSpeed = zScript.speed;
        bool move = zScript.speed > 0.01f;
        animator.SetBool("move", move);
        // Update position based on animation movement using navigation surface height
        rootPosition = animator.rootPosition;
     //   position.y = agent.nextPosition.y;
        transform.position = rootPosition;
    }
}
