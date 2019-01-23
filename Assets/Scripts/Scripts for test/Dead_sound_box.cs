using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_sound_box : MonoBehaviour
{
    
    public AudioClip[] zombie_dead;
    private AudioSource audioSource;
    public float lifeTime = 2f;
    private int sound;
   // private float reallifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        sound = (int)Random.Range(0, zombie_dead.Length);
        audioSource.PlayOneShot(zombie_dead[sound], 0.5f);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        //reallifeTime = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
