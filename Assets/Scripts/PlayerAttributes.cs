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

    Image currentHealsBar;
    SphereCollider detectionSphere;
    int previousHealth;

    void Start()
    {
        health = maxHealth;
        previousHealth = health;
        currentHealsBar = gameObject.transform.Find("Canvas_Health/Image_Health_Bar").GetComponent<Image>();
        detectionSphere = GetComponent<SphereCollider>();
        detectionSphere.radius = health + 20;
        currentHealsBar.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        if(health != previousHealth) { 
            float retio = (float) health / maxHealth;
            currentHealsBar.rectTransform.localScale = new Vector3(retio,1,1);
            detectionSphere.radius = health + 20;
        }
    }

    void ApplyDamage(int value) {
        health -= value;
    }
}

