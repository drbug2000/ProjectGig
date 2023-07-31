using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underthesea : MonoBehaviour
{
    public PlayerMove theplayermove;
    public i_move T;


    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theplayermove.onboard = false;
            i_move.Mchange();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theplayermove.onboard = true;
        }
    }
}
