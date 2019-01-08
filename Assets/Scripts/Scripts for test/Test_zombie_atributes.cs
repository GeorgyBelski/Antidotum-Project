using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Test_zombie_atributes : MonoBehaviour
{
    [Range(10, 100)]
    public int maxHealth = 100;

    [Range(0, 100)]
    public int health;

    Image currentHealsBar;
    int previousHealth;

    public AudioClip[] walkingSound;
    //public AudioClip walkingSound2;
    private AudioSource audioSource;
    public bool isCured = false;
    public Material curedMaterial;
    public Renderer renderer;
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
        if(timeRangeToSound < 0)
        {
            int index = Random.Range(0, walkingSound.Length);
            audioSource.PlayOneShot(walkingSound[index], 0.3f);
            timeRangeToSound = Random.Range(3f, 50f);
        }
        if (health != previousHealth)
        {
            float retio = (float)health / maxHealth;
            currentHealsBar.rectTransform.localScale = new Vector3(retio, 1, 1);
        }

        if (isCured && !chengeMaterial)
        {
            renderer.sharedMaterial = curedMaterial;
            chengeMaterial = true;
            GetComponent<Zombie_script>().enabled = false;
        }
    }
}
