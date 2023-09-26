using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // 향후 다른 설정 구현

    public void SaveButtonClick() {
        DatabaseManager.Instance.JsonSave();
    }

    public void QuitButtonClick() {
        SceneManager.LoadScene("title");
    } 
}
