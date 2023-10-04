using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    public GameObject PauseGameWindow;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PauseGameWindow.activeSelf) {
                PauseGameWindow.SetActive(false);
                activeGame();
            }
            else {
                PauseGameWindow.SetActive(true);
                InactiveGame();
            }
        }
    }

    void InactiveGame()
    {
        Time.timeScale = 0;
    }

    void activeGame()
    {
        Time.timeScale = 1;
    }
}
