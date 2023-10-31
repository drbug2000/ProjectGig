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
        theassetmanager.GetComponent<AssetManager>().Sell();
        CostText = Cost.ToString();
        SellAll();
    }
    public void SellAll(){
        _notice.SUB(CostText);
    }
}
