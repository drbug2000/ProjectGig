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
                //fish.DefaultState();
                //첨벙거림 effect
            }
            inWater = value;
        }
    }
    public bool IsTurn;
    private bool ISLEFT = true;
    public bool isleft
    {
        get { return ISLEFT; }
        set { if (ISLEFT ^ value)
            {
                ISLEFT = value;
                IsTurn = true;
            } 
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
        if (currentPos.y>=0)
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
        //변수 동기화
        currentPos = new Vector2(transform.position.x, transform.position.y);
        SpotDistance = (currentPos - Spot).magnitude;

        velocity = fishRigidbody.velocity;
        
        velocityM = velocity.magnitude;
        isleft = IsLeft(velocity.x);

        if (sturn) { return; }

        //방향전환
        if (IsLeft())
        {
            Renderer.flipX = false;
        }
        else
        {
            Renderer.flipX = true;
        }



    }
    public virtual void LateUpdate()
    {
        

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

    public void SetVelocity(Vector2 velo)
    {
        if (!sturn && UnderTheSea)
        {
            fishRigidbody.velocity = velo;
        }
    }
    

    public void StopFish()
    {
        if (!sturn && UnderTheSea)
        {
            fishRigidbody.velocity = Vector2.zero;
        }
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
    
    public bool IsLeft()
    {
        /*
        if (fishRigidbody.velocity.x < 0)
        {
            return true;
        }else
        {
            return false;
        }
        */
        return isleft;
    }
    
    
    public bool IsLeft(float velocityX)
    {
        return velocityX < 0;
    }
    
    public bool IsLeft(Vector2 velocity)
    {
        
        return velocity.x < 0;
    }

    //입의 좌표를 출력하는 함수
    //way parameter = -1 설정시 현재 이동 방향과 반대로 출력
    public Vector2 WhereMouth(int way = 1)
    {
        //콜라이더 사이즈에 따른 상어입 상대 위치 조정
        Vector2 mouth = fish.fishcollider.size;
        mouth.x *= -0.5f;//왼쪽 아래가 default
        mouth.y *= -0.5f;
        //보정값 적용
        mouth += fish.mouthPosAdder + fish.fishcollider.offset;
        //상어 방향에 따른 x좌표 방향 조정
        if (!isleft) { mouth.x *= -1; Debug.Log("주댕이 오른"); }
        else { Debug.Log("주댕이 왼"); }
        Debug.Log("현재 측정 속도 방향" + velocity.x);

        //if (way == -1) { mouth.x *= -1; }
        return currentPos + mouth;

    }
}
