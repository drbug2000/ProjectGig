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

        //�߻����� �ƴҽ� ���� ���콺�� ¥�� ������ ������
        if(State != fireState.fire)
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //���콺 ��ǥ ī�޶� ��ǥ�� ��ȯ
            Vector2 dirVec = mousePos - (Vector2)transform.position; //���콺 ���� ����
            transform.up = dirVec.normalized; // ���⺤�͸� ����ȭ�� ���� transform.up ���Ϳ� ��� ����

        }

        //��

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
