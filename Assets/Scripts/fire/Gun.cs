using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject gig;
    public float bulletSpeed;
    public Transform Fpos;
    public Rigidbody2D gigrb;
    public Gig gigScript;
    private Camera _camera;

    public enum fireState { fire , ready, notready };
    public fireState State;
    public float reloadTime;
    public float fireTime;
    public float StateTimer;


    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        gig = transform.Find("Gig").gameObject;
        Debug.Log(gig);
        State = fireState.ready;
        gigScript = gig.GetComponent<Gig>();
    }

    // Update is called once per frame
    void Update()
    {
        if(State != fireState.ready)
        {
            StateTimer -= Time.deltaTime;
        }

        //발사중이 아닐시 총이 마우스를 짜라 각도가 조정됨
        if(State != fireState.fire)
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //마우스 좌표 카메라 좌표로 변환
            Vector2 dirVec = mousePos - (Vector2)transform.position; //마우스 방향 구함
            transform.up = dirVec.normalized; // 방향벡터를 정규화한 다음 transform.up 벡터에 계속 대입

        }

        //쏠때

        if (Input.GetButtonDown("Fire1"))
        {
            State = fireState.fire;
            Debug.Log("fire");
            //GameObject tool = Instantiate(toolPrefab, Fpos.position, gameObject.transform.rotation);
            
            Rigidbody2D rb = gig.GetComponent<Rigidbody2D>();
            //rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
            
            gigScript.fire(fireTime, bulletSpeed);
        }


    }

    public void ready()
    {
        State = fireState.ready;
    }
}
