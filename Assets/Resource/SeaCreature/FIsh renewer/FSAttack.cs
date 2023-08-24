using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSAttack : FishState
{
    
    public float attackTime;
    public float Timer;
    public Animator animator;
    private GameObject target;
    private bool away;
    public float awayTimer;

    public override void OnEnter(FishClass pfish, FishFin FF)
    {
        base.OnEnter(pfish, FF);
        away = false;
        
        attackTime = ((NewShark)this.fish).attackTime;
        Timer = attackTime;
        
        target = pfish.target;

        //animator = GetComponent<Animator>();
        //animator.SetBool("Detected", true);
        Debug.Log(" new FSAway OnEnter");
    }
    public override void stateUpdate()
    {
        if (Timer <= 0)
        {
            fish.DefaultState();
        }
        else if(((NewShark)fish).Bite)
        {//player을 물었을때
            if (away)
            {
                
                while (awayTimer >= 0)
                {
                    if (fishfin.velocityM < fish.MinSpeed * 1.2f)
                    {
                        fishfin.SpotMoveBack(fish.MaxSpeed / 0.6f);
                    }
                    awayTimer -= Time.deltaTime;
                    return;
                }
                away = false;
                ((NewShark)fish).Bite = false;
                return;
            }else
            {
                
                Dash(fish.MaxSpeed);
                if (fishfin.velocityM < fish.MinSpeed *0.8f )
                {
                    awayTimer = attackTime / 0.3f;
                    away = true;
                }
                    
                //StartCoroutine("Away");
            }
        }
        else
        {//물기전, 물고 다시 물때
            Timer -= Time.deltaTime;
            if (fishfin.velocityM < fish.MinSpeed * 1.2f)
            {
                /*fishfin.accelFin(-(fishfin.currentPos - fishfin.TransVector(fish.awaytarget.transform.position))
                , fish.MaxSpeed / 0.6f);*/
                Dash(fish.MaxSpeed / 0.6f);
            }
        }

    }

    public override void OnExit()
    {
        //animator.SetBool("Detected", false);
        //fish.SetAway(fasle);
    }


    public void Dash(float speed)
    {
        fishfin.SetSpot(fishfin.TransVector(target.transform.position));
        fishfin.SpotMove(speed);
    }

    IEnumerator Away()
    {
        Timer = attackTime /0.5f ;
        while (Timer <= 0) {
            if (fishfin.velocityM < fish.MinSpeed * 1.2f)
            {
                fishfin.SpotMoveBack(fish.MaxSpeed / 0.6f);
            }
            Timer -= Time.deltaTime;
            yield return null;
        }
        

    }

}
