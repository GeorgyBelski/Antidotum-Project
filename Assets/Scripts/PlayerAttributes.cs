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
    
    void Start()
    {
        health = maxHealth;
        currentHealsBar = gameObject.transform.Find("Canvas_Health/Image_Health_Bar").GetComponent<Image>();
    }

    void Update()
    {
        float retio = (float) health / maxHealth;
        currentHealsBar.rectTransform.localScale = new Vector3(retio,1,1);
    }
}
