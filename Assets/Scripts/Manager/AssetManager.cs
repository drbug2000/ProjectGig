using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using JetBrains.Annotations;

public class AssetManager : MonoBehaviour
{
    public TMP_Text tmp;
    public int GoldText;
    public SellItem sellItem;
    public int addmore;
    WaitForSecondsRealtime waitforseconds = new WaitForSecondsRealtime(0.01f);

    // Start is called before the first frame update
    void Start()
    {
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
        
        addmore = GameManager.Instance.Gold / 100;
        StartCoroutine(Changemoney());
    }

    IEnumerator Changemoney() {
        Debug.Log(GoldText);

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
    //public void Asset(string Gold)
}
