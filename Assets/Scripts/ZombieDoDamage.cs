using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDoDamage : MonoBehaviour
{
    public AudioClip damage;
    private AudioSource audioSource;
    Zombie_script1 zScript;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        zScript = GetComponent<Zombie_script1>();
    }

    void Update()
    {

    }

    void Attack()
    {
        if (zScript.enemy && zScript.distanceToTarget <= 1.5f)
        {
            audioSource.PlayOneShot(damage, 0.5f);
            zScript.enemy.SendMessage("ApplyDamage", 10, SendMessageOptions.DontRequireReceiver);
        }
    }
}
