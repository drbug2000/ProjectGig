using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFish : Fish
{

    FSroam roam;
    FSaway away;
    bool awayNow;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        roam= new FSroam();
        away = new FSaway();
        SetState(roam);

        InvokeRepeating("FindAwayTarget", 0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.stateUpdate();


    }

    private void FindAwayTarget() 
    {
        
        int palyermask = LayerMask.GetMask("player");

        Collider2D tar = Physics2D.OverlapCircle(fishtail.currentPos, 3f, palyermask);
        if ( (tar != null) && currentState==roam)
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
