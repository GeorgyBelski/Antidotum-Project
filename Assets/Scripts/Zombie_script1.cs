using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_script1 : MonoBehaviour
{
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
        //Transform tr = GetComponent<enemy.>

        //rg = GetComponent<Rigidbody>();
        zombieAttributes = GetComponent<ZombieAttributes>();
    }

    void Update()
    {
        
    }
    //private float x_displacement, y_displacement;

    void FixedUpdate()
    {
        if (enemy)
        {
            distanceToTarget = (enemy.transform.position - this.transform.position).magnitude;
            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            //    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), speed / 100);
            //transform.position = new Vector3
        }
        else {
            distanceToTarget = float.PositiveInfinity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
         //   Debug.Log("OnCollisionEnter");

         //   move = false;
        //    attack = true;
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
          //  helthBar.fillAmount -= 0.2f;
            zombieAttributes.health -= 20;
            if (zombieAttributes.health <= 0)
            {
                Destroy(gameObject);
            }

        }
        //speed = 0;
        //collision.gameObject.CompareTag("bullet_Tag")
    }
    void OnCollisionExit(Collision collision)
    {

        //if(collision.gameObject.CompareTag("Player")){
        
       // if(distanceToTarget > 1f)
       //     attack = false;
        //}
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
        if (other.gameObject.CompareTag("Human"))
        {
            enemy = other.gameObject;

        }
        else
        {
            if (!enemy)
                enemy = other.gameObject;
        }




    }

    /*void OnTriggerStay(Collider other)
    {
        print("+");
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Human"))
        {
            if (other.gameObject.CompareTag("Human")) {
                enemy = GameObject.Find("Human");
            }
            else
            {
                enemy = player;
            }

            //Zombie_script zomb = collision.gameObject;//GetComponent<collision.gameObject>();
            //collision.fi
            //speed = 0;
        }

        //speed = 0;
        //collision.gameObject.CompareTag("bullet_Tag")
    }
    */

    /*
    void OnTriggerEnter(Collider other)
    {
print("+");
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Human"))
        {
            if (other.gameObject.CompareTag("Human"))
            {
                enemy = GameObject.Find("Human");
            }
            else
            {
                enemy = player;
            }

            //Zombie_script zomb = collision.gameObject;//GetComponent<collision.gameObject>();
            //collision.fi
            //speed = 0;
        }
    }
    */
    void OnTriggerExit(Collider other)
    {
       // print("+");
        //if (other.gameObject.CompareTag("Player"))
        //{

        enemy = null;
        //Zombie_script zomb = collision.gameObject;//GetComponent<collision.gameObject>();
        //collision.fi
        //speed = 0;
        //}
        move = false;
        //collision.gameObject.CompareTag("bullet_Tag")
    }
}
