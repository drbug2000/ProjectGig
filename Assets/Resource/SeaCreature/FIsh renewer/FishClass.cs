using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishClass : MonoBehaviour
{


    protected FishFin fishfin;
    protected FishHealth FishHP;
    public FishState currentState { get; private set; }
    public GameObject target;
    public GameObject awaytarget;

    //기본 상태
    public FSRoam roam;
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



    //public int turnPercent;

    


    public virtual void Awake()
    {
        fishfin = GetComponent<FishFin>();
        FishHP = GetComponent<FishHealth>();

        roam = new FSRoam();
        dead = new FSDEAD();
        sturn = new FSSTURN();

        
    }

    // Start is called before the first frame update
    public virtual void Start() {

        DefaultState();
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

    public virtual void OnDeath()
    {
        SetState(dead);
    }
    public virtual void OnCatched()
    {
        gameObject.SetActive(false);
    }
}
