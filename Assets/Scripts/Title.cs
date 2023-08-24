using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject SettingWindow;


    void Awake()
    {
        SettingWindow.SetActive(false);
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
        SceneManager.LoadScene("Main");
    }

    public void Loadgame()
    {  

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