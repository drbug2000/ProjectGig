using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllNoticeUI : MonoBehaviour
{
    
    public GameObject noticeBox;
    public TMP_Text noticeText;
    //public Animator subani;

    //coroutines
    private WaitForSecondsRealtime _UIDelay2 = new WaitForSecondsRealtime(2.0f);

    // Start is called before the first frame update
    void Start()
    {
        noticeBox.SetActive(false);
    }

    public void Alert(string message){
        noticeText.text = message;
        noticeBox.SetActive(false);
        StartCoroutine(AlertDelay());
    }

    IEnumerator AlertDelay(){
        yield return null;
        noticeBox.SetActive(true);
        yield return _UIDelay2;
        noticeBox.SetActive(false);
    }
}