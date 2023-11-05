using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    public Inventory theinventory;
    NoticeUI _notice;
    public int Cost = 0;
    public string CostText;

    public AssetManager theassetmanager;

    public void Awake(){
        _notice = FindObjectOfType<NoticeUI>();
    }
    
    public void Clicksellbutton()
    {
        Cost = 0;
        Cost = theinventory.SellItem();
        if (Cost != 0) {
            theassetmanager.GetComponent<AssetManager>().Sell();
        }
        CostText = Cost.ToString();
        //SellAll();
        _notice.SUB(CostText);
    }
    public void SellAll(){
        _notice.SUB(CostText);
    }
}
