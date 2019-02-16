using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_P_script : MonoBehaviour
{
    public Canvas canvas;
    //public Image iconImage;
    public Button buttonCont;
    public Button buttonrest;
    private Vector3 startPosCon;//, startPosRes;
    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;
    // Start is called before the first frame update
    void Start()
    {
        
        buttonrest.onClick.AddListener(RestClick);
        buttonCont.onClick.AddListener(ContClick);
        startPosCon = canvas.transform.localScale;
        canvas.transform.localScale = new Vector3(0, 0, 0);
        //canvas.transform.position = startPosCon + new Vector3(2000, 2000, 0);
        //myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes");
        //scenePaths = myLoadedAssetBundle.GetAllScenePaths();
        //print(scenePaths[0]);
        //startPosRes = 
        //iconImage.transform.position = new Vector3(1500, iconImage.transform.position.y, iconImage.transform.position.z); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            canvas.transform.localScale = startPosCon;
            //canvas.transform.position = startPosCon; //+ new Vector3(2000, 2000, 0);
            Time.timeScale = 0f;
        }
    }

    public void ContClick()
    {
        //if()
        //canvas.transform.position = new Vector3(1500, canvas.transform.position.y, canvas.transform.position.z);
        Time.timeScale = 1f;
        canvas.transform.localScale = new Vector3(0, 0, 0);

    }
    public static void RestClick()
    {
        HumanManager.humanList.Clear();
        HumanManager.savedPeopleNumber = 0;
        //SceneManager.LoadScene(scenePaths[0], LoadSceneMode.Single);
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
        
        //if()
        //canvas.transform.position = new Vector3(1500, canvas.transform.position.y, canvas.transform.position.z);
        //Time.timeScale = 1f;
    }

}