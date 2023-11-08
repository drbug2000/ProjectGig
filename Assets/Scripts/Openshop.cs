using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Openshop : MonoBehaviour
{
    public GameObject ShopImage;
    public Button _setActiveShop;
    public GameObject storageManager;


    void Start() {
        ShopImage.SetActive(false);
    }

    public void setActiveShop(){
        //Debug.Log("B");
        if (ShopImage.activeSelf == true) {
            //Debug.Log("on");
            Time.timeScale = 1f;
            ShopImage.SetActive(false);
        }
        else if (ShopImage.activeSelf == false) {
            //Debug.Log("off");
            Time.timeScale = 0f;
            ShopImage.SetActive(true);
        }
        //StartCoroutine(CanOpenShop());
    }


    //아래부터는 코루틴
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            StartCoroutine("CanOpenShop");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            Debug.Log("exit");
            if (ShopImage.activeSelf) {
                ShopImage.SetActive(false);
                GameManager.Instance.resumeGame();
            }
            StopCoroutine("CanOpenShop");
        }
    }

    IEnumerator CanOpenShop()
    {
        //yield return null;
        //Debug.Log("coroutine");
        while(true){
            Debug.Log("coroutine");
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("P");
                if (ShopImage.activeSelf) {
                    GameManager.Instance.resumeGame();
                    ShopImage.SetActive(false);
                }
                else if (!ShopImage.activeSelf) {
                    storageManager.GetComponent<StorageManager>().UpdateFish();
                    GameManager.Instance.pauseGame();
                    ShopImage.SetActive(true);
                }
            }
            yield return null;
        }
        //StartCoroutine(CanOpenShop());
    }
}
