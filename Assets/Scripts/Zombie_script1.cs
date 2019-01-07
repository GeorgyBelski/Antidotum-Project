using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_script1 : MonoBehaviour
{
    public static int playerLayer = 10;
    public static int antidotBulletLayer = 11;


    public GameObject DropAfterDead;
    public GameObject enemy;
    public Image helthBar;
    public bool attack = false;
    public bool move =false;
    public float distanceToTarget;

    private GameObject player;
    private float startSpeed;
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
        else {
            distanceToTarget = float.PositiveInfinity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
        if (collision.gameObject.CompareTag("Bullet"))
        {

            zombieAttributes.health -= 20;
            if (zombieAttributes.health <= 0)
            {
                Destroy(gameObject);
                Instantiate(DropAfterDead, this.transform.position, this.transform.rotation, null);
            }

        }
        if (collision.gameObject.layer == antidotBulletLayer) {
            zombieAttributes.isCured = true;
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
        if (other.gameObject.CompareTag("Human") || other.gameObject.layer == playerLayer)
        {
            enemy = other.gameObject;

        }
      /*  else
        {
            if (!enemy)
                enemy = other.gameObject;
        }*/
    }


    void OnTriggerExit(Collider other)
    {
        enemy = null;
        move = false;
    }
}
