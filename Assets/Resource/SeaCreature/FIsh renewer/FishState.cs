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

}
