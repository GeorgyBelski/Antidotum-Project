using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_script1 : MonoBehaviour
{
    public GameObject enemy;
    public Image helthBar;
    public bool attack = false;
    public bool move = false;
    public float distanceToTarget;

    GameObject player;
    ZombieAttributes zombieAttributes;

    void Start()
    {
        player = GameObject.Find("Player");
        zombieAttributes = GetComponent<ZombieAttributes>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (enemy)
        {
            distanceToTarget = (enemy.transform.position - this.transform.position).magnitude;
            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
        }
        else
        {
            distanceToTarget = float.PositiveInfinity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Layers.bullet)
        {
            Destroy(collision.gameObject);
            zombieAttributes.getSound();
        }
        if (collision.gameObject.layer == Layers.antidotBullet)
        {
            Destroy(collision.gameObject);
            zombieAttributes.Recover();
            this.enabled = false;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (distanceToTarget > 1f)
        {
            attack = false;
            move = true;
        }
        else
        {
            attack = true;
            move = false;
        }
        if (other.gameObject.CompareTag("Human") || other.gameObject.layer == Layers.player)
        {
            enemy = other.gameObject;
        }
    }


    void OnTriggerExit(Collider other)
    {
        enemy = null;
        move = false;
    }
}

