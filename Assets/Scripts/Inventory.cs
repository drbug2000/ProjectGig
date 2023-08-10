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
    private ItemSlotUI[] slots;

    private bool inventoryActivated;

    public GameObject selector;

    private int selectorpoint = 0;

    void Start()
    {
        // 처음 실행할 때는 inventory는 비활성화 시켜야한다.
        inventoryActivated = false;
    }

    void Update()
    {
        TryOpenInventory();

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelector("right");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelector("left");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelector("up");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelector("down");
        }
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
        if(Item.ItemType.Equipment != _item.itemType)
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

    void MoveSelector(string direction)
    {
        switch (direction)
        {
            case "right":
                {
                    if (selectorpoint != 44)
                    {
                        selectorpoint++;
                        selector.transform.position = slots[selectorpoint].transform.position;
                    }
                    break;
                }
            case "left":
                {
                    if (selectorpoint != 0)
                    {
                        selectorpoint--;
                        selector.transform.position = slots[selectorpoint].transform.position;
                    }
                    break;
                }
            case "up":
                {
                    if (selectorpoint > 8)
                    {
                        selectorpoint -= 9;
                        selector.transform.position = slots[selectorpoint].transform.position;
                    }
                    break;
                }
            case "down":
                {
                    if (selectorpoint < 36)
                    {
                        selectorpoint += 9;
                        selector.transform.position = slots[selectorpoint].transform.position;
                    }
                    break;
                }
        }

        /*if (pickingUpItem != null)
        {
            pickingUpItem.transform.position = selector.transform.position;
        }*/

        // ChangeName();
    }
}
