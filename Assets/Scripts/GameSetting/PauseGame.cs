using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class PauseGame : MonoBehaviour
{
    // 향후 다른 설정들 구현 ex) 소리, 언어, 해상도 기타 등등
    // Loading Window를 위한 gameobject
    public GameObject LoadingWindow;
    public Animator Loadinganimator;

    void Start() {
        gameObject.SetActive(false);
        LoadingWindow.SetActive(false);
    }

    void OnEnable() {
        InactiveGame();
    }

    void OnDisable() {
        activeGame();
    }

    public void SaveButtonClick() {
        DatabaseManager.Instance.JsonSave();
        LoadingWindow.SetActive(true);
        
    }

    public void QuitButtonClick() {
        SceneManager.LoadScene("title");
    } 

    public void InactiveGame()
    {
        Time.timeScale = 0;
    }

    public void activeGame()
    {
        Time.timeScale = 1;
    }
}
