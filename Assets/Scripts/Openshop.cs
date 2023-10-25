using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Openshop : MonoBehaviour
{
    public GameObject ShopImage;
    public Button _setActiveShop;
    private bool buttonPressed = false;

    void Start() {
        ShopImage.SetActive(false);
        Button btn = _setActiveShop.GetComponent<Button>();
    }
/*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            //Debug.Log("TriggerOn");
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("P");
                setActiveShop();
                
            }
        }
    }
    */
    public void setActiveShop(){
        //Debug.Log("B");
        if (ShopImage.activeSelf == true) {
            Time.timeScale = 1f;
            ShopImage.SetActive(false);
        }
        else if (ShopImage.activeSelf == false) {
            Time.timeScale = 0f;
            ShopImage.SetActive(true);
        }
        StartCoroutine(CanOpenShop());
    }

    //아래부터는 코루틴
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            StartCoroutine(CanOpenShop());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            if (ShopImage.activeSelf == true) {
                ShopImage.SetActive(false);
                Time.timeScale = 1f;
            }
            StopAllCoroutines();
        }
    }

    IEnumerator CanOpenShop()
    {
        yield return null;
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ShopImage.activeSelf == true) {
                Time.timeScale = 1f;
                ShopImage.SetActive(false);
            }
            else if (ShopImage.activeSelf == false) {
                Time.timeScale = 0f;
                ShopImage.SetActive(true);
            }
        }
        
        else if (_setActiveShop != null && _setActiveShop.interactable) {
            if (ShopImage.activeSelf == true) {
                Time.timeScale = 1f;
            }
        }
        
        StartCoroutine(CanOpenShop());
    }
}
