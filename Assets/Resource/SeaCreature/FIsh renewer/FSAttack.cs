using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSAttack : FishState
{
    
    private Animator animator;
    private GameObject target;
    
    enum attState { follow , bite ,biteWait, away, end}
    attState State;
    //private bool away;
    //private float awayTimer;

    private float attackTime;

    float timer;
    private float Timer
    {
        get { return timer; }
        set {
            timer = value;
            Debug.Log(State);
            if(Timer <= 0)
            {
                switch (State)
                {
                    case attState.follow:
                        fish.DefaultState();
                        //timer = 1f;
                        //State = attState.bite;
                        break;
                    case attState.bite:
                        timer = 2f;
                        
                        //State = attState.away;
                        break;
                    case attState.biteWait:
                        timer = 2f;
                        State = attState.away;
                        ((NewShark)fish).Bite = false;
                        break;
                    case attState.away:
                        timer = attackTime;
                        State = attState.follow;
                        fishfin.StopFish();
                        break;
                    case attState.end:
                        break;
                    default:
                        Debug.Log("attack FSM State error");
                        break;
                }
            }
        }
    }

    public override void OnEnter(FishClass pfish, FishFin FF)
    {
        base.OnEnter(pfish, FF);
        //away = false;
        
        attackTime = ((NewShark)this.fish).attackTime;
        Timer = attackTime;
        
        target = pfish.target;

        //animator = GetComponent<Animator>();
        fish.animator.SetBool("Detected", true);
        Debug.Log(" new FSATttack Enter");
    }
    public override void stateUpdate()
    {
        Timer -= Time.deltaTime;
        switch (State)
        {
            case attState.follow:
                if (((NewShark)fish).Bite)
                {
                    State = attState.bite;
                    Timer = 0.5f;
                    Debug.Log("bite");
                    break;
                }

                if (fishfin.velocityM < fish.MinSpeed)
                {
                    Dash(fish.speed * 0.7f);
                }
                break;
            case attState.bite:
                Dash(fish.MaxSpeed);
                State = attState.biteWait;
                break;
            case attState.biteWait:
                //Dash(fish.MaxSpeed);
                break;
            case attState.away:
                if (fishfin.velocityM < fish.MinSpeed * 0.9f)
                {

                    fishfin.SpotMoveBack(fish.MaxSpeed * 0.8f);
                }
                break;
            case attState.end:
                break;
            default:
                Debug.Log("attack FSM State error");
                break;
        }

    }

    public override void OnExit()
    {
        fish.animator.SetBool("Detected", false);
        //fish.SetAway(fasle);
    }


    public void Dash(float speed)
    {
        fishfin.SetSpot(fishfin.TransVector(target.transform.position));
        fishfin.SpotMove(speed);
    }

    /*
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
    */
}
