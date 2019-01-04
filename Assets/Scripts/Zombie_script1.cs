﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_script1 : MonoBehaviour
{
    public GameObject enemy;
    private GameObject player;
    public Image helthBar;
    private float startSpeed;
    //private Rigidbody rg;
    public float speed = 10f;
    public bool attack = false;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        //Transform tr = GetComponent<enemy.>
        startSpeed = speed;
        speed = 0f;
        //rg = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {


    }
    //private float x_displacement, y_displacement;

    void FixedUpdate()
    {
        if (enemy)
        {
            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
        //    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), speed / 100);
            //transform.position = new Vector3
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
         //   Debug.Log("OnCollisionEnter");
            speed = 0;
            attack = true;
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            helthBar.fillAmount -= 0.2f;
            if (helthBar.fillAmount <= 0.01)
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
        attack = false;
        //}
    }
    void OnTriggerStay(Collider other)
    {
        if(!attack)
            speed = startSpeed;
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
        speed = 0;
        //collision.gameObject.CompareTag("bullet_Tag")
    }
}
