using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashsurface : MonoBehaviour
{
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
}
