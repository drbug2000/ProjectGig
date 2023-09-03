using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFish : FishClass
{

    
    FSAway away;
    bool awayNow;

    
    public override void Awake()
    {
        base.Awake();
        away = new FSAway();
        //SetState(roam);
        //Debug.Log("Second start");
        InvokeRepeating("FindAwayTarget", 2f, detectTime);
    }


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();//SetStateDefault();
    }

    // Update is called once per frame
    public override void Update()
    {
        currentState.stateUpdate();
    }

    private void FindAwayTarget()
    {
        int palyermask = LayerMask.GetMask("Player");
        Collider2D tar = Physics2D.OverlapCircle(fishfin.currentPos, detectArea, palyermask);
        //Debug.Log(tar);
        if ((tar != null) && ReferenceEquals(currentState, roam))
        {
            awaytarget = tar.gameObject;
            Debug.Log("overlap circle active target : " + awaytarget);
            SetState(away);
        }
    }

    public override void DefaultState()
    {
        SetState(roam);
    }
    void SetAway(bool Baway)
    {
        awayNow = Baway;
    }

}
