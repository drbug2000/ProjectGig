using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // inventoryparent을 활성화하고 비활성화하는 역할을 할 것이다. 다시 말해 이 창이 켜지면 inventoy가 켜질 것 이고 이 창이 꺼지면 inventory가 꺼질 것이다.
    [SerializeField]
    private GameObject go_InventoryBase;
    // 모든 slot들의 컴포넌트에 접근하기위해 받아왔다.
    [SerializeField] 
    public ItemSlotUI[] slots;

    private bool inventoryActivated;

    public int allcost;
    void Start()
    {
        // 처음 실행할 때는 inventory는 비활성화 시켜야한다.
        inventoryActivated = false;
        allcost = 0;
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount();
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item);
                return;
            }
        }
    }

    public int SellItem()
    {
        int multiplycost;
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log(i);
            if (slots[i].item == null)
            {
                return allcost;
            }
            multiplycost = slots[i].item.Cost  * slots[i].itemCount;
            allcost += multiplycost;
            slots[i].ClearSlot();
        }

        return allcost;
    }
}
