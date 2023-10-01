using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShark : FishClass
{

    //FSRoam roam;
    //FSaway away;
    //FSAttack attack;
    FSDashAttack attack;
    bool awayNow;
    public float aggroRange;

    public float attackTime;
    public bool Bite;
    public float shakeTime;

    public FixedJoint2D joint;

    public float spitforce;
    public float shakeForce;
    public float shakeMinSpeed;

    public int shakeDamage;


    public override void Awake()
    {
        base.Awake();
        //roam = new FSRoam();
        //attack = new FSAttack();
        attack = new FSDashAttack();
        //away = new FSaway();
        //SetState(roam);
        //Debug.Log("Second start");
        InvokeRepeating("FindAttackTarget", 2f, detectTime);

        joint = GetComponent<FixedJoint2D>();
        joint.enabled = false;
    }

    // Start is called before the first frame update
    public override void Start() {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();//SetStateDefault();
    }

    // Update is called once per frame
    public override void Update()
    {
        //현재 State에 함수를 매 프레임 실행함
        currentState.stateUpdate();
    }

    private void FindAttackTarget()
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

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.name == "Player" && ReferenceEquals(currentState,attack))
        {
            this.Bite = true;
            Debug.Log("bite value true");

        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //base.OnCollisionEnter2D(collision);
        if (other.gameObject.name == "Player" && ReferenceEquals(currentState, attack))
        {
            this.Bite = true;
            Debug.Log("bite value true");

        }
    }
}
