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

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        setNewSpot();
        
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed *
        Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2)
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
    }
    public void setNewSpot()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if(moveSpot.position.x > transform.position.x)
        {
                Renderer.flipX = false;
            }
        else
        {
                Renderer.flipX = true ;
            }
    }
        

    
}

