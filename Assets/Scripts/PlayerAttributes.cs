using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour, IDamageable
{
    //public Button buttonComponent;

    [Range(10, 200)]
    public int maxHealth = 100;

    [Range(0, 200)]
    public int health;
    float healthRatio;
    [Space]

    [Range(1, 4)]
    public int livesCount;
    public Image lives;
    RectTransform size;
    private float godTime = 1;
    private bool godbool = false;
    //public static Text gameText;
    public GameObject zombieAfterDead, gameOverBox;

    [Range(1, 30)]
    public int maxBioAmount = 10;
    public int bioAmount = 0;
    public int antidoteAmount = 0;

    public List<Image> BioBars;
    public List<Image> antidoteCreationBars;
    public Text AntidoteAmountText;

    private Image healthBarColor;
    Color32 startHealthBarColor;
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
        //buttonComponent.onClick.;
        //Instantiate(gameOverBox, this.transform.position, this.transform.rotation);
        size = lives.GetComponent<RectTransform>();
        size.sizeDelta = new Vector2(270*livesCount, 240);
        //lives.rectTransform.transform.
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
        healthBarColor = currentHealsBar.GetComponent<Image>();
        //gameText.color = new Color(gameText.color.r, gameText.color.g, gameText.color.b, 0);
        startHealthBarColor = new Color32(70, 195, 111, 255);
    }

    void Update()
    {
        if(godTime > 0) { 
            godTime -= Time.deltaTime;
            healthBarColor.color = Color.Lerp(healthBarColor.color, startHealthBarColor, 1 - godTime*2);
        }
        else
        {
            //godbool = false;
            healthBarColor.color = startHealthBarColor;
        }

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
        if (godTime >= 0)
        {
            value = 0;
        }
        health -= value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health <= 0)
        {
            godTime = 2;
            livesCount -= 1;
            health = maxHealth;
            size.sizeDelta = new Vector2(270 * livesCount, 240);
            healthBarColor.color = new Color32(172, 100, 195, 255);
            //godbool = true;
            if (livesCount == 0)
            {
                Instantiate(zombieAfterDead, this.transform.position, this.transform.rotation);
                Instantiate(gameOverBox, this.transform.position, this.transform.rotation);
                //gameText.color = new Color(gameText.color.r, gameText.color.g, gameText.color.b, 255);
                Destroy(gameObject);
                //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-3, this.transform.position.z);
                

            }
       
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

