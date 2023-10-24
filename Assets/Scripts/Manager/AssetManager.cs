using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssetManager : MonoBehaviour
{
    public TMP_Text tmp;
    public int GoldText;
    public SellItem sellItem;

    // Start is called before the first frame update
    void Start()
    {
        GoldText = GameManager.Instance.Gold;
        tmp.text = GoldText.ToString();
    }

    public void Buy(){
        GameManager.Instance.Gold -= 1000;
        GoldText = GameManager.Instance.Gold;
        tmp.text = GoldText.ToString();
    }

    public void Sell(){
        GameManager.Instance.Gold += sellItem.Cost;
        GoldText = GameManager.Instance.Gold;
        tmp.text = GoldText.ToString();
    }

    //public void Asset(string Gold)
}
