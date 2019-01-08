using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle_Fire : MonoBehaviour
{
    private Transform gun;
    private GameObject bullet;
    public Rifle_Fire(Transform gun, GameObject bullet)
    {
        this.gun = gun;
        this.bullet = bullet;
    }

    public void fire()
    {
        Instantiate(bullet, gun.position, gun.rotation, null);
    }
}
