using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSAway : FishState
{
    
   
   

    public float awayTime;

    public void OnEnter(FishClass pfish, FishFin FF)
    {
        base.OnEnter(pfish, FF);


        awayTime = this.fish.awaytime;
        //fishfin.SetSpot()
        Debug.Log(" new FSAway OnEnter");
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
            fishfin.accelFin(fishfin.currentPos - fishfin.TransVector(fish.awaytarget.transform.position)
                ,fish.MaxSpeed / 0.8f);
        }

    }

    public void OnExit()
    {
        //fish.SetAway(fasle);
    }


}
