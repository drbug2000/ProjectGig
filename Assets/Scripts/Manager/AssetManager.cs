using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;

public class AssetManager : MonoBehaviour
{
    public TMP_Text tmp;
    public int GoldText;
    public SellItem sellItem;

    WaitForSeconds waitforseconds = new WaitForSeconds(0.01f);

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
        Debug.Log("GameManager에 있는 돈 : " + GameManager.Instance.Gold);
        Debug.Log("GoldText에 표시할 돈 : " + GoldText);
        Debug.Log("바뀔 돈 : " + sellItem.Cost);
        StartCoroutine(Changemoney());

    }

    IEnumerator Changemoney() {
        if (GoldText < GameManager.Instance.Gold) {
            for (; GoldText <= GameManager.Instance.Gold; ++GoldText) {
                yield return waitforseconds;
                tmp.text = GoldText.ToString();
            }
        }
        else if (GoldText > GameManager.Instance.Gold) {
            for (; GoldText >= GameManager.Instance.Gold; --GoldText) {
                yield return waitforseconds;
                tmp.text = GoldText.ToString();
            }
        }
        StopCoroutine(Changemoney());
    }
    //public void Asset(string Gold)
}
