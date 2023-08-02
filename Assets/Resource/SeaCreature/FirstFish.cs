using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFish : Fish
{

    FSroam roam;
    FSaway away;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        roam= new FSroam();
        away = new FSaway();
        SetState(roam);

        InvokeRepeating("FindAwayTarget", 0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.stateUpdate();


    }

    private void FindAwayTarget() 
    {
        
        int palyermask = LayerMask.GetMask("player");

        Collider2D tar = Physics2D.OverlapCircle(fishtail.currentPos, 5f, palyermask);
        if (tar != null)
        {
            awaytarget = tar.gameObject;
            SetState(away);
        }

        
    }
    void DefaultState()
    {
        SetState(roam);
    }

}
