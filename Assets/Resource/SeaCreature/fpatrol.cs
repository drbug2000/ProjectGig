using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fpatrol : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    private float waitTime;
    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private SpriteRenderer Renderer;
    private Rigidbody2D fishRigidbody;

    Vector2 dir;


    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        fishRigidbody = GetComponent<Rigidbody2D>();
        setNewSpot();
        speed = 10;
        
    }
    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed *Time.deltaTime);
        //MoveSpot�� �����ϸ� waitTime��ŭ ���
        if (Vector2.Distance(transform.position, moveSpot.position) < 2)
        {
            if (waitTime <= 0)
            {
                setNewSpot();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        //�ӵ��� 1 ���Ϸ� �������� ���� �缳�� ���� �ٽ� ����
        if(fishRigidbody.velocity.magnitude < 1)
        {
            dir = moveSpot.position - transform.position;
            fishRigidbody.AddForce(dir * (speed/dir.magnitude));

            if (moveSpot.position.x > transform.position.x)
            {
                Renderer.flipX = false;
            }
            else
            {
                Renderer.flipX = true;
            }
        }
    }
    public void setNewSpot()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Debug.Log(moveSpot.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dir = transform.position - other.transform.position; 
            fishRigidbody.AddForce(dir * (speed*10) / dir.magnitude);
            Debug.Log("PP");
            /*
        ContactPoint2D contact = other.contacts[0];
        Vector2 normal = -1 * contact.normal;
        fishRigidbody.AddForce(normal * (speed*3 / dir.magnitude));
            */
        }
    }



}

