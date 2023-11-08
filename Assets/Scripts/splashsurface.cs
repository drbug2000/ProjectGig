using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashsurface : MonoBehaviour
{

    public particleController particlecontroller;


    void Start()
    {
        particlecontroller = GetComponent<particleController>();

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.tag == "Player")
        {
            theplayermove.onboard = false;
        }
        */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //collision.gameObject.transform.position;

        //theplayermove.onboard = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        splashEffect(collision);

    }

    private void splashEffect(Collider2D collision)
    {
        Debug.Log("splash");
        Vector2 position = collision.transform.position;
        Vector3 size = collision.bounds.size;

        particlecontroller.AddEffect(3f, position, 5*(int)size.x, -1f); //size.x*size.y);



    }
}
