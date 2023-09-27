using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class Title : MonoBehaviour
{
    public GameObject SettingWindow;
    public GameObject ContinueButton;


    void Start()
    {
        SettingWindow.SetActive(false);
        if (!File.Exists(DatabaseManager.Instance.path)) {
            Debug.Log("없어요");
            ContinueButton.SetActive(false);
        }
        else {
            Debug.Log("있어요");
            ContinueButton.SetActive(true);
        }
    }

    void Update()
    {
        if (SettingWindow.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SettingWindow.SetActive(false);
            }
        }
    }

    public void startgame()
    {
        SceneManager.LoadScene("fishScenes");
    }

    public void Loadgame()
    {  
        DatabaseManager.Instance.JsonLoad();
        SceneManager.LoadScene("fishScenes");
    }

    public void Setting()
    {
        SettingWindow.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void ExitSetting()
    {
        if (SettingWindow.activeSelf == true)
        {
            SettingWindow.SetActive(false);
        }
    }

       
}