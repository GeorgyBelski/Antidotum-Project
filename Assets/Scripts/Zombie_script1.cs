using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_script1 : MonoBehaviour {
    public GameObject enemy;
    private GameObject player;
    private float startSpeed;
    //private Rigidbody rg;
    public float speed = 10f;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        //Transform tr = GetComponent<enemy.>
        startSpeed = speed;
        //rg = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        
		
	}
    //private float x_displacement, y_displacement;

    void FixedUpdate()
    {
        if (enemy) { 
            transform.LookAt(new Vector3( enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z) , new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z) , speed/100);
            //transform.position = new Vector3
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            speed = 0;
        }
        //speed = 0;
        //collision.gameObject.CompareTag("bullet_Tag")
    }
    void OnCollisionExit(Collision collision)
    {
        
        //if(collision.gameObject.CompareTag("Player")){
            speed = startSpeed;
        //}
    }
    void OnTriggerEnter(Collider other)
    {
        //print("+");
        if (other.gameObject.CompareTag("Player"))
        {
            
            enemy = player;
            //Zombie_script zomb = collision.gameObject;//GetComponent<collision.gameObject>();
            //collision.fi
            //speed = 0;
        }
        //speed = 0;
        //collision.gameObject.CompareTag("bullet_Tag")
    }
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
        //speed = 0;
        //collision.gameObject.CompareTag("bullet_Tag")
    }
}
