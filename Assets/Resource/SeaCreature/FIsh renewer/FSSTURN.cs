using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSSTURN : FishState
{
    float sturnTime;

    public override void OnEnter(FishClass pfish, FishFin FF)
    {
        sturnTime = fish.sturntime;
        fishfin.SetSturn(true);
    }

    public override void stateUpdate()
    {
        if (sturnTime < 0)
        {
            fish.DefaultState();
        }
        else {
            sturnTime -= Time.deltaTime;
        }
    }

    public override void OnExit()
    {
        fishfin.SetSturn(false);
    }
}