using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishState
{
    protected FishFin fishfin;
    protected FishClass fish;
    

    public virtual void OnEnter(FishClass pfish, FishFin FISHFIN) 
    {
        this.fishfin = FISHFIN; 
        this.fish = pfish;
        
    }
    public virtual void stateUpdate() { }
    public virtual void OnExit() { }
    public virtual void setDefault() { }
    /*
        fishfin.SetDrag(fish.drag);
        fishfin.Speed = fish.speed;
        //MinSpeed = fish.MinSpeed;
        //waitTime = startWaitTime;
    */
}
