using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fpatrol : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    private float waitTime;
    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private SpriteRenderer Renderer;
    private Rigidbody2D fishRigidbody;

    Vector2 dir;

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        fishRigidbody = GetComponent<Rigidbody2D>();
        setNewSpot();
        
    }
    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed *Time.deltaTime);
        
        if (Vector2.Distance(transform.position, moveSpot.position) < 1)
        {
            if (waitTime <= 0)
            {
                setNewSpot();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if(fishRigidbody.velocity.magnitude < 1)
        {
            dir = moveSpot.position - transform.position;
            fishRigidbody.AddForce(dir * (3));
        }
    }
    public void setNewSpot()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        dir = moveSpot.position - transform.position;
        if(moveSpot.position.x > transform.position.x)
        {
                Renderer.flipX = false;
            }
        else
        {
                Renderer.flipX = true ;
            }

        fishRigidbody.AddForce(dir*(3));
    }
        

    
}

