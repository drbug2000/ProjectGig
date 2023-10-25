using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openshop : MonoBehaviour
{
    public GameObject ShopImage;

    void Start() {
        ShopImage.SetActive(false);
    }

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

    public void setActiveShop(){
        //Debug.Log("B");
        if (ShopImage.activeSelf == true) {
                ShopImage.SetActive(false);
        }
        else if (ShopImage.activeSelf == false) {
                ShopImage.SetActive(true);
        }
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
        else if (Input.GetMouseButtonDown(0)) {
            if (ShopImage.activeSelf == true) {
                Time.timeScale = 1f;
            }
        }
        StartCoroutine(CanOpenShop());
    }
}
