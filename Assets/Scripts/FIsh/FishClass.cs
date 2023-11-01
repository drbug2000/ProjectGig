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


    //�⺻ ����
    public FSRoam roam;
    public FSGrab grab;
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

    //������ Ư�� ���� ����
    public float DefaultSize;//������ �⺻ ũ��(������ ���� ������)
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
        //���� ���¿� ������� ������ ���� && ü�� 0 �̸� �ۻ쿡 ��������
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

    //Debug �ӽ� �ݶ��̴�
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("fish : touch something");
        if (collision.gameObject.tag == "Player" && ReferenceEquals(currentState, grab))
        {
           
      
            Inventory playerInventory = collision.gameObject.GetComponent<Inventory>();
            if (!playerInventory.isinventoryfull) {
                OnCaught(playerInventory);
            }else{
                //�κ��丮 ���� á�� �� ������ ����
                fishfin.accelFin(4*(new Vector2(Random.Range(-2,2),2)));
                SetState(dead);
            }
            
            //Debug.Log("fish : Player touch");

            //AttTarget = collision.gameObject.GetComponent<IDamageable>();
            //�ӽ� ����
            //AttTarget.OnDamage(3, gameObject, Vector2.zero, Vector2.zero);
        }
    }
    
}
