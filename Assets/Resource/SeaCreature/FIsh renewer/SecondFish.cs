using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFish : FishClass
{

    FSRoam roam;
    FSAway away;
    bool awayNow;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        roam = new FSRoam();
        away = new FSAway();
        SetState(roam);
        //Debug.Log("Second start");
        InvokeRepeating("FindAwayTarget", 0f, 1f);
    }

    // Update is called once per frame
    public override void Update()
    {
        currentState.stateUpdate();


    }

    private void FindAwayTarget()
    {
        //Debug.Log("new fish envoke");
        int palyermask = LayerMask.GetMask("Player");
        //int palyermask = 7;
        Collider2D tar = Physics2D.OverlapCircle(fishfin.currentPos, 1f, palyermask);
        if ((tar != null) && currentState == roam)
        {
            awaytarget = tar.gameObject;
            SetState(away);
        }
        

    }
    void DefaultState()
    {
        SetState(roam);
    }
    void SetAway(bool Baway)
    {
        awayNow = Baway;

    }

}
