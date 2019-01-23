using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAttributes : MonoBehaviour, IDamageable
{
    [Range(10, 100)]
    public int maxHealth = 100;

    [Range(0, 100)]
    public int health;

    public Image currentHealsBar;
    float healthRatio;
    int previousHealth;

    public GameObject soundBox_dead;
    public GameObject DropAfterDead;
    public bool isHealPackDrops = false;
    public GameObject DropAfterDeadItems;

    public AudioClip[] walkingSound;
    public AudioClip zombieInPain;
    //public AudioClip walkingSound2;
    private AudioSource audioSource;
    public bool isCured = false;
    public bool isInjured = false;
    public Material curedMaterial;
    public Transform neckPosition;
    
    new Renderer renderer;
    private float timeRangeToSound;
    bool chengeMaterial = false;
    


    void Start()
    {
        timeRangeToSound = Random.Range(1f, 40f);
        
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        renderer = gameObject.transform.Find("Personnage2").GetComponent<Renderer>();
        health = maxHealth;
        previousHealth = health;
        currentHealsBar = gameObject.transform.Find("Canvas_Health/Image_Health_Bar").GetComponent<Image>();
        currentHealsBar.rectTransform.localScale = new Vector3(1, 1, 1);
        healthRatio = 1f;
    }

    void Update()
    {
        timeRangeToSound -= Time.deltaTime;
        if (timeRangeToSound < 0)
        {
            audioSource.pitch = Random.Range(0.85f, 1.15f);
            int index = Random.Range(0, walkingSound.Length);
            audioSource.PlayOneShot(walkingSound[index], 0.3f);
            timeRangeToSound = Random.Range(3f, 50f);
        }
        if (health != previousHealth)
        {
            isInjured = true;
            healthRatio = (float)health / maxHealth;
            currentHealsBar.rectTransform.localScale = new Vector3(healthRatio, 1, 1);
            if (health <= 0)
            {
                HumanManager.humanList.Remove(this);
                if (isHealPackDrops) { 
                    int index = Random.Range(0, 10);
                    if (index == 1)
                    {
                        Instantiate(DropAfterDeadItems, this.transform.position, this.transform.rotation, null);
                    }
                }
                Instantiate(soundBox_dead, this.transform.position, this.transform.rotation, null);
                Instantiate(DropAfterDead, this.transform.position, this.transform.rotation, null);
                Destroy(gameObject);
            }
        }       
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Layers.bullet)
        {
            Destroy(collision.gameObject);
            getSound();
        }
    }
    */
    public void Recover() {
        if (!isCured) {
            isCured = true;
            renderer.sharedMaterial = curedMaterial;
            chengeMaterial = true;
            var sc = gameObject.AddComponent<SphereCollider>();
            sc.center += new Vector3(0f,5f,0f);
            sc.radius = 16f;
            sc.isTrigger = true;
     
            gameObject.layer = Layers.human;
            currentHealsBar.color = new Color(0.25f,0.6f,0.8f);
            HumanManager.humanList.Add(this);
        }
    }

    public void ApplyDamage(int value)
    {
        health -= value;
    }

    public void getSound()
    {
        int index = Random.Range(1, 10);
        if (index == 2)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(zombieInPain, 1f);
        }
    }

    public float GetHealthRatio()
    {
        return healthRatio;
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
}
