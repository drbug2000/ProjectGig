using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoticeUI : MonoBehaviour
{
    public GameObject subbox;
    public TMP_Text subintext;
    //public Animator subani;

    //coroutines
    WaitForSeconds _UIDelay1 = new WaitForSeconds(2.0f);
    private WaitForSeconds _UIDelay2 = new WaitForSeconds(0.3f);

    // Start is called before the first frame update
    void Start()
    {
        subbox.SetActive(false);
    }

    public void SUB(string message){
        subintext.text = message;
        subbox.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(SUBDelay());
        /*
        if (subbox.activeSelf == true) {
            subbox.SetActive(false);
        }
        */
    }

    IEnumerator SUBDelay(){
        yield return null;
        subbox.SetActive(true);
        /*
        if (Input.anyKeyDown) {
            subbox.SetActive(false);
            StopCoroutine(SUBDelay());
        }
        */
        // yield return _UIDelay1;
        yield return _UIDelay1;
        subbox.SetActive(false);
        //subani.SetBool("isOn", true);
        // yield return _UIDelay1;
        //subani.SetBool("isOn", false);
        // yield return _UIDelay2;
        /*
        StartCoroutine(SUBDelay());
        StopCoroutine(SUBDelay());
        */
    }
}
