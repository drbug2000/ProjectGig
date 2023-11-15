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
	public particleController effectsystem;
	public GameObject storageManager;
    public AssetManager theassetmanager;

    public void Awake(){
        _notice = FindObjectOfType<NoticeUI>();
        effectsystem = GetComponent<particleController>();
    }
    
    public void Clicksellbutton()
    {
        Cost = 0;
        Cost = theinventory.SellItem();
        CostText = Cost.ToString();
        
        if (Cost != 0) {
            storageManager.GetComponent<StorageManager>().UpdateFish();
            theassetmanager.GetComponent<AssetManager>().Sell();
            _notice.SUB(CostText);
            
        }
        //effectsystem.StartMainEffect(1f);
        //SellAll();

    }
    public void SellAll(){
        _notice.SUB(CostText);
    }
}
