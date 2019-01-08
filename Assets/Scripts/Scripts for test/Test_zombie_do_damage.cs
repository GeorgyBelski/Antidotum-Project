using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_zombie_do_damage : MonoBehaviour
{
    public AudioClip damage;
    private AudioSource audioSource;
    Zombie_script zScript;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        zScript = GetComponent<Zombie_script>();
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