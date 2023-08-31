using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

        if (IsPointerOverUI() == true)
        {

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

    private bool IsPointerOverUI()

    {

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        pointerEventData.position = Input.mousePosition;



        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, results);



        for (int i = 0; i < results.Count; i++)

        {

            if (results[i].gameObject.layer == LayerMask.NameToLayer("ButtonUI"))

                return true;

        }



        return false;

    }
}