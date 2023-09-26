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
    private Animator fishAimator;

    //이동 정보
    public Vector2 Dir;
    public Vector2 Spot;

    public Vector2 currentPos;
    public Vector2 velocity;


    //DeBug
    public float velocityM;
    //public GameObject SpotPoint;


    public float SpotDistance;
    public Vector2 SpotDir;

    //public float MaxSpeed;
    public float Speed;
    //public float MinSpeed;
    //public float waitTime;

    //fishfin script의 애니메이션 속도 통제권
    //true일시 물고기 애니메이션 속도가 이동속도와 같음
    private bool aniControl = true;

    bool sturn=false;
    private bool inWater;
    public bool UnderTheSea
    {
        get { return inWater; }
        set
        {
            if (inWater == value)
            {
                return;
            }

            if (value)
            {//물속에 다시 들어왔을 때
                //중력 상승
                //Drag 상승
                fishRigidbody.gravityScale = fish.gravity;
                SetDrag(fish.drag);
            }
            else
            {//물밖으로 나갔을때
                fishRigidbody.gravityScale = fish.gravity*10;
                SetDrag(fish.drag*2);
                //첨벙거림 effect
            }
            inWater = value;
        }
    }
    


    // Start is called before the first frame update
    void Awake()
    { 
        Renderer = GetComponent<SpriteRenderer>();
        fishRigidbody = GetComponent<Rigidbody2D>();
        fish = GetComponent<FishClass>();
        fishAimator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        sturn = false;
        if (currentPos.y >= 0)
        {
            //Debug.Log("fish out");
            this.UnderTheSea = false;
            //StopFish();

        }
        else
        {
            this.UnderTheSea = false;
        }

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

        if (aniControl){
            fishAimator.SetFloat("fishSpeed", velocityM * 0.5f + 0.5f);
        }
        if (sturn){return;}

        //최대속력 한계 설정
        if (velocity.magnitude > fish.MaxSpeed)
        {
            fishRigidbody.velocity = velocity.normalized * fish.MaxSpeed;
        }

        //바다 표면위로 올라가지 않게
        //Collider로 구현하면 조금 더 좋을꺼 같긴함
        if (currentPos.y >= 0)
        {
            //Debug.Log("fish out");
            this.UnderTheSea = false;
            //StopFish();

        }
        else
        {
            this.UnderTheSea = true;
        }

    }

    public void accelFin(float acc=1.0f)
    {
        if (!sturn && UnderTheSea)
        {
            fishRigidbody.AddForce(acc * Speed * Dir.normalized);
        }
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
        SpotDir = Spot-currentPos  ;
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

    public void SetAniControl(bool flag)
    {
        aniControl = flag;
    }

    public Vector2 TransVector(Vector3 V)
    {
        return new Vector2(V.x, V.y);
    }
}
