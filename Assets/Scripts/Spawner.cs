using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject zombiePrefab;
    public float spawnCooldown = 5f;
    float currentCooldown =0f;
    public int cooldownOffset = 0;
    private bool spawn = true;
    void Start()
    {
        
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        cooldownOffset = HumanManager.humanList.Count;
        if (currentCooldown <= (0 + Math.Min(cooldownOffset, spawnCooldown - 2)) && spawn)
        {
            SpawnZombie();
            currentCooldown = spawnCooldown;
        }
    }

    private void SpawnZombie()
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
