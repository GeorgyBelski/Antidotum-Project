using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle_Fire : MonoBehaviour
{
    private Transform gun;
    Transform player;
    private GameObject bullet;
    
    public Rifle_Fire(Transform gun, Transform player, GameObject bullet)
    {
        this.gun = gun;
        this.player = player;
        this.bullet = bullet;
        
    }

    public void fire()
    {
        Instantiate(bullet, gun.position, player.rotation, null);
    }
}
