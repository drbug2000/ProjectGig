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

    float MinSpeed;

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
        setDefault();
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
                //fishfin.Speed = fish.speed / 5;
                
                //Debug.Log("in spot");
                waitTime -= Time.deltaTime;
                MinSpeed = fish.MinSpeed *  0.7f;
            }
        }
        else if (fishfin.SpotDistance < SpotMax)
        {
            fishfin.SetDrag(0.5f);
            //fishfin.Speed = fish.speed / 2;
            //fishtail.SetDrag(1f);
            //Debug.Log("almost spot");
            //Debug.Log(fishtail.SpotDistance());
        }

        //Àç°¡¼Ó
        if (fishfin.velocityM < MinSpeed)
        {
            fishfin.SpotMove();

        }
        
    }

    public override void OnExit()
    {
        setDefault();
    }

    void setNewSpot()
    {
        
        
        fishfin.SetSpot(new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)));
        //fishfin.StopFish();
        setDefault();
        //Debug.Log("new" + fishfin.Spot);
    }

    void setDefault()
    {
        fishfin.SetDrag(0.25f);
        fishfin.Speed = fish.speed;
        MinSpeed = fish.MinSpeed;
        waitTime = startWaitTime;
    }
}

