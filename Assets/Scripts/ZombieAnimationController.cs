﻿using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{

    Animator animator;
    Vector3 rootPosition;
    ZombieAttributes zAttributes;
    Zombie_script1 zScript;
    bool isMove = false;
    bool isAttack = false;
    bool hasCured = false;
    bool isInjured = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        zAttributes = GetComponent<ZombieAttributes>();
        animator.SetFloat("speedMultiplier", Random.Range(1f,1.8f));
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

        if (!hasCured && zAttributes.isCured)
        {
            hasCured = true;
            animator.SetBool("isCured", hasCured);
        }
        if(!isInjured && zAttributes.isInjured)
        {
            isInjured = true;
            animator.SetBool("isInjured", isInjured);
        }
    }
}
