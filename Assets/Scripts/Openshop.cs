using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Openshop : MonoBehaviour
{
    public GameObject ShopImage;
    public Button _setActiveShop;

    void Start() {
        ShopImage.SetActive(false);
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
            if (ShopImage.activeSelf) {
                ShopImage.SetActive(false);
                GameManager.Instance.resumeGame();
            }
            StopCoroutine(CanOpenShop());
        }
    }

    IEnumerator CanOpenShop()
    {
        yield return null;
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ShopImage.activeSelf) {
                GameManager.Instance.resumeGame();
                ShopImage.SetActive(false);
            }
            else if (!ShopImage.activeSelf) {
                GameManager.Instance.pauseGame();
                ShopImage.SetActive(true);
            }
        }

        StartCoroutine(CanOpenShop());
    }
}
