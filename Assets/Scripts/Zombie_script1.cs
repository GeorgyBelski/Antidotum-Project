using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_script1 : MonoBehaviour
{


  /*  
    public GameObject soundBox_dead;
    public GameObject DropAfterDead;
    public GameObject DropAfterDeadItems;
    */
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
         //   zombieAttributes.health -= 20;
            zombieAttributes.getSound();
            

        }
        if (collision.gameObject.layer == Layers.antidotBullet)
        {
            zombieAttributes.isCured = true;
            Destroy(collision.gameObject);
        }

    }
    void OnCollisionExit(Collision collision)
    {

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

