using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underthesea : MonoBehaviour
{
    public PlayerMove theplayermove;
    public float escapeforce;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            theplayermove.onboard = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            theplayermove.onboard = true;
            
        }
        if (collision.gameObject.name == "Player 1")
        {
            Debug.Log(collision.gameObject.transform.position.y + ": " + transform.position.y);
            if (collision.gameObject.transform.position.y > transform.position.y)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new(0, escapeforce));
            }
        }
    }

    //OnEnter
}
