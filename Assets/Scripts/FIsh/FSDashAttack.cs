using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSDashAttack : FishState
{
    NewShark shark;

    private Animator animator;
    private GameObject target;
    
    private Vector2 targetPos;
    private Rigidbody2D targetRD;

    private PlayerMove playermove;
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
    private float shakeTime;
    

    private int beforeShakeWay=1;

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
                        //Debug.Log("Timeset" + State);
                        
                        
                        setState((int)attState.setTarget);
                        break;
                    case attState.setTarget:
                        //Debug.Log("Timeset" + State);
                        
                        setState((int)attState.Dash);
                        break;
                    case attState.Dash:
                        //Debug.Log("Timeset" + State);
                        
                        setState((int)attState.ready);
                        //((NewShark)fish).Bite = false;
                        break;
                    case attState.bite:
                        //timer =attackTime;
                        //Debug.Log("Timeset" + State);
                        setState((int)attState.shake);
                        
                        //fishfin.StopFish();
                        break;
                    case attState.shake:
                        //Debug.Log("Timeset" + State);
                        
                        setState((int)attState.restore);
                        break;
                    case attState.restore:
                        //Debug.Log("Timeset" + State);
                        
                        setState((int)attState.ready);
                        break;
                    case attState.end:

                        //Debug.Log("Timeset" + State);
                        break;
                    default:
                        Debug.Log("attack FSM State error");
                        break;
                }
            }
        }
    }

    //attState�� �����ϴ� �Լ�
    //State�� �ٲ�� ��� �ʿ��� �ڵ带 �ۼ�(�� �������� �ƴ� �ѹ� ����)
    public void setState( int state )
    {
        State = (attState)state;
       // Debug.Log("state set" + State);
        //Debug.Log("Bite value" + shark.Bite);
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
                shark.Bite = false;
                //set target spot
                fishfin.SetSpot(fishfin.TransVector(target.transform.position));
                //Debug.Log("player " + target.transform.position);
               // Debug.Log("shark " + fish.GetComponent<Transform>().position);

                //change anime
                break;
            case attState.Dash:
                timer = 2f;
                //check target length
                //Ainime : Dash anime
                fishfin.ReDirSpot();
                //Debug.Log("how far:" + fishfin.SpotDir.magnitude);
                //Debug.Log("shark.aggroRange:" + shark.aggroRange);
                if (fishfin.SpotDir.magnitude > shark.aggroRange)
                {
                    //���� ����
                    //Debug.Log("attack end");
                    setState((int)attState.end);
                }
                else
                {
                    //Debug.Log("Dash");
                    fishfin.SetDrag(0.05f);
                    Dash(fish.MaxSpeed);
                }


                //Debug.Log("Spot " + fishfin.Spot);
                //Debug.Log("SpotDir " + fishfin.SpotDir);
                //Debug.Log("Dir " + fishfin.Dir);



                break;
            case attState.bite:
                timer = 1.5f;
                //add joint with target
                //shark.joint.connectedBody = targetRD;
                Bite(targetRD);
                //fish.particlecontroller.AddEffect(1f, fishfin.WhereMouth());
                
                //Ainime : Stop anime
                Dash(fish.MaxSpeed);
                break;
            case attState.shake:
                timer = shakeTime;
                fishfin.SetDrag(0.5f);
                break;
            case attState.restore:
                //shark.joint.connectedBody = null;
                SpitOut();
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
        //Debug.Log(" new FSATttack Enter");

        State = attState.ready;

        shakeTime = shark.shakeTime;

        targetRD = target.GetComponent<Rigidbody2D>();
        playermove = fish.target.GetComponent<PlayerMove>();

        target.GetComponent<IDamageable>().OnDamage(shark.shakeDamage, shark.gameObject, Vector2.zero, Vector2.zero);
    }

    //�ش� state������ �۵��ϴ� �ڵ带 �ۼ�(�� ������ ����)
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
                if (fishfin.IsTurn)
                {
                    playermove.Teleport(fishfin.WhereMouth());
                    fishfin.IsTurn = false;
                }
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
        if (shark.Bite)
        {
            playermove.Teleport(fishfin.WhereMouth());
        }
    }

    public void Bite(Rigidbody2D targetRigidbody)
    {
        shark.joint.connectedBody = targetRigidbody;
        shark.joint.enabled = true;
        playermove.GetBitten();
        playermove.Teleport(fishfin.WhereMouth());
        fish.particlecontroller.AddEffectLocal(1f, fishfin.WhereMouth() - fishfin.currentPos);
    }
    public void SpitOut()
    {
        //fishfin.SetSpot(fishfin.TransVector(target.transform.position));
        //fishfin.ReDirSpot();


        shark.joint.connectedBody = null;
        shark.joint.enabled = false;
        
        playermove.SpitOut(shark.spitforce*(fishfin.WhereMouth() - fishfin.currentPos));
        shark.Bite = false;//not work here

    }

    public void Shake()
    {
        int shakePer = 95;//������ȯ Ȯ��
        Vector2 dashdir;
        float shakespeed;
        if (fishfin.velocityM < shark.shakeMinSpeed)
        {
            //Debug.Log("shake");
            dashdir = new Vector2(Random.Range(6, 8), Random.Range(-3, 3));
            shakespeed = Random.Range(10, fish.MaxSpeed)* shark.shakeForce;
            if (Percent(shakePer))
            {
                beforeShakeWay *= -1;
                
            }
            
            //fishfin.StopFish();
            fishfin.SetVelocity(dashdir.normalized * shakespeed * beforeShakeWay);
            if (fishfin.UnderTheSea)
            {
                target.GetComponent<IDamageable>().OnDamage(shark.shakeDamage, shark.gameObject, Vector2.zero, Vector2.zero);
                
            }
            /*
            Debug.Log("shake speed " + shakespeed);
            Debug.Log("fish speed " + fishfin.velocityM);
            Debug.Log("shake Dir " + dashdir);
            */
        }
        if (fishfin.IsTurn)
        {
            playermove.Teleport(fishfin.WhereMouth());
            fish.particlecontroller.AddEffectLocal(2.5f, fishfin.WhereMouth() - fishfin.currentPos);
            fishfin.IsTurn = false;
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
    //���� ��ǥ�� ����ϴ� �Լ�
    //way parameter = -1 ������ ���� �̵� ����� �ݴ�� ���
    public Vector2 WhereMouth(int way = 1) 
    {
        //�ݶ��̴� ����� ���� ����� ��� ��ġ ����
        Vector2 mouth = fish.fishcollider.size;
        mouth *= new Vector2(-0.5f, -0.5f);
        //������ ����
        mouth += shark.mouthPosAdder + fish.fishcollider.offset;
        //��� ���⿡ ���� x��ǥ ���� ����
        if (!fishfin.IsLeft()) { mouth.x *= -1; Debug.Log("�ִ��� ��"); }
        else { Debug.Log("�ִ��� ����"); }
        
        if(way == -1) { mouth.x *= -1; }
        return fishfin.currentPos + mouth;

    }
    //fishfin���� �̵�
    */

    private void targetToMouth(Vector2 mouth)
    {
        playermove.Teleport(mouth);
    }

    /*
    public bool Bite()
    {
    //fish script���� �� script�� �Լ��� ���� ȣ���ϴ� ���� �ָ��ϴٰ� �Ǵ�
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
