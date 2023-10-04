using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssetManager : MonoBehaviour
{
    public TMP_Text tmp;
    public int GoldText;
    // Start is called before the first frame update
    void Start()
    {
        GoldText = GameManager.Instance.Gold;
        tmp.text = GoldText.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy(){
        GameManager.Instance.Gold -= 1000;
        GoldText = GameManager.Instance.Gold;
        tmp.text = GoldText.ToString();
    }

    //public void Asset(string Gold)
}
