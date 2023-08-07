using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSRoam : FishState
{
    
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float waitTime;
    public float startWaitTime;

    float SpotMax;
    float SpotMin;

    public override void OnEnter(FishClass pfish, FishFin FF)
    {
        base.OnEnter(pfish,FF);


        minX = fish.RoamBoxMinX;
        maxX = fish.RoamBoxMaxY;
        minY = fish.RoamBoxMinY;
        maxY = fish.RoamBoxMaxY;

        startWaitTime = fish.RoamWaitTime;
        waitTime = fish.RoamWaitTime;

        SpotMax = fish.SpotRangeBig;
        SpotMin = fish.SpotRangeSmall;
        setNewSpot();

        Debug.Log(" new FSRoam OnEnter");
    }
    public override void stateUpdate()
    {
        
        if (fishfin.SpotDistance <SpotMin)
        {
            if (waitTime <= 0)
            {
                setNewSpot();
            }
            else
            {
                fishfin.Speed = fish.speed / 5;
                //Debug.Log("in spot");
                waitTime -= Time.deltaTime;
            }
        }
        else if (fishfin.SpotDistance < SpotMax)
        {
            fishfin.Speed = fish.speed / 2;
            //fishtail.SetDrag(1f);
            //Debug.Log("almost spot");
            //Debug.Log(fishtail.SpotDistance());
        }

        //Àç°¡¼Ó
        if (fishfin.velocityM < fish.MinSpeed)
        {
            fishfin.SpotMove();

        }
        
    }

    public override void OnExit()
    {

    }

    void setNewSpot()
    {
        waitTime = startWaitTime;
        fishfin.Speed = fish.speed;
        fishfin.SpotMove(new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)));
       

        Debug.Log("new" + fishfin.Spot);
    }
}

