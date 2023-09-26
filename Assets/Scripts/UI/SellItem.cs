using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItem : MonoBehaviour
{
    public Inventory theinventory;

    public int Cost;
    public void Clicksellbutton()
    {
        Cost = theinventory.SellItem();
        Debug.Log(Cost);
    }
}
