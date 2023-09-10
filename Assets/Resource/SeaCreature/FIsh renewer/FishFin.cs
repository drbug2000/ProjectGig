using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFin  : MonoBehaviour
{

    public float orientation;
    public float rotation;
    // public Vector2 dir;

    private FishClass fish;
    private Rigidbody2D fishRigidbody;
    private SpriteRenderer Renderer;

    //이동 정보
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

    bool sturn=false;

    //public GameObject SpotPoint;


    // Start is called before the first frame update
    void Awake()
    { 
        Renderer = GetComponent<SpriteRenderer>();
        fishRigidbody = GetComponent<Rigidbody2D>();
        fish = GetComponent<FishClass>();
    }


    private void OnEnable()
    {
        sturn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (sturn) { return; }

        //방향전환
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
        //변수 동기화
        currentPos = new Vector2(transform.position.x, transform.position.y);
        SpotDistance = (currentPos - Spot).magnitude;
        velocity = fishRigidbody.velocity;
        velocityM = velocity.magnitude;
        

        if (sturn){return;}

        //최대속력 한계 설정
        if (velocity.magnitude > fish.MaxSpeed)
        {
            fishRigidbody.velocity = velocity.normalized * fish.MaxSpeed;
        }
        //바다 표면위로 올라가지 않게
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

    public void SpotMoveBack(float acc = 1.0f)
    {
        ReDirSpot();
        accelFin(-1*SpotDir, acc);
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
    public void SetSturn(bool Sturn)
    {
        this.sturn = Sturn;
    }

    public void SetPosition(Vector3 targetPosition)
    {
        gameObject.transform.position = targetPosition;
    }

    public Vector2 TransVector(Vector3 V)
    {
        return new Vector2(V.x, V.y);
    }
}
