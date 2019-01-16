using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour, IDamageable
{
    [Range(10, 200)]
    public int maxHealth = 100;

    [Range(0, 200)]
    public int health;
    float healthRatio;
    [Space]

    [Range(1, 30)]
    public int maxBioAmount = 10;
    public int bioAmount = 0;
    public int antidoteAmount = 0;

    public List<Image> BioBars;
    public List<Image> antidoteCreationBars;
    public Text AntidoteAmountText;
    

    Image currentHealsBar;
    Image antidoteImage;
    SphereCollider detectionSphere;
    int previousHealth;
    int previousBioAmount = -1;

    float antidoteCreationTime = 3f;
    float currentCreationTime = 0f;
    [HideInInspector]
    public bool antidoteCreationProcess = false;

    public int count = 0;

    void Start()
    {
        health = maxHealth;
        previousHealth = health;
        currentHealsBar = gameObject.transform.Find("Canvas_Health/Image_Health_Bar").GetComponent<Image>();
        antidoteImage = gameObject.transform.Find("Canvas_Bio/Image_Antidote").GetComponent<Image>();
        antidoteImage.color = new Color(0.2f, 0.2f, 0.2f);
        detectionSphere = GetComponent<SphereCollider>();
        detectionSphere.radius = health + 20;
        currentHealsBar.rectTransform.localScale = new Vector3(1, 1, 1);
        AntidoteAmountText.text = "" + antidoteAmount;
        SetAntidoteCreationBars(0f);
        healthRatio = 1f;

    }

    void Update()
    {
        
        if (health != previousHealth)
        {
        //    currentHealsBar.color = new Color(0f,.5f+0.2f*Mathf.Sin(Time.time*12f),0f);
            healthRatio = (float)health / maxHealth;
            currentHealsBar.rectTransform.localScale = new Vector3(healthRatio, 1, 1);
            detectionSphere.radius = health + 20;
            previousHealth = health;
        }
        if (bioAmount != previousBioAmount)
        {
            float retioBio = (float)bioAmount / maxBioAmount;
            SetBioBars(retioBio);
        }
        if (antidoteCreationProcess)
        {

            if (currentCreationTime < antidoteCreationTime)
            {
                currentCreationTime += Time.deltaTime;
                float creationAntidoteRetio = currentCreationTime / antidoteCreationTime;
                SetAntidoteCreationBars(creationAntidoteRetio);
            }
            else
            {
                AddAntidote();
            }
        }

    }
    void SetBioBars(float retioBio)
    {
        foreach (Image currentBioBar in BioBars)
        {
            currentBioBar.rectTransform.localScale =  new Vector3(1, retioBio, 1);
        }
        previousBioAmount = bioAmount;


    }

    void SetAntidoteCreationBars(float processRetio)
    {
        foreach (Image antidoteCreationBar in antidoteCreationBars)
        {
            antidoteCreationBar.fillAmount = processRetio;
        }
    }

    void AddAntidote()
    {
        SetAntidoteCreationBars(0f);
        antidoteCreationProcess = false;
        currentCreationTime = 0f;
        bioAmount = 0;
        antidoteAmount++;
        AntidoteAmountText.text = "" + antidoteAmount;
        if(antidoteAmount == 1)
            antidoteImage.color = Color.white;
    }

    public void RemoveAntidote() {
        if(antidoteAmount > 0)
        {
            antidoteAmount--;
            AntidoteAmountText.text = "" + antidoteAmount;
            if (antidoteAmount == 0)
                antidoteImage.color = new Color(0.3f, 0.3f, 0.3f);
        }   
    }

    public void ApplyDamage(int value)
    {
        health -= value;
        if(health > maxHealth)
        {
            health = maxHealth;
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

