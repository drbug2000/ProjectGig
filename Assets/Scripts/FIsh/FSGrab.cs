using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSGrab : FishState
{
    public override void OnEnter(FishClass pfish, FishFin FF)
    {
        base.OnEnter(pfish, FF);
        fishfin.SetSturn(true);
    }

    public override void stateUpdate()
    {
        fishfin.SetPosition(fish.target.transform.position);
    }

    public override void OnExit()
    {
        fishfin.SetSturn(false);
    }
}
