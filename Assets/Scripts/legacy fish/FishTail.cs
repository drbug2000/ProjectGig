using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTail : MonoBehaviour
{


    
    public float orientation;
    public float rotation;
   // public Vector2 dir;

    public Rigidbody2D fishRigidbody;
    public SpriteRenderer Renderer;

    public Tail tail;

    public Vector2 velocity;
    public float MaxSpeed;
    public float MinSpeed;
    public float waitTime;

    public Vector2 currentPos;

    public GameObject SpotPoint;

    //DeBug
    public float velocityM;

    // Start is called before the first frame update
    void Start()
    {
        tail = new Tail();

        Renderer = GetComponent<SpriteRenderer>();
        fishRigidbody = GetComponent<Rigidbody2D>();
    }

    

    // Update is called once per frame
    void Update()
    {
        //Spot�� �����Ǿ� ������
        //if ((tail.Spot - Vector2.zero).magnitude!=0)

        // fishRigidbody.AddForce(tail.Speed * tail.Dir);
        SpotPoint.transform.position = tail.Spot;
        //Debug.Log(tail.Spot);
        if (velocity.magnitude <= MinSpeed)
        {
            tail.ReDir(currentPos);
            SetDrag(0.25f);
            fishRigidbody.AddForce(MaxSpeed * tail.Dir);

            if (tail.IsRight())
            {
                Renderer.flipX = false;
            }
            else
            {
                Renderer.flipX = true;
            }
        }
        

    }
    public virtual void LateUpdate()
    {

        currentPos = new Vector2(transform.position.x, transform.position.y);

        //wait time ���� �� ���� ���
        /*
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        */
        //�ִ�ӷ� �Ѱ� ����
        velocity = fishRigidbody.velocity;
        velocityM = velocity.magnitude;

        if (velocity.magnitude > MaxSpeed)
        {
            fishRigidbody.velocity = velocity.normalized * MaxSpeed; 
        }
        //�ٴ� ǥ������ �ö��� �ʰ�
        if (currentPos.y >= 0)
        {
            Debug.Log("fish out");
            StopFish();

        }


    }

    public float SpotDistance()
    {

        return (currentPos - tail.Spot).magnitude;

    }

    /*
    public void ReDir(Tail tail)
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = tail.Spot - position;
    }
    */
    public void SetTail(Tail T)
    {
        this.tail = T;
        StopFish();
    }

    public void StopFish() 
    {
        fishRigidbody.velocity = Vector2.zero;
    }

    public void SetDrag(float drag)
    {
        fishRigidbody.drag = drag;

    }

}
