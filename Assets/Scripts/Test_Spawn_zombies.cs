using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Spawn_zombies : MonoBehaviour
{
    public float time = 1;
    public GameObject zombiePrefub;

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
            realtime = time;
            Instantiate(zombiePrefub, new Vector3(Random.Range(-5.0f, 5.0f), 1, Random.Range(-5.0f, 5.0f)), new Quaternion(0, 0, 0, 0), null);
        } 
    }
}
