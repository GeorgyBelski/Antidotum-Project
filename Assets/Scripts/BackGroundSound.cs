using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSound : MonoBehaviour
{
    public AudioClip backGround;
    private AudioSource audioSource;
    private GameObject player;
    //public Toggle m_Toggle;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(backGround, 0.3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        player = GameObject.Find("Player");
        if(player == null)
        {
            audioSource.pitch = 0.85f;
        }
    }
}
