using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject gig;
    public float bulletSpeed;
    public Transform Fpos;
    public Rigidbody2D gigrb;
    public Transform gigtr;
    public Gig gigScript;
    private Camera _camera;
    private GameObject gunob;
    private Transform fire_point_tr;
    private PlayerController playerInput;
    private PlayerMove playermove;

    private SpriteRenderer Gunsprite;
    private SpriteRenderer Gigsprite;

    private Vector3 default_gun;
    public Vector3 left_guns;
    //public Vector3 left_gun_gap = new Vector3(0.9f, 1.0f, 0);

    public Vector3 default_gig;
    public Vector3 left_gig;

    public Vector3 default_fire_point;
    public Vector3 left_fire_point;


    public enum fireState { ready, fire , hit, rollback,  notready };
    public fireState State;
    public float reloadTime;
    public float fireTime;
    public float hitTime;

    public float gigDamage;
    //damage upgrade 시 늘어나는 damage값
    public float gigDamageUpGap;

    //range upgrade시 늘어나는 범위/속도 비율 
    public float gigRangeUpGap;
    public float SpeedPercent = 1.1f;

    public float StateTimer;
    public float timer;

    //public bool GunIsLeft;
    public Vector2 dirVec;
    private float Timer
    {
        get { return timer; }
        set
        {
            timer = value;
            if (timer < 0)
            {
                //Timer = fireTime;
                switch (State)
                {
                    case fireState.fire:
                    case fireState.hit:
                        Timer = fireTime*2;
                        State = fireState.rollback;
                        break;
                    case fireState.rollback:
                        State = fireState.ready;
                        break;
                    case fireState.ready:
                        break;
                    default:
                        Debug.Log("Timer set error"+State);
                        break;
                }
            }
            
        }
    }
    


    // Start is called before the first frame update
    void Start()
    {
        

        _camera = Camera.main;
        gig = transform.Find("Gig").gameObject;
        // Debug.Log(gig);
        State = fireState.ready;
        gigScript = gig.GetComponent<Gig>();
        gigtr = gig.GetComponent<Transform>();
        gigrb = gig.GetComponent<Rigidbody2D>();
        gunob = transform.Find("Gun").gameObject;
        Gunsprite = gunob.GetComponent<SpriteRenderer>();
        Gigsprite = gig.GetComponent<SpriteRenderer>();
        fire_point_tr = transform.Find("firePoint").gameObject.transform;
        playerInput = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
        playermove = gameObject.transform.parent.gameObject.GetComponent<PlayerMove>();

        default_gun = new Vector3(-0.65f, 0.4f, 0);
        left_guns = new Vector3(0.3f, 1.4f, 0);
        //public Vector3 left_gun_gap = new Vector3(0.9f, 1.0f, 0);

        default_gig = new Vector3(-0.8f, 2.95f, 0);
        left_gig = new Vector3(0.4f, 4.0f, 0);

        default_fire_point = new Vector3(-0.59f, 1.51f, 0);
        left_fire_point = new Vector3(0.2f, 2.3f, 0);

        //Debug.Log("left gun" + left_guns);
        //Debug.Log("default gun " + default_gun);
        //Debug.Log("left gig" + left_gig);
        //Debug.Log("default gig " + default_gig);

        //shopManager까지 연결후 활성화
        GameManager.Instance.shopManager.DamageUpgrade += DamageUP;
        GameManager.Instance.shopManager.RangeUpgrade += RangeUP;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(State != fireState.ready)
        {
            if (Timer < 0)
            {
                Timer = 0;
            }
            return;
        }
        */

        //발사중이 아닐시 총이 마우스를 따라 각도가 조정됨
        if(State == fireState.ready)
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //마우스 좌표 카메라 좌표로 변환
            dirVec = mousePos - (Vector2)transform.position; //마우스 방향 구함
            if (dirVec.x < 0)
            {
                //GunIsLeft = true;
                Gunsprite.flipY = true;
                Gigsprite.flipY = true;
                gunob.GetComponent<Transform>().localPosition = left_guns; //default_gun +left_gun_gap;
                gigtr.localPosition = left_gig;
                fire_point_tr.localPosition = left_fire_point;
                //Debug.Log("left gun" + left_guns);
                //Debug.Log("default gun " + default_gun);
            }
            else
            {
                //GunIsLeft = false;
                Gunsprite.flipY = false;
                Gigsprite.flipY = false;

                gunob.GetComponent<Transform>().localPosition = default_gun;
                fire_point_tr.localPosition = default_fire_point;
                gigtr.localPosition = default_gig;
            }
            transform.up = dirVec.normalized; // 방향벡터를 정규화한 다음 transform.up 벡터에 계속 대입

        }

        /*
         *작동 안함
        if (State == fireState.fire && Input.GetButton("Fire1"))
        {
            State = fireState.rollback;
            Debug.Log("rollback");
        }
        */

        //쏠때
        if (playerInput.fire )//&& !playerInput.ConSturn  )
        {
            if(State == fireState.ready)
            {
                State = fireState.fire;
                //Debug.Log("fire");
                StartCoroutine("Fire");
            }else if (State == fireState.fire)
            {
                Hit();
                //Debug.Log("rollback");
            }else
            {
                //Debug.Log("exception ");
                //Debug.Log(State);
            }
             
        }
        

    }

    public void ready()
    {
        State = fireState.ready;
    }

    public void Hit()
    {
        if (State != fireState.hit)
        {
            State = fireState.hit;
            Timer = hitTime;
        }
    }


    IEnumerator Fire()
    {

        gigrb.isKinematic = false;
        //float StopTime = 0;
        gigScript.onfire();
        playermove.SetSturn(true);
        

        float FireTimer=0;
        Timer = fireTime;
        Vector2 DIR = gigtr.position - transform.position;
        bool end_flag = false;
        while (!end_flag)
        {
            Timer -= Time.deltaTime;
            DIR = gigtr.position - transform.position;
            //FireTimer -= Time.deltaTime;
            //Debug.Log("corrutine while ");
            
            switch (State)
            {
                case fireState.fire:
                    gigtr.Translate(transform.up * bulletSpeed * Time.deltaTime, Space.World);
                    gigtr.right = DIR.normalized;
                    //FireTimer += Time.deltaTime;
                    //Debug.Log("switch fire");
                    //Timer -= Time.deltaTime;
                    //yield return null;
                    break;
                case fireState.hit:
                    //Timer = hitTime;
                    //State = fireState.rollback;
                    
                    break;
                case fireState.rollback:
                    //State = fireState.ready;
                    //FireTimer -= Time.deltaTime;
                    
                    //Vector2 DIR =  gigtr.position - transform.position;
                    gigtr.right = DIR.normalized;
                    //transform.localRotation = Quaternion.Euler(0, 0, 90);
                    //gigtr.Translate(-1 * transform.up * bulletSpeed * Time.deltaTime, Space.World);
                    gigtr.Translate(-1 * DIR.normalized * bulletSpeed * Time.deltaTime, Space.World);

                    if (DIR.magnitude < 0.5f)
                    {
                        Debug.Log("magnitude");
                        Timer = -1;
                        end_flag = true;
                    }
                    break;
                case fireState.ready:
                    //FireTimer = -1;
                    end_flag = true;
                    break;
                default:
                    Debug.Log("corutine Timer set error" + State);
                    break;
            }
            
            yield return null;

        }
        State = fireState.ready;
        gigrb.isKinematic = true;
        gigScript.outfire();
        playermove.SetSturn(false);   


    }


    public void DamageUP()
    {
        gigDamage += gigDamageUpGap;
        gigScript.updateDamage();

    }

    public void RangeUP()
    {
        // 0.4
        bulletSpeed *= SpeedPercent;
        fireTime *= gigRangeUpGap / SpeedPercent;
    }

}
