using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지
    public int _count = 1;
    
    [SerializeField]
    private TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    // 아이템 이미지의 투명도 조절
    // private void SetColor(float _alpha)
    // {
    //     Color color = itemImage.color;
    //     color.a = _alpha;
    //     itemImage.color = color;
    // }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.itemImage;
        Debug.Log(this.gameObject.name);

        if(item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

    }

    // 해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount()
    {
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}
