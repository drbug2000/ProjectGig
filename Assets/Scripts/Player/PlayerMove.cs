using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public float jumpForce = 700f; // 점프 힘
    public float swimForce = 3f; // 헤엄 힘
    public int DashForce;//대쉬 힘
    public float Speed = 5f;
    protected int jumpCount = 0; // 누적 점프 횟수
   
    protected bool isGrounded = false; // 바닥에 닿았는지 나타냄 점프할 때 쓰는 변수
    private bool isDead = false; // 사망 상태
    public bool onboard; // 갑판 위에 있는지
    private PlayerHealth playerHealth;

    protected Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    // private SpriteRenderer playerSpriteRenderer;
    private Animator animator; // 사용할 애니메이터 컴포넌트
    // private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    protected bool CurrentSpeed = false; // 이건 뭔지 모르겠습니다...?

    public PlayerController playerInput;
    // private Animator playerAnimator;

    public GameObject inventoryparents;


    public float DashCoolTime;
    public float DashMoveCool;
    public float DashTimer;


    private bool Sturn = false;
    // 저장된 위치로 옮기기 위한 변수입니다.
    private Vector3 playerpos;
    
    // Start is called before the first frame update
    void Start()
    {
        if (DatabaseManager.Instance.path != null) {
            playerpos = DatabaseManager.Instance.toplayerpos;
        }
        // 초기화
        playerInput = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        
        animator = GetComponent<Animator>();
        // playerAudio = GetComponent<AudioSource>();
        // playerSpriteRenderer = GetComponent<SpriteRenderer>();
        onboard = true;
        //inventoryparents.SetActive(false);

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
        if (onboard == true)
        {
            playerRigidbody.gravityScale = 1; // 배 위에 있을 때 중력 1
            playerRigidbody.drag = 1;
            playerwalk();
            animator.SetBool("intoOcean", false);



        }
        else
        {
            playerRigidbody.gravityScale = 0; // 물 속에 있을 때 중력 0
            playerRigidbody.drag = 1.0f;
            playerswim();
            animator.SetBool("intoOcean", true);
        }

        Attack();

        
        // animator.SetBool("Grounded",isGrounded);
        // animator.SetBool("CurrentSpeed", CurrentSpeed);
    }
    
    // 플레이어가 땅 위에서 움직이는 것에 관한 함수
    public void playerwalk()
    {
        // 점프에 관한 내용입니다.
        if(Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpCount++;

            playerRigidbody.velocity = Vector2.zero;

            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            // playerAudio.Play();

        }
        
        else if(Input.GetButtonDown("Jump") && playerRigidbody.velocity.y > 0 )
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
         if(DashTimer < DashMoveCool)
        {

            playerRigidbody.AddForce(swimForce * Vector3.right * playerInput.move_x);
            playerRigidbody.AddForce(swimForce * Vector3.up * playerInput.move_y);
        }

        if (DashTimer <= 0 && Input.GetButtonDown("Jump"))
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

        if(DashTimer >= 0)
        {
            DashTimer -= Time.deltaTime;
        }


        // // x축 방향으로 움직일 때
        // if (playerInput.move_x > 0)
        // {
        //     playerRigidbody.AddForce(Vector3.right * playerInput.move_x);
        //     // playerSpriteRenderer.flipX = false;
        // }
        // else if (playerInput.move_x < 0)
        // {
        //     playerRigidbody.AddForce(Vector3.right * playerInput.move_x);
        //     // playerSpriteRenderer.flipX = true;
        // }
        // else
        // {
        //     transform.Translate(Vector3.zero);
        // }

            // // y축 방향으로 움직일 때 
            // if (playerInput.move_y > 0)
            // {
            //     playerRigidbody.AddForce(Vector3.up * playerInput.move_y);
            // }
            // else if (playerInput.move_y < 0)
            // {
            //     playerRigidbody.AddForce(Vector3.up * playerInput.move_y);
            // }
            // else
            // {
            //     transform.Translate(Vector3.zero);
            // }
    }

    public void Attack()
    {
        if (playerInput.fire == true)
        {
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
        Debug.Log("a물렸다");
        
        Sturn = true;
        if (playerRigidbody.mass > 0.1f){
            defaultmass = playerRigidbody.mass; }
        if (playerRigidbody.drag > 1f) {
            defaultdrag = playerRigidbody.drag;
        }
        playerRigidbody.mass = 0;
        playerRigidbody.drag = 0;
        Debug.Log("default mass : " + defaultmass + "\n default drag : " + defaultdrag);
        Debug.Log("current mass : " + playerRigidbody.mass + "\n current drag : " + playerRigidbody.drag);
    }

    public void SpitOut(Vector2 spitForce)
    {
        Debug.Log("뱉었다");
        Debug.Log(spitForce);
        playerRigidbody.mass = defaultmass;
        playerRigidbody.drag = defaultdrag;
        playerRigidbody.AddForce(spitForce);
        Debug.Log("default mass : " + defaultmass + "\n default drag : " + defaultdrag);
        Debug.Log("current mass : " + playerRigidbody.mass + "\n current drag : " + playerRigidbody.drag);
        Sturn = false;
    }

    public void SetSturn(bool sturn)
    {
        Sturn = sturn;
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

        yield return new WaitForSeconds(sturntime);

        //playerRigidbody.mass = defaultmass;
        //playerRigidbody.drag = defaultdrag;
        Sturn = false;
    }

    

}