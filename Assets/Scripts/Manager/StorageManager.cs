using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageManager : MonoBehaviour
{
    public TMP_Text value1;
    public TMP_Text value2;
    public TMP_Text value3;
    public TMP_Text value4;

    public int littleFishCount = 0;
    public int middleFishCount = 0;
    public int bigFishCount = 0;
    public int sharkCount = 0;

    public GameObject[] slot = null;
    /*
    public ItemSlotUI itemSlotUI;
    public Inventory inventory;
    */
    public void CheckFish(){
        if(slot[0].GetComponent<ItemSlotUI>().item == null){
                Debug.Log("null");
                ResetFish();
            }
        for(int i=0; i<slot.Length; i++){
            
            if(slot[i].GetComponent<ItemSlotUI>().item != null){
                if(slot[i].GetComponent<ItemSlotUI>().item.itemName == "littlefish"){
                    littleFishCount = slot[i].GetComponent<ItemSlotUI>().itemCount;
                } else if(slot[i].GetComponent<ItemSlotUI>().item.itemName == "middlefish"){
                    middleFishCount = slot[i].GetComponent<ItemSlotUI>().itemCount;
                    Debug.Log("middle");
                } else if(slot[i].GetComponent<ItemSlotUI>().item.itemName == "bigfish"){
                    Debug.Log("big");
                    bigFishCount = slot[i].GetComponent<ItemSlotUI>().itemCount;
                } else if(slot[i].GetComponent<ItemSlotUI>().item.itemName == "shark"){
                    sharkCount = slot[i].GetComponent<ItemSlotUI>().itemCount;
                }
            } 
        }
    }

    public void UpdateFish(){
        Debug.Log("Update");
        CheckFish();
        value1.text = littleFishCount.ToString();
        value2.text = middleFishCount.ToString();
        value3.text = bigFishCount.ToString();
        value4.text = sharkCount.ToString();
    }

    public void ResetFish(){
        littleFishCount = 0;
        middleFishCount = 0;
        bigFishCount = 0;
        sharkCount = 0;
    }

    void Start()
    {
        /*
        slot = new GameObject[8];
        for(int i=0; i<slot.Length; i++){
            slot[i] = GameObject.Find("Slot_0"+i);
        }
        */
    }
    void Awake()
    {
        UpdateFish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
