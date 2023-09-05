using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabSet : MonoBehaviour
{
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    public void Select(){
        if(onTabSelected != null){
            onTabSelected.Invoke();
        }
    }

    public void Deselect(){
        if(onTabDeselected != null){
            onTabDeselected.Invoke();
        }
    }

    public void OnSelectTab(TabSet button){
        TabController.Instance.SelectedButton(button);
    }
}
