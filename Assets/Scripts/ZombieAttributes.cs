using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAttributes : MonoBehaviour
{
    [Range(10, 100)]
    public int maxHealth = 100;

    [Range(0, 100)]
    public int health;

    Image currentHealsBar;
    int previousHealth;

    public GameObject soundBox_dead;
    public GameObject DropAfterDead;
    public GameObject DropAfterDeadItems;

    public AudioClip[] walkingSound;
    public AudioClip zombieInPain;
    //public AudioClip walkingSound2;
    private AudioSource audioSource;
    public bool isCured = false;
    public Material curedMaterial;
    Renderer renderer;
    private float timeRangeToSound;
    bool chengeMaterial = false;


    void Start()
    {
        timeRangeToSound = Random.Range(1f, 30f);
        audioSource = GetComponent<AudioSource>();

        renderer = gameObject.transform.Find("Personnage2").GetComponent<Renderer>();
        health = maxHealth;
        previousHealth = health;
        currentHealsBar = gameObject.transform.Find("Canvas_Health/Image_Health_Bar").GetComponent<Image>();
        currentHealsBar.rectTransform.localScale = new Vector3(1, 1, 1);

    }

    void Update()
    {
        timeRangeToSound -= Time.deltaTime;
        if (timeRangeToSound < 0)
        {
            int index = Random.Range(0, walkingSound.Length);
            audioSource.PlayOneShot(walkingSound[index], 0.3f);
            timeRangeToSound = Random.Range(3f, 50f);
        }
        if (health != previousHealth)
        {
            float retio = (float)health / maxHealth;
            currentHealsBar.rectTransform.localScale = new Vector3(retio, 1, 1);
            if (health <= 0)
            {
                int index = Random.Range(0, 10);
                if (index == 1)
                {
                    Instantiate(DropAfterDeadItems, this.transform.position, this.transform.rotation, null);
                }
                Instantiate(soundBox_dead, this.transform.position, this.transform.rotation, null);
                Instantiate(DropAfterDead, this.transform.position, this.transform.rotation, null);
                Destroy(gameObject);
            }
        }

        if (isCured && !chengeMaterial)
        {
            renderer.sharedMaterial = curedMaterial;
            chengeMaterial = true;
            GetComponent< Zombie_script1 >().enabled = false;
        }

        
    }


    public void ApplyDamage(int value)
    {
        health -= value;
    }

    public void getSound()
    {
        //print("+");
        int index = Random.Range(1, 6);
        if (index == 2)
        {
            audioSource.PlayOneShot(zombieInPain, 0.8f);
        }
    }
}
