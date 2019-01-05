using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    public Transform gun;
    public GameObject bulletPrefab;

    float coolDown = 0.3f;
    float currentCoolDown = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        currentCoolDown -= Time.deltaTime;
        if (currentCoolDown <=0 && Input.GetMouseButton(0))
        {
            Fire();
            currentCoolDown = coolDown;
        }
    }

    void Fire()
    {
        Instantiate(bulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);
    }
}
