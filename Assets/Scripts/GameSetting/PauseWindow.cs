using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    public GameObject PauseGameWindow;

    void Start() {
        PauseGameWindow.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PauseGameWindow.activeSelf) {
                PauseGameWindow.SetActive(false);
                GameManager.Instance.resumeGame();
            }
            else {
                PauseGameWindow.SetActive(true);
                GameManager.Instance.pauseGame();
            }
        }
    }
}
