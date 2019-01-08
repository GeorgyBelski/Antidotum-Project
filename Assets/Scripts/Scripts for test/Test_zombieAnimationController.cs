using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_zombieAnimationController : MonoBehaviour
{

    Animator animator;
    Vector3 rootPosition;
    Test_zombie_atributes zAttributes;
    Zombie_script zScript;
    bool isMove = false;
    bool isAttack = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        zAttributes = GetComponent<Test_zombie_atributes>();
        animator.SetFloat("speedMultiplier", Random.Range(1f, 2f));
        zScript = GetComponent<Zombie_script>();
    }

    void FixedUpdate()
    {

        //  Vector3 rootPosition = animator.rootPosition;
        //   position.y = agent.nextPosition.y;
    }

    void OnAnimatorMove()
    {
        if (isMove != zScript.move)
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

        if (zAttributes.isCured)
        {
            animator.SetBool("isCured", true);
        }
    }
}
