using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class PauseGame : MonoBehaviour
{
    // 향후 다른 설정들 구현 ex) 소리, 언어, 해상도 기타 등등
    // Loading Window를 위한 gameobject
    // public GameObject LoadingWindow;
    // public Animator Loadinganimator;
    AllNoticeUI _notice;

    void Start() {
        // LoadingWindow.SetActive(false);
        _notice = FindObjectOfType<AllNoticeUI>();
    }

    public void SaveButtonClick() {
        // LoadingWindow.SetActive(true);
        DatabaseManager.Instance.JsonSave();
        _notice.Alert("Save complete");
        
    }

    public void QuitButtonClick() {
        SceneManager.LoadScene("title");
    } 
}
