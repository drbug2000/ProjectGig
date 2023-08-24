using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShark : FishClass
{

    FSRoam roam;
    //FSaway away;
    FSAttack attack;
    bool awayNow;

    public float attackTime;
    public bool Bite;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        roam = new FSRoam();
        attack = new FSAttack();
        //away = new FSaway();
        SetState(roam);
        //Debug.Log("Second start");
        InvokeRepeating("FindAwayTarget", 2f, detectTime);
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
        if ((tar != null)&& ReferenceEquals(currentState, roam))
        {
            target = tar.gameObject;
            Debug.Log("overlap circle active target : " + target);
            SetState(attack);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.Bite = true;
        }

    }

}
