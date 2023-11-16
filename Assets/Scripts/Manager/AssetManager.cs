using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using Unity.VisualScripting;
//using Unity.VisualScripting.ReorderableList;
using JetBrains.Annotations;
// using UnityEditor.UIElements;

public class AssetManager : MonoBehaviour
{
    void OnEnable()
    {
    	  // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (SceneManager.GetActiveScene().name == "Merge 2"){
            StartCoroutine(Restoremoney());
        }
        
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public TMP_Text tmp;
    public int GoldText;
    public SellItem sellItem;
    public int addmore;
    WaitForSecondsRealtime waitforseconds = new WaitForSecondsRealtime(0.01f);

    // Start is called before the first frame update
    void Awake()
    {
        GoldText = GameManager.Instance.Gold;
        tmp.text = GoldText.ToString();
    }

    void Start() {
        GoldText = GameManager.Instance.Gold;
        tmp.text = GoldText.ToString();
    }

    public void Buy(){
        GameManager.Instance.Gold -= 1000;
        StartCoroutine(Changemoney()); // 
    }

    public void Sell(){
        GameManager.Instance.Gold += sellItem.Cost;
        if (sellItem.Cost <= 100) {
            addmore = 1;
        }
        else {
            addmore = sellItem.Cost / 100;    
        }
        StartCoroutine(Changemoney());
    }

    IEnumerator Changemoney() {
        if (GoldText < GameManager.Instance.Gold) {
            for (; GoldText <= GameManager.Instance.Gold; GoldText += addmore) {
                yield return waitforseconds;
                tmp.text = GoldText.ToString();
            }
            if (GoldText > GameManager.Instance.Gold) {
                GoldText = GameManager.Instance.Gold;
                tmp.text = GoldText.ToString();
            }
        }
        else if (GoldText > GameManager.Instance.Gold) {
            for (; GoldText >= GameManager.Instance.Gold; GoldText -= 10) {
                yield return waitforseconds;
                tmp.text = GoldText.ToString();
            }
            if (GoldText < GameManager.Instance.Gold) {
                GoldText = GameManager.Instance.Gold;
                tmp.text = GoldText.ToString();
            }
        }
        StopCoroutine(Changemoney());
    }

    IEnumerator Restoremoney() {
        yield return new WaitForSecondsRealtime(1f);
        GoldText = GameManager.Instance.Gold;
        tmp.text = GoldText.ToString();
    }
    //public void Asset(string Gold)
}
