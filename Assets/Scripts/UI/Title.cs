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
            ContinueButton.SetActive(false);
        }
        else {
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
        SceneManager.LoadScene("Merge 2");
    }

    public void Loadgame()
    {  
        DatabaseManager.Instance.JsonLoad();
        SceneManager.LoadScene("Merge 2");
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