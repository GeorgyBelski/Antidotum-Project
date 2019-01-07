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

    [Range(1, 30)]
    public int maxBioAmount = 10;
    public int bioAmount = 0;
    public int antidoteAmount = 0;

    public List<Image> BioBars;
    public List<Image> antidoteCreationBars;
    Image currentHealsBar;
    SphereCollider detectionSphere;
    int previousHealth;
    int previousBioAmount = -1;
    float antidoteCreationTime = 3f;
    float currentCreationTime = 0f;
    bool antidoteCreationProcess = false;

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
        if (health != previousHealth)
        {
            float retioHealth = (float)health / maxHealth;
            currentHealsBar.rectTransform.localScale = new Vector3(retioHealth, 1, 1);
            detectionSphere.radius = health + 20;
        }
        if (bioAmount != previousBioAmount)
        {
            float retioBio = (float)bioAmount / maxBioAmount;
            foreach (Image currentBioBar in BioBars)
            {
                currentBioBar.rectTransform.localScale = new Vector3(1, retioBio, 1);
            }
        }
        if (antidoteCreationProcess)
        {

            if (currentCreationTime < antidoteCreationTime)
            {
                currentCreationTime += Time.deltaTime;
                float retioAnidote = currentCreationTime / antidoteCreationTime;
                foreach (Image antidoteCreationBar in antidoteCreationBars)
                {
                    antidoteCreationBar.fillAmount = retioAnidote;
                }
            }
            else
            {
                antidoteCreationProcess = false;
                currentCreationTime = 0f;
                bioAmount = 0;
                antidoteAmount++;
            }
        }
        else {
            foreach (Image antidoteCreationBar in antidoteCreationBars)
            {
                antidoteCreationBar.fillAmount = 0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //print("DNA");
        if (!antidoteCreationProcess && collision.gameObject.CompareTag("DNA"))
        {
            Destroy(collision.gameObject);
            bioAmount++;
            if (bioAmount >= maxBioAmount)
            {
                antidoteCreationProcess = true;
                
            }

        }
    }

    void ApplyDamage(int value)
    {
        health -= value;
    }
}

