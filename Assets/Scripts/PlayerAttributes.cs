using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    [Range(10, 200)]
    public int maxHealth = 100;

    [Range(0, 200)]
    public int health;

    [Space]

    [Range(10, 30)]
    public int maxBioAmount = 10;
    public int bioAmount = 0;
    public int anidoreAmount = 0;

    Image currentHealsBar;
    Image currentBioBar;
    SphereCollider detectionSphere;
    int previousHealth;
    int previousBioAmount = -1;

    void Start()
    {
        health = maxHealth;
        previousHealth = health;
        currentHealsBar = gameObject.transform.Find("Canvas_Health/Image_Health_Bar").GetComponent<Image>();
        currentBioBar = gameObject.transform.Find("Canvas_Bio/Image_Bio_Bar").GetComponent<Image>();
        detectionSphere = GetComponent<SphereCollider>();
        detectionSphere.radius = health + 20;
        currentHealsBar.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        if(health != previousHealth) { 
            float retioHealth = (float) health / maxHealth;
            currentHealsBar.rectTransform.localScale = new Vector3(retioHealth, 1,1);
            detectionSphere.radius = health + 20;
        }
        if (bioAmount != previousBioAmount) {
            float retioBio = (float)bioAmount / maxBioAmount;
            currentBioBar.rectTransform.localScale = new Vector3(1, retioBio, 1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //print("DNA");
        if (collision.gameObject.CompareTag("DNA"))
        {

            //dna.text = "1";
            Destroy(collision.gameObject);
            
            if (bioAmount < maxBioAmount) { 
                bioAmount++;
            }
        }
    }

    void ApplyDamage(int value) {
        health -= value;
    }
}

