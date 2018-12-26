using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 7f;
    public Transform coordinator;


    // Rigidbody rb;
    public int floorMask;
    float camRayLength = 60f;

    void Start ()
    {
        floorMask = LayerMask.GetMask("Floor");
      //  rb = GetComponent<Rigidbody>();

    }
	
	void FixedUpdate ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

           transform.position +=  coordinator.transform.rotation * new Vector3(h,0,v).normalized * speed * Time.deltaTime;
        //  rb.velocity = coordinator.transform.rotation * new Vector3(h, 0, v).normalized * speed ;
        Turning();
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
         /* Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
         */
            transform.LookAt(new Vector3(floorHit.point.x, transform.position.y, floorHit.point.z ));
        }
    }

    
}
