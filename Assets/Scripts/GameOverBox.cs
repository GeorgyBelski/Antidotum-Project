using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverBox : MonoBehaviour
{
    public Button buttonrest;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Men")
        buttonrest.onClick.AddListener(Menu_P_script.RestClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
