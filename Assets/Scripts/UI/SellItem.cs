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

    public void Awake(){
        _notice = FindObjectOfType<NoticeUI>();
    }
    
    public void Clicksellbutton()
    {
        Cost = theinventory.SellItem();
        CostText = Cost.ToString();
        SellAll();
        Debug.Log(Cost);
    }
    public void SellAll(){
        _notice.SUB(CostText);
    }
}
