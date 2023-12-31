using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSaway : FState
{
    FishTail fishtail;
    Tail tail;
    Fish fish;
    
    public float awayTime;

    public void OnEnter(Fish pfish, FishTail FT)
    {
        this.fish = pfish;
        fishtail = FT;
        tail = new Tail();
        fishtail.SetTail(tail);


        awayTime = 5f;


        tail.SetSpot( -this.fish.awaytarget.transform.position);
        tail.Speed = fish.speed*1.5f;
        fishtail.SetTail(tail);
        fishtail.StopFish();

        Debug.Log(" FSaway OnEnter");
    }
    public void stateUpdate()
    {
        if (awayTime <= 0)
        {
            
            fish.DefaultState();

        }
        else
        {
            awayTime -= Time.deltaTime;
        }

    }

    public void OnExit()
    {
        //fish.SetAway(fasle);
    }

    
}
