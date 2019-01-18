using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanManager : MonoBehaviour
{
    public static int savedPeopleNumber = 0;
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
        if (savedPeopleNumber != previoussavedPeopleNumber) {
            size.sizeDelta += new Vector2(fullWidth / 5, 0);
            previoussavedPeopleNumber = savedPeopleNumber;
        }
    }
}
