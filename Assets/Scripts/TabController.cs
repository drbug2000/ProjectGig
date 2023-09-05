using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    private static TabController _instance = null;

    public static TabController Instance{
        get{
            if(_instance == null){
                GameObject.FindObjectOfType<TabController>();

                if(_instance == null){
                    Debug.LogError("There's no active TabController object");
                }
            }
            return _instance;
        }
    }
    
    TabSet tabButton;

  // Start is called before the first frame update
  void Start()
    {
        //tabButton = FindObjectOfType<TabSet>();
        SelectedButton(transform.GetChild(0).GetComponent<TabSet>());
    }

    public void SelectedButton(TabSet _button){
        if(tabButton != null){
            tabButton.Deselect();
        }
        
        tabButton = _button;
        tabButton.Select();
        tabButton.Deselect();
    }
}
