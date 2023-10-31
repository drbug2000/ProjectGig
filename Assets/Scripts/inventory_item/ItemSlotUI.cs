using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemSlotUI : MonoBehaviour
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지
    
    [SerializeField]
    private TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    void Start()
    {
        SetColor(0f);
        itemCount = 0;
    }

    //아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.itemImage;
        go_CountImage.SetActive(true);
        SetColor(255f);
        text_Count.text = itemCount.ToString();
        SetSlotCount();

    }

    // 해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount()
    {
        itemCount += 1;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemCount = 0;
        text_Count.text = "";
        SetColor(0f);
    }
}
