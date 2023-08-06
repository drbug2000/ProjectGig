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

    float SpotMax ;
    float SpotsMin;

    public void OnEnter(Fish pfish, FishTail FT)
    {
        this.fish = pfish;
        fishtail = FT;
        tail = new Tail();
        fishtail.SetTail(tail);
      
        minX = fish.RoamBoxMinX;
        maxX = fish.RoamBoxMaxX;
        minY = fish.RoamBoxMaxY;
        maxY = fish.RoamBoxMinX;
        startWaitTime = fish.RoamBoxMaxX;

        SpotMax = fish.SpotRangeBig;
        SpotsMin = fish.SpotRangeBig;
        setNewSpot();

        Debug.Log(" FSroam OnEnter");
    }
    public void stateUpdate()
    {

        if (fishtail.SpotDistance() < SpotMax)
        {
            tail.Speed = fish.speed/2;
            fishtail.SetDrag(1f);
            //Debug.Log("almost spot");
        }
        else if (fishtail.SpotDistance() < SpotsMin)
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

