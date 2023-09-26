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

    //�̵� ����
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

    //fishfin script�� �ִϸ��̼� �ӵ� ������
    //true�Ͻ� ����� �ִϸ��̼� �ӵ��� �̵��ӵ��� ����
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
            {//���ӿ� �ٽ� ������ ��
                //�߷� ���
                //Drag ���
                fishRigidbody.gravityScale = fish.gravity;
                SetDrag(fish.drag);
            }
            else
            {//�������� ��������
                fishRigidbody.gravityScale = fish.gravity*10;
                SetDrag(fish.drag*2);
                //fish.DefaultState();
                //÷���Ÿ� effect
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
        //���� ����ȭ
        currentPos = new Vector2(transform.position.x, transform.position.y);
        SpotDistance = (currentPos - Spot).magnitude;

        velocity = fishRigidbody.velocity;
        
        velocityM = velocity.magnitude;
        isleft = IsLeft(velocity.x);

        if (sturn) { return; }

        //������ȯ
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

        //�ִ�ӷ� �Ѱ� ����
        if (velocity.magnitude > fish.MaxSpeed)
        {
            fishRigidbody.velocity = velocity.normalized * fish.MaxSpeed;
        }

        //�ٴ� ǥ������ �ö��� �ʰ�
        //Collider�� �����ϸ� ���� �� ������ ������
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

    //���� ��ǥ�� ����ϴ� �Լ�
    //way parameter = -1 ������ ���� �̵� ����� �ݴ�� ���
    public Vector2 WhereMouth(int way = 1)
    {
        //�ݶ��̴� ����� ���� ����� ��� ��ġ ����
        Vector2 mouth = fish.fishcollider.size;
        mouth.x *= -0.5f;//���� �Ʒ��� default
        mouth.y *= -0.5f;
        //������ ����
        mouth += fish.mouthPosAdder + fish.fishcollider.offset;
        //��� ���⿡ ���� x��ǥ ���� ����
        if (!isleft) { mouth.x *= -1; Debug.Log("�ִ��� ����"); }
        else { Debug.Log("�ִ��� ��"); }
        Debug.Log("���� ���� �ӵ� ����" + velocity.x);

        //if (way == -1) { mouth.x *= -1; }
        return currentPos + mouth;

    }
}
