using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFin  : MonoBehaviour
{

    

    public float orientation;
    public float rotation;
    // public Vector2 dir;

    public FishClass fish;
    public Rigidbody2D fishRigidbody;
    public SpriteRenderer Renderer;

    //�̵� ����
    public Vector2 Dir;
    public Vector2 Spot;

    public Vector2 currentPos;
    public Vector2 velocity;
    //DeBug
    public float velocityM;

    public float SpotDistance;
    public Vector2 SpotDir;


    //public float MaxSpeed;
    public float Speed;
    //public float MinSpeed;
    //public float waitTime;

    public GameObject SpotPoint;


    // Start is called before the first frame update
    void Start()
    { 
        Renderer = GetComponent<SpriteRenderer>();
        fishRigidbody = GetComponent<Rigidbody2D>();
        fish = GetComponent<FishClass>();
    }


    private void OnEnable()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        //Spot�� �����Ǿ� ������
        //if ((tail.Spot - Vector2.zero).magnitude!=0)

        // fishRigidbody.AddForce(tail.Speed * tail.Dir);
        SpotPoint.transform.position = Spot;
        //Debug.Log(tail.Spot);

        //������ȯ
        if (velocity.x>0)
        {
            Renderer.flipX = true;
        }
        else
        {
            Renderer.flipX = false;
        }



    }
    public virtual void LateUpdate()
    {
        //���� ����ȭ
        currentPos = new Vector2(transform.position.x, transform.position.y);
        SpotDistance = (currentPos - Spot).magnitude;
        velocity = fishRigidbody.velocity;
        velocityM = velocity.magnitude;
        //wait time ���� �� ���� ���
        /*
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        */
        //�ִ�ӷ� �Ѱ� ����
        if (velocity.magnitude > fish.MaxSpeed)
        {
            fishRigidbody.velocity = velocity.normalized * fish.MaxSpeed;
        }
        //�ٴ� ǥ������ �ö��� �ʰ�
        if (currentPos.y >= 0)
        {
            Debug.Log("fish out");
            StopFish();

        }


    }

    public void accelFin(float acc=1.0f)
    {
        fishRigidbody.AddForce(acc * Speed * Dir.normalized);
        //Debug.Log(acc + Speed +"accele active");
    }
    public void accelFin(Vector2 DIR, float acc=1.0f )
    {
        this.Dir = DIR;
        accelFin(acc);

    }

    public void SetSpot(Vector2 nextSpot)
    {

        Spot = nextSpot;

    }

    public void SpotMove(float acc = 1.0f)
    {
        ReDirSpot();
        accelFin(SpotDir,acc);
    } 
    public void SpotMove(Vector2 SPOT,float acc = 1.0f)
    {
        SetSpot(SPOT);
        SpotMove(acc);
    }
    
    public void ReDirSpot()
    {
        SpotDir = Spot - currentPos;
    }
    
    public void SpeedReset()
    {
        
    }
    

    public void StopFish()
    {
        fishRigidbody.velocity = Vector2.zero;
    }

    public void SetDrag(float drag)
    {
        fishRigidbody.drag = drag;

    }

    public Vector2 TransVector(Vector3 V)
    {
        return new Vector2(V.x, V.y);
    }
}
