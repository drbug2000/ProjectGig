using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    public Inventory theinventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (theinventory == null || item == null)
            {
                Debug.Log("theinventory가 비었습니다.");
            }
            theinventory.GetComponent<Inventory>().AcquireItem(item);
            Destroy(this.gameObject);
        }
    }
}
