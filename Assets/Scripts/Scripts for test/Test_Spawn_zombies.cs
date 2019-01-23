using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Test_Spawn_zombies : MonoBehaviour
{
    public float time = 10;
    public GameObject zombiePrefub;
    public GameObject player;

     
    private static float realtime, or_time;
    private static float m_timeSpaner;
    // Start is called before the first frame update
    void Start()
    {
        or_time = time;
        //Test_Spawn_zombies
        realtime = time;
    }

    // Update is called once per frame
    void Update()
    {
        m_timeSpaner -= Time.deltaTime;
        if (m_timeSpaner > 0)
        {
           
            time = 0.75f;
           
        }
        else
        {
            time = or_time;
        }
        if (player) { 
           realtime -= Time.deltaTime;
           if (realtime < 0)
            {
                switch (Random.Range(1, 5))
                {
                    case 1:
                        Instantiate(zombiePrefub, 
                            new Vector3(player.transform.position.x + 18, 
                            0, 
                            player.transform.position.z + 18), 
                            new Quaternion(0, 0, 0, 0), 
                            null);
                        break;
                    case 2:
                        Instantiate(zombiePrefub,
                            new Vector3(player.transform.position.x + 18,
                            0,
                            player.transform.position.z - 18),
                            new Quaternion(0, 0, 0, 0),
                            null);
                        break;
                    case 3:
                        Instantiate(zombiePrefub,
                            new Vector3(player.transform.position.x - 18,
                            0,
                            player.transform.position.z - 18),
                            new Quaternion(0, 0, 0, 0),
                            null);
                        break;
                    case 4:
                        Instantiate(zombiePrefub,
                        new Vector3(player.transform.position.x - 18,
                        0,
                        player.transform.position.z + 18),
                        new Quaternion(0, 0, 0, 0),
                        null);
                        break;
                    default:
                        break;
                }
                realtime = time;
            
            } 
        }
    }

    public static void massiveSpawn()
    {
        
        m_timeSpaner = 10f;
        realtime = 0;
    }
}
