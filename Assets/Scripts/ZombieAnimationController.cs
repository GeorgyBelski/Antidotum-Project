using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{

    Animator animator;
    Vector3 rootPosition;
    Zombie_script1 zScript;
    bool isMove = false;
    bool isAttack = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("speedMultiplier", Random.RandomRange(1f,2f));
        zScript = GetComponent<Zombie_script1>();
    }

    void FixedUpdate()
    {
        
      //  Vector3 rootPosition = animator.rootPosition;
        //   position.y = agent.nextPosition.y;
    }

    void OnAnimatorMove()
    {
        if(isMove != zScript.move)
        {
            isMove = zScript.move;
            animator.SetBool("move", isMove);
        }
        // Update position based on animation movement using navigation surface height
        rootPosition = animator.rootPosition;
     //   position.y = agent.nextPosition.y;
        transform.position = rootPosition;

        if (isAttack != zScript.attack)
        {
            isAttack = zScript.attack;
            animator.SetBool("attack", isAttack);
        }
    }
}
