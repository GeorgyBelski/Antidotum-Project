using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_sound_box : MonoBehaviour
{
    
    public AudioClip zombie_dead;
    private AudioSource audioSource;
    public float lifeTime = 2f;
   // private float reallifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(zombie_dead, 0.4f);
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
