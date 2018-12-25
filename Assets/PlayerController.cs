using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 7f;
    public Transform coordinator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position +=  coordinator.transform.rotation * 
            ((Input.GetAxisRaw("Horizontal")* Vector3.right + Input.GetAxisRaw("Vertical") * Vector3.forward).normalized * speed * Time.deltaTime);
    }
}
