using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player_Firing : MonoBehaviour
{
    public AudioClip fire_pistol;
    public AudioClip fire_Antidotum;
    private AudioSource audioSource;
    public Transform gun;
    public GameObject bulletPrefab;
    public GameObject antidoteBulletPrefab;

    float coolDown = 0.3f;
    float currentCoolDown = 0f;

    void Start()
    {
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
    }

    private void AntidoteFire()
    {

        Instantiate(antidoteBulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);
        audioSource.PlayOneShot(fire_Antidotum, 0.7f);
    }

    void Fire()
    {
        audioSource.PlayOneShot(fire_pistol, 0.5f);
        Instantiate(bulletPrefab, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), this.transform.rotation);
    }
}
