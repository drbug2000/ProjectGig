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

    //�⺻ ����
    public FSRoam roam;
    public FSDEAD dead;
    public FSSTURN sturn;
    //FScatched;

    //HP ���� ����
    public int startHP;

    //������ ���� ����
    // public float startWaitTime;
    //public float waitTime;
    //public Transform moveSpot;

    //public SpriteRenderer Renderer;
    //public Rigidbody2D fishRigidbody;

    //����� Ư�� ���� ����
    public float DefaultSize;//����� �⺻ ũ��(����� ���� ������)
    public float RatioSize=1;//�������� ũ�� (DefaultSize * RatioSize = ���� ũ��)

    public float mass;//����
    public float drag;//����
    public float gravity;//�޴� �߷�
    public float speed;

    public float MaxSpeed;
    public float MinSpeed;

    //���� ����
    public float RoamBoxMaxX;
    public float RoamBoxMinX;
    public float RoamBoxMaxY;
    public float RoamBoxMinY;
    
    //Spot���� 
    public float SpotRangeBig;
    public float SpotRangeSmall;

    public float RoamWaitTime;

    //����
    public float detectArea;
    public float detectTime;

    //����
    public float awaytime;
    public float awaySpeed;

    //���ݴ���
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
