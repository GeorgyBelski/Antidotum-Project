using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Player_Firing : MonoBehaviour
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

    public Text pistolBulletAmmount;
    public Text rifleBulletAmmount;

    public AudioClip fire_pistol;
    public AudioClip fire_rifle;
    public AudioClip reload_pistol;
    public AudioClip reload_rifle;
    public AudioClip fire_Antidotum;
    private AudioSource audioSource;
    public Transform gun;
    public GameObject bulletPrefab;
    public GameObject antidoteBulletPrefab;


    float pistolcoolDown = 0.3f;
    float pistolcurrentCoolDown = 0f;

    float riflecoolDown = 0.1f;
    float riflecurrentCoolDown = 0f;

    void Start()
    {
        reloadRifleTimeLeft = reloadRifleTime;
        realRifleBulletInClip = rifleBulletInClip;
        rifle = new Rifle_Fire(gun.transform, bulletPrefab);
        reloadPistolTimeLeft = reloadPistolTime;
        realPistolBulletInClip = pistolBulletInClip;
        pistolBulletAmmount.text = realPistolBulletInClip + "/-" ;
        audioSource = GetComponent<AudioSource>();
        rifleBulletAmmount.text = realRifleBulletInClip + "/-";
    }

    void Update()
    {
        pistolcurrentCoolDown -= Time.deltaTime;
        riflecurrentCoolDown -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            Fire();
            
            
        }

        if (pistolcurrentCoolDown <= 0 && Input.GetMouseButton(1))
        {
            AntidoteFire();
            pistolcurrentCoolDown = pistolcoolDown;
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
                if(pistolcurrentCoolDown <= 0) { 
                    realPistolBulletInClip -= 1;
                    pistolBulletAmmount.text = realPistolBulletInClip + "/-";
                    audioSource.PlayOneShot(fire_pistol, 0.25f);
                    Instantiate(bulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);
                    pistolcurrentCoolDown = pistolcoolDown;
                    if (realPistolBulletInClip == 0)
                    {
                        reload();
                    }
                }
                break;
            case 2:
                if (riflecurrentCoolDown <= 0)
                {
                    realRifleBulletInClip -= 1;
                    rifleBulletAmmount.text = realRifleBulletInClip + "/-";
                    rifle.fire();
                    audioSource.PlayOneShot(fire_rifle, 0.3f);
                    riflecurrentCoolDown = riflecoolDown;
                    if (realRifleBulletInClip == 0)
                    {
                        reloadRifle();
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
        if(reloadPistolTimeLeft <= 0)
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
