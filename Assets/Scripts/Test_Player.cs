using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player : MonoBehaviour
{
    private ShotGunFire s_Fire;
    private int type = 1;
    private float pistolShotRealTime;
    private float shotGunShotRealTime;
    private float smgShotRealTime;

    public GameObject gun;
    public float smgShotTime = 0.15f;
    public float animationDamping = 0.15f;
    public float pistolShotTime = 0.5f;
    public float shotGunShotTime = 1f;
    public float speed = 7f;

    public Transform coordinator;
    public GameObject bulletPrefab;
    
    public int floorMask;
    // Rigidbody rb;
    
    float camRayLength = 60f;
    Animator animator;

    void Start()
    {
        pistolShotRealTime = pistolShotTime;
        shotGunShotRealTime = shotGunShotTime;
        smgShotRealTime = smgShotTime;
        s_Fire = new ShotGunFire(this, bulletPrefab);
        floorMask = LayerMask.GetMask("Floor");
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        pistolShotRealTime -= Time.deltaTime;
        shotGunShotRealTime -= Time.deltaTime;
        smgShotRealTime -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            fire();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            type = 1;
            //print(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            type = 2;
            //print(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            type = 3;
            //print(1);
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        var inputAxisVector = new Vector3(h, 0, v);
        transform.position += coordinator.transform.rotation * inputAxisVector.normalized * speed * Time.deltaTime;
        //  rb.velocity = coordinator.transform.rotation * new Vector3(h, 0, v).normalized * speed ;
        Turning();
        if (animator != null)
        {
            var angleHorizontalAxisVSPlayerForward = coordinator.transform.rotation * Quaternion.Inverse(transform.rotation);
            var animatorDirection = angleHorizontalAxisVSPlayerForward * inputAxisVector;
            animator.SetFloat("Right", animatorDirection.x, animationDamping, Time.deltaTime);
            animator.SetFloat("Forward", animatorDirection.z, animationDamping, Time.deltaTime);
        }
       
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
            transform.LookAt(new Vector3(floorHit.point.x, transform.position.y, floorHit.point.z));
        }
    }

    void fire()
    {
        switch (type)
        {
            case 1:
                //pistolShotRealTime -= Time.deltaTime;
                if(pistolShotRealTime < 0) { 
                    Instantiate(bulletPrefab, new Vector3(
                    gun.transform.position.x,
                    gun.transform.position.y, gun.transform.position.z),
                    gun.transform.rotation, null
                    );
                    pistolShotRealTime = pistolShotTime;
                }
                break;
            case 2:
                if (shotGunShotRealTime < 0)
                {
                    s_Fire.fire(10);
                    shotGunShotRealTime = shotGunShotTime;
                }
                break;
            case 3:
                if (smgShotRealTime < 0)
                {
                    Instantiate(bulletPrefab, new Vector3(
                    gun.transform.position.x,
                    gun.transform.position.y + 1, gun.transform.position.z),
                    gun.transform.rotation, null
                    );
                    smgShotRealTime = smgShotTime;
                }
                break;
            default:
                Instantiate(bulletPrefab, new Vector3(
                    gun.transform.position.x,
                    gun.transform.position.y + 1, gun.transform.position.z),
                    gun.transform.rotation, null
                    );
                break;

        }
        
    }

}
