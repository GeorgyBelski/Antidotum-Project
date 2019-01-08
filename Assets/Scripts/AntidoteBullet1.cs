﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidoteBullet1 : MonoBehaviour
{
    public static int floorLayer = 9;
    public AudioClip wizzing;
    private AudioSource audioSource;
    public float speed = 50f;
    public float life_time = 1.3f;
    Rigidbody rb;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    private void Update()
    {

        life_time -= Time.deltaTime;
        if (life_time < 0)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
      //  transform.position += transform.forward * speed * Time.deltaTime;
          

    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        { 
            Destroy(gameObject);
            //  Debug.Log("collision.gameObject.layer == floorLayer");
        }
        else
        {
            if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet"))
            //Destroy(gameObject);
            audioSource.PlayOneShot(wizzing, 0.3f);
        }
    }
    
}
