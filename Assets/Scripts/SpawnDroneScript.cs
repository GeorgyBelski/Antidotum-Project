using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDroneScript : MonoBehaviour
{
    public float coolDown = 10f;
    public GameObject drone;

    private GameObject player;
    private float timeLeft;

    private float posX, posZ;
    private int randX, randZ;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = coolDown;
        randX = 30;
        randZ = 30;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {
            if(((int)Random.Range(0f, 2f)) == 0)
            {
                randX *= -1;
            }
            if (((int)Random.Range(0f, 2f)) == 0)
            {
                randZ *= -1;
            }
            
            //posX = Random.Range(1f, 10f) + randX;
            //posZ = Random.Range(1f, 10f) + randZ;
            timeLeft = coolDown;
            player = GameObject.Find("Player");
            if(player)
            Instantiate(drone, new Vector3(player.transform.position.x + randX, player.transform.position.y + 6, player.transform.position.z + randZ), player.transform.rotation, null);
        }

    }
}
