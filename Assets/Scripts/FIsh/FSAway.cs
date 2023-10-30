using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSAway : FishState
{




    private float awayTime;
    

    public override void OnEnter(FishClass pfish, FishFin FF)
    {
        base.OnEnter(pfish, FF);


        awayTime = this.fish.awaytime;
        //fishfin.SetSpot()
        fishfin.accelFin(fishfin.currentPos - fishfin.TransVector(fish.awaytarget.transform.position)
                , fish.MaxSpeed / 0.8f);
    }
    public override void stateUpdate()
    {
        if (awayTime <= 0)
        {

            fish.DefaultState();

        }
        else
        {
            awayTime -= Time.deltaTime;

            if (fishfin.velocityM < fish.awaySpeed)
            {
                fishfin.accelFin(fishfin.currentPos - fishfin.TransVector(fish.awaytarget.transform.position)
                , fish.MaxSpeed / 0.8f);

            }
            
        }

    }

    public override void OnExit()
    {
        //fish.SetAway(fasle);
    }


}
