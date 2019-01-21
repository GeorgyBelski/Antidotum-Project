using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDroneScript : MonoBehaviour
{
    public GameObject[] amunitions;
    public Transform catcher;
    private GameObject element;

    public float speed = 6;
    private float realSpeed;
    private float posX, posZ;
    private GameObject player;
    private float distanse, downSpeed = 4f;
    private Vector3 target, catcherStartPos;
    private bool onPos = false, onGround = false;
    private float stopTime = 5;
    private float realStopTime = 0;
    private float liveTime = 15;
    private int randAmmo;
    // Start is called before the first frame update
    void Start()
    {
        catcherStartPos = catcher.transform.position;
        randAmmo = (int)Random.Range(0, amunitions.Length);
        element = Instantiate(amunitions[randAmmo], catcher.position, catcher.rotation, null);

        realSpeed = speed;
        //realStopTime = 5;
        player = GameObject.Find("Player");
        posX = Random.Range(1f, 10f);
        posZ = Random.Range(1f, 10f);
        target = new Vector3(player.transform.position.x + posX, transform.position.y, player.transform.position.z + posZ);

        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {

        liveTime -= Time.deltaTime;
        realStopTime -= Time.deltaTime;
        distanse = (target - this.transform.position).magnitude;
        Vector3 toTarget = target - transform.position;
        if (realStopTime <= 0)
        {
            transform.localPosition += toTarget.normalized * realSpeed * Time.deltaTime;
        }
        else
        {
            catcher.transform.position = new Vector3(catcher.transform.position.x, catcher.transform.position.y - downSpeed * Time.deltaTime, catcher.transform.position.z);
            if (catcher.transform.position.y <= 0.65f && !onGround)
            {
                onGround = true;
                downSpeed *= -1;
            }
            if (onGround && catcher.transform.position.y >= catcherStartPos.y)
            {
                realStopTime = 0;
            }
        }
        if (distanse <= 2 && !onPos)
        {
            onPos = true;
            stop();
        }
        if (liveTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        if (element && !onGround)
        {
            element.transform.position = new Vector3(catcher.transform.position.x, catcher.transform.position.y - 0.5f, catcher.transform.position.z);
        }
    }
    void stop()
    {
        if (player) { 
        //realSpeed = 0;
            realStopTime = stopTime;
            target = new Vector3(player.transform.position.x + 50, transform.position.y, player.transform.position.z + 50);
            transform.LookAt(target);
        }
    }

}
