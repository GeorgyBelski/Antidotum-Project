using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanManager : MonoBehaviour
{
    public static int savedPeopleNumber = 0;
    public Text gameWinText;
    int previoussavedPeopleNumber = 0;
    public static List<ZombieAttributes> humanList = new List<ZombieAttributes>();

    public Image savedHumansBar;
    RectTransform size;
    float fullWidth;
    void Start()
    {
        size = savedHumansBar.GetComponent<RectTransform>();
        fullWidth = size.sizeDelta.x;
        size.sizeDelta = new Vector2(0, size.sizeDelta.y);
    }

    void Update()
    {
        if (savedPeopleNumber != previoussavedPeopleNumber && size) {
            size.sizeDelta += new Vector2(fullWidth / 5, 0);
            previoussavedPeopleNumber = savedPeopleNumber;
        }
        if (savedPeopleNumber >= 5)
        {
            Time.timeScale = 0.3f;
            //GameObject.Find("")
            gameWinText.color = new Color(gameWinText.color.r, gameWinText.color.g, gameWinText.color.b, 255);
        }
    }
}
