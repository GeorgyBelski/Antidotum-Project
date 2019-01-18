//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public bool InfinityAntidote = true;

    public float pistolBulletInClip = 14f;
    public float reloadPistolTime = 1f;
    private float reloadPistolTimeLeft;
    private float realPistolBulletInClip;

    public float rifleBulletInClip = 30f;
    public float reloadRifleTime = 2f;
    private float reloadRifleTimeLeft;
    private float realRifleBulletInClip;

    public float range = 12f;

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

    public Text pistolBulletAmmount;
    public Text rifleBulletAmmount;

    float coolDown = 0.3f;
    public float timerCoolDown = 0f;

    float pistolcoolDown = 0.3f;
    float pistolcurrentCoolDown = 0f;

    float riflecoolDown = 0.1f;
    float riflecurrentCoolDown = 0f;

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    float effectDisplayTime = 0.6f;
    PlayerAttributes pAttributes;


    void Start()
    {
        reloadRifleTimeLeft = reloadRifleTime;
        realRifleBulletInClip = rifleBulletInClip;
        rifle = new Rifle_Fire(gun.transform, this.transform, bulletPrefab);
        reloadPistolTimeLeft = reloadPistolTime;
        realPistolBulletInClip = pistolBulletInClip;
        audioSource = GetComponent<AudioSource>();
        shootableMask = LayerMask.GetMask("Obstacle");
        shootableMask |= LayerMask.GetMask("Zombie");
        shootableMask |= LayerMask.GetMask("Human");
        gunLine = GetComponent<LineRenderer>();
        pAttributes = GetComponent<PlayerAttributes>();
    }

    void Update()
    {
        timerCoolDown -= Time.deltaTime;
        pistolcurrentCoolDown -= Time.deltaTime;
        riflecurrentCoolDown -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            Fire();

        }

        if (timerCoolDown <= 0 && Input.GetMouseButton(1))
        {
            if (pAttributes.antidoteAmount > 0 || InfinityAntidote) {
                AntidoteFire();
                pAttributes.RemoveAntidote();
                timerCoolDown = coolDown;
            }     
        }
        if (timerCoolDown < coolDown * effectDisplayTime)
        {
            DisableEffect();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            type = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            type = 2;
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
                    
                    timerCoolDown = coolDown;
                    realPistolBulletInClip -= 1;
                    pistolBulletAmmount.text = realPistolBulletInClip + "/-";
                    audioSource.PlayOneShot(fire_pistol, 0.25f);
                    pistolcurrentCoolDown = pistolcoolDown;
                    if (realPistolBulletInClip == 0)
                    {
                        reload();
                    }

                    Instantiate(bulletPrefab,gun.transform.position, this.transform.rotation);

                    MakeShootRay();
                }
                break;
            case 2:
                if (riflecurrentCoolDown <= 0)
                {
                    
                    timerCoolDown = coolDown;
                    realRifleBulletInClip -= 1;
                    rifleBulletAmmount.text = realRifleBulletInClip + "/-";
                    rifle.fire();
                    audioSource.PlayOneShot(fire_rifle, 0.3f);
                    riflecurrentCoolDown = riflecoolDown;
                    if (realRifleBulletInClip == 0)
                    {
                        reloadRifle();
                    }

                    //Instantiate(bulletPrefab, gun.transform.position, this.transform.rotation);
                    MakeShootRay();

                }
                break;
        }
    }


    void MakeShootRay()
    {
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
    void reload()
    {
        pistolcurrentCoolDown = reloadPistolTime;
        realPistolBulletInClip = pistolBulletInClip;
        audioSource.PlayOneShot(reload_pistol, 0.7f);
        reloadPistolTimeLeft -= Time.deltaTime;
        if (reloadPistolTimeLeft <= 0)
        {
            pistolBulletAmmount.text = realPistolBulletInClip + "/-";
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
            rifleBulletAmmount.text = realRifleBulletInClip + "/-";
            reloadRifleTimeLeft = reloadRifleTime;
        }
    }
}
