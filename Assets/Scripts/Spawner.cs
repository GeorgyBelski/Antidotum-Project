using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject zombiePrefab;
    public float spawnCooldown = 5f;
    float currentCooldown =0f;
    private bool spawn = true;
    void Start()
    {
        
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0 && spawn)
        {
            SpownZombie();
            currentCooldown = spawnCooldown;
        }
    }

    private void SpownZombie()
    {
        Instantiate(zombiePrefab, this.transform.position, this.transform.rotation);
    }
    void OnBecameInvisible()
    {
        spawn = true;
    }
    void OnBecameVisible()
    {
        spawn = false;
    }
}
