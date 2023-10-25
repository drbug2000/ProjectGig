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

    public enum fireState { ready, fire , hit, rollback,  notready };
    public fireState State;
    public float reloadTime;
    public float fireTime;
    public float hitTime;

    public float gigDamage;

    
    public float StateTimer;
    public float timer;
    
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
                        Timer = fireTime;
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
        //Debug.Log(gig);
        State = fireState.ready;
        gigScript = gig.GetComponent<Gig>();
        gigtr = gig.GetComponent<Transform>();
        gigrb = gig.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(State != fireState.ready)
        {
            if (Timer < 0)
            {
                Timer = 0;
            }
            return;
        }

        //�߻����� �ƴҽ� ���� ���콺�� ���� ������ ������
        if(State != fireState.fire)
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //���콺 ��ǥ ī�޶� ��ǥ�� ��ȯ
            Vector2 dirVec = mousePos - (Vector2)transform.position; //���콺 ���� ����
            transform.up = dirVec.normalized; // ���⺤�͸� ����ȭ�� ���� transform.up ���Ϳ� ��� ����

        }

        //��
        if (Input.GetButtonDown("Fire1") )
        {
            State = fireState.fire;
            //Debug.Log("fire");
            
            StartCoroutine("Fire");
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

        /*
        Debug.Log("fire corutine start");

        //Debug.Log("transform.up"+ transform.up);
        //Debug.Log("gigrt.up" + gigtr.up);
        
        Timer = fireTime;
        while (State == fireState.fire)
        {
            gigtr.Translate(transform.up * bulletSpeed*Time.deltaTime,Space.World);
            Timer -= Time.deltaTime;
            yield return null;
        }

        //���߽�
        //Debug.Log("fire corutine on Hit");
        if(State == fireState.hit)
        {
            Debug.Log("fire corutine on Hit");
            StopTime = Timer;
            Timer = hitTime;
        }
        while (State == fireState.hit) {
            //��� ����
            Timer -= Time.deltaTime;
            yield return null;
        }

        //rollback
        Debug.Log("fire corutine rollback");
        Timer = fireTime - StopTime;
        while (State == fireState.rollback)
        {
            gigtr.Translate(-1*transform.up * bulletSpeed * Time.deltaTime, Space.World);
            Timer -= Time.deltaTime;
            yield return null;
        }

        //rollback ���� HIT �� ��Ȳ
        while (State == fireState.hit)
        {
            //��� ����
            Timer -= Time.deltaTime;
            yield return null;
        }

        */
        


        /*New Code*/

        float FireTimer=0;
        Timer = fireTime;
        

        while (FireTimer >= 0)
        {
            Timer -= Time.deltaTime;
            //FireTimer -= Time.deltaTime;
            //Debug.Log("corrutine while ");
            switch (State)
            {
                case fireState.fire:
                    gigtr.Translate(transform.up * bulletSpeed * Time.deltaTime, Space.World);
                    FireTimer += Time.deltaTime;
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
                    FireTimer -= Time.deltaTime;
                    gigtr.Translate(-1 * transform.up * bulletSpeed * Time.deltaTime, Space.World);
                    break;
                case fireState.ready:
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


    }


}
