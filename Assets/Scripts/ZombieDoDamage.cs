using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDoDamage : MonoBehaviour
{
    public AudioClip damage;
    private AudioSource audioSource;
    Zombie_script1 zScript;
    object[] message = new object[3];

    IDamageable damageable;

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
          //  zScript.enemy.SendMessage("ApplyDamage", 15, SendMessageOptions.DontRequireReceiver);
            damageable = zScript.enemy.GetComponent<ZombieAttributes>();
            if (damageable == null)
            {
                damageable = zScript.enemy.GetComponent<PlayerAttributes>();
            }
            damageable.ApplyDamage(15, zScript.enemy.transform.position + Vector3.up, Vector3.zero);
        }
    }
}
