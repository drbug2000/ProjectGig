using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Fish
{
    FSroam roam;
    FSthreat threat;
    FSattack attack;
    FSaway away;
    bool awayNow;

    int Damage;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        roam = new FSroam();
        threat = new FSthreat();
        attack = new FSattack();
        away = new FSaway();
        SetState(roam);

        InvokeRepeating("FindAwayTarget", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.stateUpdate();
    }

    private void FindAwayTarget()
    {

        int palyermask = LayerMask.GetMask("player");

        Collider2D tar = Physics2D.OverlapCircle(fishtail.currentPos, 4f, palyermask);
        if ((tar != null) && currentState == roam)
        {
            awaytarget = tar.gameObject;
            SetState(attack);
        }


    }
}
