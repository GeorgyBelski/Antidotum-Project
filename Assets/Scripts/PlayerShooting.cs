using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform gun;
    public GameObject bulletPrefab;
    public GameObject antidoteBulletPrefab;

    float coolDown = 0.3f;
    float timerCoolDown = 0f;

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    float effectDisplayTime = 0.5f;
    float range = 10f;
    void Start()
    {
        shootableMask = LayerMask.GetMask("Zombie");
        gunLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        timerCoolDown -= Time.deltaTime;
        if (timerCoolDown <= 0 && Input.GetMouseButton(0))
        {
            Fire();
            timerCoolDown = coolDown;
        }

        if (timerCoolDown <= 0 && Input.GetMouseButton(1))
        {
            AntidoteFire();
            timerCoolDown = coolDown;
        }
        if (timerCoolDown <= coolDown * effectDisplayTime)
        {
            DisableEffect();
        }
    }

    public void DisableEffect()
    {
        gunLine.enabled = false;
    }

    private void AntidoteFire()
    {
        Instantiate(antidoteBulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);

    }

    void Fire()
    {
        Instantiate(bulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);

        gunLine.enabled = true;
        gunLine.SetPosition(0, gun.transform.position);
        shootRay.origin = gun.transform.position;
        shootRay.direction = transform.forward;
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            ZombieAttributes zAttributes = shootHit.collider.GetComponent<ZombieAttributes>();
            if (zAttributes)
            {
                int damage = 20;
                zAttributes.ApplyDamage(damage);
            }
            gunLine.SetPosition(1, shootHit.point);

        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
