using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSattack : FState
{

    FishTail fishtail;
    Tail tail;
    Fish fish;
    public void OnEnter(Fish pfish, FishTail FT)
    {
        this.fish = pfish;
        fishtail = FT;
        tail = new Tail();
        fishtail.SetTail(tail);

        tail.SetSpot(this.fish.awaytarget.transform.position);
        tail.Speed = fish.speed * 1.5f;
        fishtail.SetTail(tail);
        fishtail.StopFish();

        Debug.Log(" FSattack");
    }

    public void stateUpdate()
    {

    }

    public void OnExit()
    {

    }
}
