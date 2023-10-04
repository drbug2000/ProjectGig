using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openshop : MonoBehaviour
{
    public GameObject ShopImage;

    void Start() {
        ShopImage.SetActive(false);
    }

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
        StartCoroutine(CanOpenShop());
    }

}
