using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDoDamage : MonoBehaviour
{
    Zombie_script1 zScript;
    void Start()
    {
        zScript = GetComponent<Zombie_script1>();
    }

    void Update()
    {
        
    }

    void Attack() {
        if (zScript.enemy && zScript.distanceToTarget <= 1.5f) {
            zScript.enemy.SendMessage("ApplyDamage", 10, SendMessageOptions.DontRequireReceiver);
        }
    }
}
