using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FState 
{

    public void OnEnter(Fish fish,FishTail fishtail);
    public void stateUpdate();
    public void OnExit();
    
}
