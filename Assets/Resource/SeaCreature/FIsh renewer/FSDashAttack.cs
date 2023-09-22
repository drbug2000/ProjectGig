using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSDashAttack : FishState
{
    NewShark shark;

    private Animator animator;
    private GameObject target;
    
    private Vector2 targetPos;
    enum attState { ready=1, setTarget=2, Dash=3, bite=4, shake=5, restore=6, end=7}
    attState State;
    /*
    attState tempState;
    int stateTrans { get tempState; set; }
    */
    //public 
    //private bool away;
    //private float awayTimer;
    
    private float attackTime;

    float timer;
    private float Timer
    {
        get { return timer; }
        set {
            timer = value;
            //Debug.Log("Timeset" +State);
            if (Timer <= 0)
            {
                switch (State)
                {
                    case attState.ready:
                        Debug.Log("Timeset" + State);
                        
                        
                        setState((int)attState.setTarget);
                        break;
                    case attState.setTarget:
                        Debug.Log("Timeset" + State);
                        
                        setState((int)attState.Dash);
                        break;
                    case attState.Dash:
                        Debug.Log("Timeset" + State);
                        
                        setState((int)attState.ready);
                        //((NewShark)fish).Bite = false;
                        break;
                    case attState.bite:
                        //timer =attackTime;
                        Debug.Log("Timeset" + State);
                        setState((int)attState.shake);
                        
                        fishfin.StopFish();
                        break;
                    case attState.shake:
                        Debug.Log("Timeset" + State);
                        
                        setState((int)attState.restore);
                        break;
                    case attState.restore:
                        Debug.Log("Timeset" + State);
                        
                        setState((int)attState.ready);
                        break;
                    case attState.end:

                        Debug.Log("Timeset" + State);
                        break;
                    default:
                        Debug.Log("attack FSM State error");
                        break;
                }
            }
        }
    }

    //attState를 설정하는 함수
    //State가 바뀌는 경우 필요한 코드를 작성(매 프레임이 아닌 한번 실행)
    public void setState( int state )
    {
        State = (attState)state;
        Debug.Log("state set" + State);
        switch (State) 
        {
            case attState.ready:
                timer = 2f;
                fishfin.SetDrag(1f);
                LookTarget();
                //fishfin.StopFish();
                break;
            case attState.setTarget:
                timer = 1f;
                //set target spot
                fishfin.SetSpot(fishfin.TransVector(target.transform.position));
                Debug.Log("player " + target.transform.position);
                Debug.Log("shark " + fish.GetComponent<Transform>().position);

                //change anime
                break;
            case attState.Dash:
                timer = 2f;
                //check target length
                //Ainime : Dash anime
                fishfin.ReDirSpot();
                Debug.Log("how far:" + fishfin.SpotDir.magnitude);
                Debug.Log("shark.aggroRange:" + shark.aggroRange);
                if (fishfin.SpotDir.magnitude > shark.aggroRange)
                {
                    //공격 해제
                    Debug.Log("attack end");
                    setState((int)attState.end);
                }
                else
                {
                    Debug.Log("Dash");
                    fishfin.SetDrag(0.05f);
                    Dash(fish.MaxSpeed);
                }


                Debug.Log("Spot " + fishfin.Spot);
                Debug.Log("SpotDir " + fishfin.SpotDir);
                Debug.Log("Dir " + fishfin.Dir);



                break;
            case attState.bite:
                timer = 1.5f;
                //add joint with target
                //Ainime : Stop anime
                Dash(fish.MaxSpeed);
                break;
            case attState.shake:
                timer = 4f;
                fishfin.SetDrag(1f);
                break;
            case attState.restore:
                timer = 1.5f;
                break;
            case attState.end:
                fish.DefaultState();
                break;
        }
    }

    public override void OnEnter(FishClass pfish, FishFin FF)
    {
        base.OnEnter(pfish, FF);
        //away = false;
        shark = (NewShark)pfish;
        attackTime = ((NewShark)this.fish).attackTime;
        Timer = attackTime;
        
        target = pfish.target;

        //animator = GetComponent<Animator>();
        fish.animator.SetBool("Detected", true);
        Debug.Log(" new FSATttack Enter");

        State = attState.ready;
    }

    //해당 state동작중 작동하는 코드를 작성(매 프레임 마다)
    public override void stateUpdate()
    {
        Timer -= Time.deltaTime;
        switch (State)
        {
            case attState.ready:
                
                /*
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
                */
                break;
            case attState.setTarget:
                //Dash(fish.MaxSpeed);
                //State = attState.biteWait;
                
                fishfin.StopFish();
                break;
            case attState.Dash:

                //Dash(fish.MaxSpeed);
                //State = attState.biteWait;
                if (shark.Bite)
                {
                    setState((int)attState.bite);
                }
                if(fishfin.velocityM < fish.MinSpeed * 0.9f)
                {
                    //Dash.
                    fishfin.SpotMove(fish.MaxSpeed * 0.8f);
                }
                break;
            case attState.bite:
                break;
            case attState.shake:
                Shake();
                break;
            case attState.restore:
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
        setDefault();
    }


    public void Dash(float speed)
    {
        fishfin.SpotMove(speed);
    }

    public void Shake()
    {
        int shakePer = 90;//방형전환 확률
        Vector2 dashdir;
        float shakespeed;
        if (fishfin.velocityM < 6)
        {
            Debug.Log("shake");
            dashdir = new Vector2(Random.Range(4, 8), Random.Range(-3, 3));
            shakespeed = Random.Range(7, fish.MaxSpeed);
            if (Percent(shakePer))
            {
                dashdir.x *= -1;
            }
            Debug.Log("speed " + fishfin.velocityM);
            Debug.Log("shake Dir " + dashdir);
            //fishfin.StopFish();
            fishfin.SetVelocity(dashdir.normalized * shakespeed);
        }
    }

    bool Percent(int a)
    {
        if (a > Random.Range(0, 100))
        {
            return true;
        }
        return false;
    }

    void LookTarget()
    {
        fishfin.SpotMove(0.1f);
    }

    public override void setDefault()
    {
        fishfin.SetDrag(fish.drag);
        fishfin.Speed = fish.speed;
       
    }

    /*
    public bool Bite()
    {
    //fish script에서 이 script의 함수를 직접 호출하는 것이 애매하다고 판단
    }
    */
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
