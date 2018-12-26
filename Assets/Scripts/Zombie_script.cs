using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_script : MonoBehaviour {
    public GameObject enemy;
    private float startSpeed;
    //private Rigidbody rg;
    public float speed = 10f;
	// Use this for initialization
	void Start () {
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
        transform.LookAt(new Vector3( enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z) , new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z) , speed/100);
        //transform.position = new Vector3
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Zombie"))
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
}
