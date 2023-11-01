using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    void start(){
        gameObject.SetActive(false);
    }
    public AssetManager assetManager;
    //gun script에 upgrade 전달
    public Action OxygenUpgrade;
    public Action DamageUpgrade;
    public Action RangeUpgrade;

    public ItemSlotUI[] itemslotui;
    public Item[] item;
    public UnityEngine.UI.Image[] image;

    void Start() {
        itemslotui = new ItemSlotUI[8];
        item = new Item[8];
        image = new UnityEngine.UI.Image[8];
    }

    #region  Buy
    public void OxygenLvlUp(){
        if (GameManager.Instance.Gold >= 1000){
            assetManager.Buy();
            GameManager.Instance.HpLvl++;
            OxygenUpgrade();
            // Debug.Log("OxygenLvl:"+ GameManager.Instance.HpLvl);
        }
        else{
            // Debug.Log("OxygenLvl:"+ GameManager.Instance.HpLvl);
            // Debug.Log("Gold 부족");
        }
    }
    public void DamageLvlUp(){
        if (GameManager.Instance.Gold >= 1000){
            assetManager.Buy();
            GameManager.Instance.GigDamLvl++;
            DamageUpgrade();
            // Debug.Log("DamageLvl:"+ GameManager.Instance.GigDamLvl);
        }
        else{
            // Debug.Log("DamageLvl:"+ GameManager.Instance.GigDamLvl);
            // Debug.Log("Gold 부족");
        }
    }
    public void RangeLvlUp(){
        if (GameManager.Instance.Gold >= 1000){
            assetManager.Buy();
            GameManager.Instance.GigRangeLvl++;
            RangeUpgrade();
            // Debug.Log("RangeLvl:"+ GameManager.Instance.GigRangeLvl);
        }
        else{
            // Debug.Log("RangeLvl:"+ GameManager.Instance.GigRangeLvl);
            // Debug.Log("Gold 부족");
        }
    }
    public void ShopActive(){
        gameObject.SetActive(true);
    }

    #endregion

    #region Sell
    // inventory의 변경사항을 다시 불러오는 역할을 할 것이다.
    public void CallInventory() {
        for (int i = 0; i < 8; ++i) {
            if (itemslotui[i].item == null) {
                break;
            }
            item[i] = itemslotui[i].item;
        }
    }

    // Item 목록을 리스트에 저장을 한 뒤 상점에 item 이미지 띄우기
    public void ShowInventory() {
        for (int i = 0; i < 8; ++i) {
            image[i].sprite = item[i].itemImage;
        }
    }

    #endregion
}
