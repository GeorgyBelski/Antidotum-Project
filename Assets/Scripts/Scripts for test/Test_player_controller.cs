using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_player_controller : MonoBehaviour
{
    public float speed = 7f;

    public float animationDamping = 0.15f;

    public AudioClip leftStep;
    public AudioClip rightStep;
    private AudioSource audioSource;
    private float stepTime = 0.3f;
    private float realstepTime;
    private bool rightstep = true;
    Rigidbody rb;
    public int floorMask;
    float camRayLength = 60f;

    Animator animator;
    Vector3 moveDirection;
    Quaternion coordinator;

    void Start()
    {
        realstepTime = stepTime;
        audioSource = GetComponent<AudioSource>(); 
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        DefineCoordinator();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        realstepTime -= Time.deltaTime;
        if (h != 0 || v != 0)
        {
            if(realstepTime < 0) {
                if (rightstep) { 
                    audioSource.PlayOneShot(rightStep, 0.2f);
                    rightstep = false;
                }
                else
                {
                    rightstep = true;
                    audioSource.PlayOneShot(leftStep, 0.2f);
                }
                realstepTime = stepTime;
            }
            //audioSource.UnPause();
        }
        var inputAxisVector = new Vector3(h, 0, v);

        // moveDirection = coordinator * inputAxisVector.normalized * speed * Time.deltaTime;
        // transform.position += moveDirection;
        moveDirection = coordinator * inputAxisVector.normalized * speed;
        rb.velocity = moveDirection;
        // rb.AddForce(moveDirection, ForceMode.Impulse);
        Turning();
        if (animator != null)
        {
            var angleHorizontalAxisVSPlayerForward = coordinator * Quaternion.Inverse(transform.rotation);
            var animatorDirection = angleHorizontalAxisVSPlayerForward * inputAxisVector;
            animator.SetFloat("Right", animatorDirection.x, animationDamping, Time.deltaTime);
           
            animator.SetFloat("Forward", animatorDirection.z, animationDamping, Time.deltaTime);
            //audioSource.PlayOneShot(leftStep, 0.5f);
        }
    }

    void DefineCoordinator()
    {
        Vector3 camersAngles = Camera.main.transform.eulerAngles;
        coordinator = Quaternion.Euler(0, camersAngles.y, 0);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(this.transform.position, this.transform.position + moveDirection * 1.1f);
    }

}
