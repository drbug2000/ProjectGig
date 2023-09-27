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
            }
            else {
                PauseGameWindow.SetActive(true);
            }
        }
    }
}
