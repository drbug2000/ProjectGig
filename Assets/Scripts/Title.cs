using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    public void startgame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Loadgame()
    {  

    }

    public void Setting()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}