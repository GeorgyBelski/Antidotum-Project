using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Test_Spawn_zombies : MonoBehaviour
{
    public float time = 1;
    public GameObject zombiePrefub;
    public GameObject player;

     
    private float realtime;
    // Start is called before the first frame update
    void Start()
    {
        realtime = time;
    }

    // Update is called once per frame
    void Update()
    {
        realtime -= Time.deltaTime;
       if (realtime < 0)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    Instantiate(zombiePrefub, 
                        new Vector3(player.transform.position.x + 10, 
                        0.2f, 
                        player.transform.position.z + 10), 
                        new Quaternion(0, 0, 0, 0), 
                        null);
                    break;
                case 2:
                    Instantiate(zombiePrefub,
                        new Vector3(player.transform.position.x + 10,
                        0.2f,
                        player.transform.position.z - 10),
                        new Quaternion(0, 0, 0, 0),
                        null);
                    break;
                case 3:
                    Instantiate(zombiePrefub,
                        new Vector3(player.transform.position.x - 10,
                        0.2f,
                        player.transform.position.z - 10),
                        new Quaternion(0, 0, 0, 0),
                        null);
                    break;
                case 4:
                    Instantiate(zombiePrefub,
                    new Vector3(player.transform.position.x - 10,
                    0.2f,
                    player.transform.position.z + 10),
                    new Quaternion(0, 0, 0, 0),
                    null);
                    break;
            }
            realtime = time;
            
        } 
    }
}
