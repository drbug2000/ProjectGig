using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishClass : MonoBehaviour
{


    protected FishFin fishfin;
    protected FishHealth FishHP;
    public static FishSpawn spawner;
    public FishState currentState { get; private set; }
    public GameObject target;
    public GameObject awaytarget;
    public Animator animator;
    public CapsuleCollider2D fishcollider;

    public Item item;


    //기본 상태
    public FSRoam roam;
    public FSGrab grab;
    public FSDEAD dead;
    public FSSTURN sturn;
    //FScatched;

    //HP 관련 변수
    public int startHP;

    //움직임 관련 변수
    // public float startWaitTime;
    //public float waitTime;
    //public Transform moveSpot;

    //public SpriteRenderer Renderer;
    //public Rigidbody2D fishRigidbody;

    //물고기 특성 관련 변수
    public float DefaultSize;//물고기 기본 크기(물고기 종류 고유값)
    public float RatioSize=1;//곱해지는 크기 (DefaultSize * RatioSize = 실제 크기)

    public float mass;//무게
    public float drag;//저항
    public float gravity;//받는 중력
    public float speed;

    public float MaxSpeed;
    public float MinSpeed;

    //유영 범위
    public float RoamBoxMaxX;
    public float RoamBoxMinX;
    public float RoamBoxMaxY;
    public float RoamBoxMinY;
    
    //Spot범위 
    public float SpotRangeBig;
    public float SpotRangeSmall;

    public float RoamWaitTime;

    //감지
    public float detectArea;
    public float detectTime;

    //도망
    public float awaytime;
    public float awaySpeed;

    //공격당함
    public float sturntime;

    public Vector2 mouthPosAdder;



    //public int turnPercent;




    public virtual void Awake()
    {
        fishfin = GetComponent<FishFin>();
        FishHP = GetComponent<FishHealth>();
        animator = GetComponent<Animator>();
        fishcollider = GetComponent<CapsuleCollider2D>();

        if (spawner == null)
        {    
            spawner = GameObject.Find("spawner").GetComponent<FishSpawn>();
        }
        roam = new FSRoam();
        grab = new FSGrab();
        dead = new FSDEAD();
        sturn = new FSSTURN();

        FishHP.startingHealth = startHP;
    }

    // Start is called before the first frame update
    public virtual void Start() {

        //DefaultState();
    }


    public virtual void OnEnable()
    {
        DefaultState();
        
    }


    // Update is called once per frame
    public virtual void Update()
    {
        currentState.stateUpdate();
    }

    public void SetState(FishState nextState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = nextState;

        currentState.OnEnter(this,this.fishfin);

    }

    public virtual void DefaultState()
    {

    }

    public void OnDeath()
    {
        //이전 상태와 상관없이 공격을 받음 && 체력 0 이면 작살에 끌려가기
        SetState(grab);
        /*
        if (ReferenceEquals(currentState, dead))
        {
            SetState();
        }
        else
        {
            SetState(dead);
        }
        */

    }
    public void OnCaught(Inventory Inven)
    {
        Inven.AcquireItem(item);
        spawner.fishOnCaught(gameObject);
        gameObject.SetActive(false);
    }

    public void respawn(Vector2 respawnPos)
    {
        fishfin.SetPosition(respawnPos);
        gameObject.SetActive(true);
    }

    //Debug 임시 콜라이더
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("fish : touch something");
        if (collision.gameObject.tag == "Player" && ReferenceEquals(currentState, grab))
        {
           
      
            Inventory playerInventory = collision.gameObject.GetComponent<Inventory>();
            Debug.Log("playerInventory : " + playerInventory);
            if (!playerInventory.isinventoryfull) {
                OnCaught(playerInventory);
            }else{
                //인벤토리 가득 찼을 시 떨어져 나감
                fishfin.accelFin(4*(new Vector2(Random.Range(-2,2),2)));
                SetState(dead);
            }
            
            //Debug.Log("fish : Player touch");

            //AttTarget = collision.gameObject.GetComponent<IDamageable>();
            //임시 변수
            //AttTarget.OnDamage(3, gameObject, Vector2.zero, Vector2.zero);
        }
    }
    
}
