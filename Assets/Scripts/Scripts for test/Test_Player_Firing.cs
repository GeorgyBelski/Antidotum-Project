using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Player_Firing : MonoBehaviour
{
    public float pistolBulletInClip = 14f;
    public float reloadPistolTime = 1f;
    private float reloadPistolTimeLeft;
    public float realPistolBulletInClip;

    public Text pistolBulletAmmount;
    public AudioClip fire_pistol;
    public AudioClip reload_pistol;
    public AudioClip fire_Antidotum;
    private AudioSource audioSource;
    public Transform gun;
    public GameObject bulletPrefab;
    public GameObject antidoteBulletPrefab;


    float coolDown = 0.3f;
    float currentCoolDown = 0f;

    void Start()
    {
        reloadPistolTimeLeft = reloadPistolTime;
        realPistolBulletInClip = pistolBulletInClip;
        pistolBulletAmmount.text = realPistolBulletInClip + "/-" ;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        currentCoolDown -= Time.deltaTime;
        if (currentCoolDown <= 0 && Input.GetMouseButton(0))
        {
            Fire();
            currentCoolDown = coolDown;
        }

        if (currentCoolDown <= 0 && Input.GetMouseButton(1))
        {
            AntidoteFire();
            currentCoolDown = coolDown;
        }
        if(realPistolBulletInClip == 0)
        {
            reload();
        }
    }

    private void AntidoteFire()
    {

        Instantiate(antidoteBulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);
        audioSource.PlayOneShot(fire_Antidotum, 0.7f);
    }

    void Fire()
    {
        realPistolBulletInClip -= 1;
        pistolBulletAmmount.text = realPistolBulletInClip + "/-";
        audioSource.PlayOneShot(fire_pistol, 0.25f);
        Instantiate(bulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);
    }
    void reload()
    {
        currentCoolDown = reloadPistolTime;
        realPistolBulletInClip = pistolBulletInClip;
        audioSource.PlayOneShot(reload_pistol, 0.7f);
        reloadPistolTimeLeft -= Time.deltaTime;
        if(reloadPistolTimeLeft < 0)
        {
            pistolBulletAmmount.text = realPistolBulletInClip + "/-";
            reloadPistolTimeLeft = reloadPistolTime;
        }
    }
}
