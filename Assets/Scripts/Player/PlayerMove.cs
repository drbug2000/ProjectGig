using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // isfire 변수 값을 받아오기 위한 변수
    [SerializeField]
    private Gig gig;
    // public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public float jumpForce = 700f; // 점프 힘
    public float swimForce = 3f; // 헤엄 힘
    public int DashForce;//대쉬 힘
    public float Speed = 5f;
    protected int jumpCount = 0; // 누적 점프 횟수
   
    protected bool isGrounded = false; // 바닥에 닿았는지 나타냄 점프할 때 쓰는 변수
    private bool isDead = false; // 사망 상태
    
    private PlayerHealth playerHealth;

    protected Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    // private SpriteRenderer playerSpriteRenderer;
    private Animator animator; // 사용할 애니메이터 컴포넌트
    // private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트
    private SpriteRenderer Renderer;
    protected bool CurrentSpeed = false; // 이건 뭔지 모르겠습니다...?

    public PlayerController playerInput;
    // private Animator playerAnimator;

    public GameObject inventoryparents;
    public GameObject Gun;
    public Gun Gunscript;

    public float DashCoolTime;
    public float DashMoveCool;
    public float DashTimer;
    public Image barImage;
    public particleController particlecontroller;

    public bool Sturn = false;
    // 저장된 위치로 옮기기 위한 변수입니다.
    private Vector3 playerpos;

    private Color green_color = new Color(0.15f, 0.77f, 0.33f, 1f);
    private Color yellow_color = new Color(0.7f, 0.7f, 0.15f, 1f);


    private bool ONBOARD=true;
    private WaitForSeconds float_time;
    public bool onboard // 갑판 위에 있는지
    {
        get { return ONBOARD; }
        set
        {
            if(ONBOARD ^ value)
            {
                ONBOARD = value;
                if (value)
                {
                    playerRigidbody.gravityScale = 1; // 배 위에 있을 때 중력 1
                    playerRigidbody.drag = 1;
                    particlecontroller.EndMainEffect();
                    animator.SetBool("intoOcean", false);
                    ChangeDashBarColor(green_color);
                }
                else
                {
                    playerRigidbody.gravityScale = 0; // 물 속에 있을 때 중력 0
                    playerRigidbody.drag = 1.0f;
                    //particlecontroller.AddEffect(100f, new(0, 0));
                    particlecontroller.StartMainEffect();
                    animator.SetBool("intoOcean", true);
                }
            }
        }

    }
    private bool ISLEFT = true;
    public bool isleft
    {
        get { return ISLEFT; }
        set
        {
            if (ISLEFT ^ value)
            {
                ISLEFT = value;
                //IsTurn = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        if (DatabaseManager.Instance.path != null) {
            playerpos = DatabaseManager.Instance.toplayerpos;
        }
        */
        if (barImage == null) {
            Debug.Log("Dashbar is NULL");
                }
        else
        {
            Debug.Log("clor" + barImage.color);
        }
        // 초기화
        playerInput = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        Renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // playerAudio = GetComponent<AudioSource>();
        // playerSpriteRenderer = GetComponent<SpriteRenderer>();
        onboard = true;
        Gun = transform.Find("gun").gameObject;
        Gunscript = Gun.GetComponent <Gun> ();
        particlecontroller = GetComponent<particleController>();
        //inventoryparents.SetActive(false);
        float_time = new WaitForSeconds(0.1f);

        ChangeDashBarColor(green_color);

    }

    // Update is called once per frame
    void Update()
    {
        // isDead = playerHealth.dead;
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead || Sturn)
        {
            return;
        }

        if (DashTimer >= 0)
        {
            DashTimer -= Time.deltaTime;
            ChangeDashBarAmount(DashTimer / DashCoolTime);
        }

        if (onboard == true)
        {
            //주석부분은 onboard 변수에 get/set으로 구현됨
            //playerRigidbody.gravityScale = 1; // 배 위에 있을 때 중력 1
            //playerRigidbody.drag = 1;
    
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
                //Debug.Log("0");
                animator.SetBool("move",false);
            } else{
                animator.SetBool("move",true);
            }

            playerwalk();
            
            
            //animator.SetBool("intoOcean", false);

        }
        else
        {
            //주석부분은 onboard 변수에 get/set으로 구현됨
            //playerRigidbody.gravityScale = 0; // 물 속에 있을 때 중력 0
            //playerRigidbody.drag = 1.0f;

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
                //Debug.Log("0");
                animator.SetBool("move",false);
            } else{
                animator.SetBool("move",true);
            }

            playerswim();

            //animator.SetBool("intoOcean", true);
        }

        //방향전환
        if (IsLeft())
        {
            Renderer.flipX = true;
        }
        else
        {
            //if (!IsStop(velocity.x))
            {
                Renderer.flipX = false;
            }
        }
        
        //Attack();

        
        // animator.SetBool("Grounded",isGrounded);
        // animator.SetBool("CurrentSpeed", CurrentSpeed);
    }
    
    // 플레이어가 땅 위에서 움직이는 것에 관한 함수
    public void playerwalk()
    {
        //walk의 경우 현재 작살의 방향을 기준으로 
        
        isleft = IsLeft(Gunscript.dirVec);

        // 점프에 관한 내용입니다.
        if (playerInput.jump && jumpCount < 2)
        {
            jumpCount++;

            playerRigidbody.velocity = Vector2.zero;

            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            // playerAudio.Play();

        }
        
        else if(playerInput.jump && playerRigidbody.velocity.y > 0 )
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // x축 방향으로 움직일 때 사용되는 내용입니다.
        transform.Translate(Vector3.right * Speed * playerInput.move_x * Time.deltaTime);
        

        // current speed가 필요가 없다면 위의 내용과 아래의 내용은 같습니다.
        // if (playerInput.move_x > 0)
        // {
        //     transform.Translate(Vector3.right * Speed * playerInput.move_x * Time.deltaTime);
        //     // playerSpriteRenderer.flipX = false;
        //     CurrentSpeed = true;
        // }
        // else if (playerInput.move_x < 0)
        // {
        //     transform.Translate(Vector3.right * Speed * playerInput.move_x * Time.deltaTime);
        //     // playerSpriteRenderer.flipX = true;
        //     CurrentSpeed = true;
        // }
        // else if (playerInput.move_x == 0 || Input.GetKeyUp("A") || Input.GetKeyUp("D"))
        // {
        //     transform.Translate(Vector3.zero);
        //     CurrentSpeed = false;
        // }
    }

    // 플레이어가 물 속에 있을 때 움직이는 것에 관한 함수입니다.
    public void playerswim()
    {
        //물속의 경우 현재 운동 방향을 기준으로 sprite 방향 설정
        //isleft = IsLeft(playerRigidbody.velocity.x);

        if (DashTimer < DashMoveCool)
        {
            //move in sea
            if (playerRigidbody.velocity.x > -5 && playerRigidbody.velocity.x < 5) {
                playerRigidbody.AddForce(swimForce * Vector3.right * playerInput.move_x);
            }
            //물속의 경우 현재 입력 방향을 기준으로 sprite 방향 설정
            isleft = IsLeft(playerInput.move_x);

            if (playerRigidbody.velocity.y > -5 && playerRigidbody.velocity.y < 5) {
                playerRigidbody.AddForce(swimForce * Vector3.up * playerInput.move_y);
            }
        }
        else
        {
            ChangeDashBarColor(yellow_color);
        }

        //dash
        if (DashTimer <= 0 && playerInput.jump)
        {
            float x;
            float y;
            //Dash
            if(playerInput.move_x == 0 && playerInput.move_y == 0)
            {
                x = playerRigidbody.velocity.x;
                y = playerRigidbody.velocity.y;
            }
            else
            {
                x = playerInput.move_x;
                y = playerInput.move_y;
            }
            playerRigidbody.AddForce(DashForce*(new Vector2(x,y)));
            DashTimer = DashCoolTime;
        }

        


        
    }

    public bool IsLeft()
    {
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

    public bool IsStop(float velocityM)
    {
        return velocityM == 0;
    }

    public void Attack()
    {
        if (gig.isfire == true)
        {
            playerRigidbody.velocity = new Vector3(0f, 0f, 0);
            // Animator.SetBool("?", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }

    public void Teleport(Vector2 POS)
    {
        gameObject.transform.position = POS;
    }
    

    
    //임시 저장 변수
    float defaultmass;
    float defaultdrag;

    public void GetBitten()
    {
        //Debug.Log("a물렸다");

        SetSturn(true, true);
        SetGravitySturn(true);
        // Debug.Log("default mass : " + defaultmass + "\n default drag : " + defaultdrag);
        // Debug.Log("current mass : " + playerRigidbody.mass + "\n current drag : " + playerRigidbody.drag);
    }

    public void SpitOut(Vector2 spitForce)
    {
        // Debug.Log("뱉었다");
        // Debug.Log(spitForce);
        SetGravitySturn(false);
        SetSturn(false,false);
        playerRigidbody.AddForce(spitForce);
        // Debug.Log("default mass : " + defaultmass + "\n default drag : " + defaultdrag);
        // Debug.Log("current mass : " + playerRigidbody.mass + "\n current drag : " + playerRigidbody.drag);
        Sturn = false;
    }

    public void SetGravitySturn(bool sturn)
    {
        if(sturn){
            if (playerRigidbody.mass > 0.1f)
            {
                defaultmass = playerRigidbody.mass;
            }
            if (playerRigidbody.drag > 0.9f)
            {
                defaultdrag = playerRigidbody.drag;
            }
            playerRigidbody.mass = 0;
            playerRigidbody.drag = 0;

        }else
        {
            playerRigidbody.mass = defaultmass;
            playerRigidbody.drag = defaultdrag;
        }

    }

    public void SetSturn(bool sturn ,bool inputstrun)
    {
        Sturn = sturn;
        playerInput.SetConSturn(inputstrun);
    }

    public void SetSturn(bool sturn)
    {
        Sturn = sturn;
        //playerInput.SetConSturn(sturn);
    }

    public void getSturn(float sturntime)
    {

        StartCoroutine("Sturning", sturntime);

    }

    IEnumerator Sturning(float sturntime)
    {
        Sturn = true;
        //float defaultmass = playerRigidbody.mass;
        //float defaultdrag = playerRigidbody.drag;
        //playerRigidbody.mass=0;
        //playerRigidbody.drag=0;
        for(int i =0; i< sturntime*10; i++)
        {
            yield return float_time;
        }

        //playerRigidbody.mass = defaultmass;
        //playerRigidbody.drag = defaultdrag;
        Sturn = false;
    }
    

    private void ChangeDashBarAmount(float amount) //* HP 게이지 변경 
    {
        if (amount < DashMoveCool/DashCoolTime)
        {
            ChangeDashBarColor(green_color);
        }
        barImage.fillAmount = amount;

        //* HP가 0이거나 꽉차면 HP바 숨기기
        if (barImage.fillAmount == 0f || barImage.fillAmount == 1f)
        {
            //Hide();
            barImage.enabled = false;
        }
        else if(!barImage.enabled)
        {
            barImage.enabled = true;
        }//else if(bar)
    }
    private void ChangeDashBarColor(Color color)
    {
        barImage.color = color;
    }



}