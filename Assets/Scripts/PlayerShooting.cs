using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public float pistolBulletInClip = 14f;
    public float reloadPistolTime = 1f;
    private float reloadPistolTimeLeft;
    private float realPistolBulletInClip;

    public float rifleBulletInClip = 30f;
    public float reloadRifleTime = 2f;
    private float reloadRifleTimeLeft;
    private float realRifleBulletInClip;

    private int type = 1;

    private Rifle_Fire rifle;

    public AudioClip fire_pistol;
    public AudioClip fire_rifle;
    public AudioClip reload_pistol;
    public AudioClip reload_rifle;
    public AudioClip fire_Antidotum;
    private AudioSource audioSource;

    public Transform gun;
    public GameObject bulletPrefab;
    public GameObject antidoteBulletPrefab;

    float coolDown = 0.3f;
    float timerCoolDown = 0f;

    float pistolcoolDown = 0.3f;
    float pistolcurrentCoolDown = 0f;

    float riflecoolDown = 0.1f;
    float riflecurrentCoolDown = 0f;

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    float effectDisplayTime = 0.5f;
    float range = 10f;
    void Start()
    {
        reloadRifleTimeLeft = reloadRifleTime;
        realRifleBulletInClip = rifleBulletInClip;
        rifle = new Rifle_Fire(gun.transform, bulletPrefab);
        reloadPistolTimeLeft = reloadPistolTime;
        realPistolBulletInClip = pistolBulletInClip;
        audioSource = GetComponent<AudioSource>();
        shootableMask = LayerMask.GetMask("Zombie");
        gunLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        timerCoolDown -= Time.deltaTime;
        pistolcurrentCoolDown -= Time.deltaTime;
        riflecurrentCoolDown -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            Fire();


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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            type = 1;
            print(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            type = 2;
            print(2);
        }
    }

    public void DisableEffect()
    {
        gunLine.enabled = false;
    }

    private void AntidoteFire()
    {
        Instantiate(antidoteBulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);
        audioSource.PlayOneShot(fire_Antidotum, 0.7f);
    }

    void Fire()
    {
        switch (type)
        {
            case 1:
                if (pistolcurrentCoolDown <= 0)
                {
                    realPistolBulletInClip -= 1;
                    audioSource.PlayOneShot(fire_pistol, 0.25f);
                    pistolcurrentCoolDown = pistolcoolDown;
                    if (realPistolBulletInClip == 0)
                    {
                        reload();
                    }

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
                break;
            case 2:
                if (riflecurrentCoolDown <= 0)
                {
                    realRifleBulletInClip -= 1;
                    rifle.fire();
                    audioSource.PlayOneShot(fire_rifle, 0.3f);
                    riflecurrentCoolDown = riflecoolDown;
                    if (realRifleBulletInClip == 0)
                    {
                        reloadRifle();
                    }

                    //Instantiate(bulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);

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
                break;
        }
    }
    void reload()
    {
        pistolcurrentCoolDown = reloadPistolTime;
        realPistolBulletInClip = pistolBulletInClip;
        audioSource.PlayOneShot(reload_pistol, 0.7f);
        reloadPistolTimeLeft -= Time.deltaTime;
        if (reloadPistolTimeLeft <= 0)
        {
            reloadPistolTimeLeft = reloadPistolTime;
        }
    }
    void reloadRifle()
    {
        riflecurrentCoolDown = reloadRifleTime;
        realRifleBulletInClip = rifleBulletInClip;
        audioSource.PlayOneShot(reload_rifle, 1f);
        reloadRifleTimeLeft -= Time.deltaTime;
        if (reloadRifleTimeLeft <= 0)
        {
            reloadRifleTimeLeft = reloadRifleTime;
        }
    }
}
