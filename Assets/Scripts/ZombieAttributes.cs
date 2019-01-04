﻿using System.Collections;
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

    void Start()
    {
        health = maxHealth;
        previousHealth = health;
        currentHealsBar = gameObject.transform.Find("Canvas_Health/Image_Health_Bar").GetComponent<Image>();
        currentHealsBar.rectTransform.localScale = new Vector3(1, 1, 1);

    }

    void Update()
    {
        if (health != previousHealth)
        {
            float retio = (float)health / maxHealth;
            currentHealsBar.rectTransform.localScale = new Vector3(retio, 1, 1);
        }
    }
}