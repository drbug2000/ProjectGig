using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSroam : FState
{
    FishTail fishtail;
    Tail tail;
    Fish fish;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float waitTime;
    public float startWaitTime;

    public void OnEnter(Fish pfish, FishTail FT)
    {
        this.fish = pfish;
        fishtail = FT;
        tail = new Tail();
        fishtail.SetTail(tail);
      
        minX = -50;
        maxX = 50;
        minY = -50;
        maxY = 0;
        startWaitTime = 5;

        setNewSpot();

        Debug.Log(" FSroam OnEnter");
    }
    public void stateUpdate()
    {

        if (fishtail.SpotDistance() < 5)
        {
            tail.Speed = fish.speed/2;
            //fishtail.SetDrag(1f);
            //Debug.Log("almost spot");
        }
        else if (fishtail.SpotDistance() < 3)
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

    public void OnExit() 
    { 
    
    }

    void setNewSpot()
    {
        waitTime = startWaitTime;
        tail.SetSpot( new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)));
        tail.Speed = fish.speed;
       

        fishtail.SetTail(tail);

        Debug.Log("1" + tail.Spot);
    }
}

