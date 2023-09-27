using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // 향후 다른 설정 구현

    void Start() {
        gameObject.SetActive(false);
    }

    void OnEnable() {
        InactiveGame();
    }

    void OnDisable() {
        activeGame();
    }

    public void SaveButtonClick() {
        DatabaseManager.Instance.JsonSave();
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
