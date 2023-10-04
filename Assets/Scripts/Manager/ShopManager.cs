using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public AssetManager assetManager;
    // Start is called before the first frame update
    void Start()
    {
        //assetManager = GetComponent<AssetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OxygenLvlUp(){
        if (GameManager.Instance.Gold >= 1000){
            assetManager.Buy();
            GameManager.Instance.HpLvl++;
            Debug.Log("OxygenLvl:"+ GameManager.Instance.HpLvl);
        }
        else{
            Debug.Log("OxygenLvl:"+ GameManager.Instance.HpLvl);
            Debug.Log("Gold 부족");
        }
    }
    public void DamageLvlUp(){
        if (GameManager.Instance.Gold >= 1000){
            assetManager.Buy();
            GameManager.Instance.GigDamLvl++;
            Debug.Log("DamageLvl:"+ GameManager.Instance.GigDamLvl);
        }
        else{
            Debug.Log("DamageLvl:"+ GameManager.Instance.GigDamLvl);
            Debug.Log("Gold 부족");
        }
    }
    public void RangeLvlUp(){
        if (GameManager.Instance.Gold >= 1000){
            assetManager.Buy();
            GameManager.Instance.GigRangeLvl++;
            Debug.Log("RangeLvl:"+ GameManager.Instance.GigRangeLvl);
        }
        else{
            Debug.Log("RangeLvl:"+ GameManager.Instance.GigRangeLvl);
            Debug.Log("Gold 부족");
        }
    }
}
