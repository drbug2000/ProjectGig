using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public float jumpForce = 700f; // 점프 힘
    public float swimForce = 350f; // 헤엄 힘
    public float Speed = 5f;
    protected int jumpCount = 0; // 누적 점프 횟수
   
    protected bool isGrounded = false; // 바닥에 닿았는지 나타냄 점프할 때 쓰는 변수
    // private bool isDead = false; // 사망 상태
    public bool onboard; // 갑판 위에 있는지

    protected Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    // private SpriteRenderer playerSpriteRenderer;
    // private Animator animator; // 사용할 애니메이터 컴포넌트
    // private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    protected bool CurrentSpeed = false; // 이건 뭔지 모르겠습니다...?

    public PlayerController playerInput;
    // private Animator playerAnimator;

    public GameObject inventoryparents;
    
    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        playerInput = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
        // playerAudio = GetComponent<AudioSource>();
        // playerSpriteRenderer = GetComponent<SpriteRenderer>();
        onboard = true;
        inventoryparents.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // 사용자 입력을 감지하고 점프하는 처리
        // if (isDead)
        // {
        //     return;
            // }
        if (onboard == true)
        {
            playerRigidbody.gravityScale = 1; // 배 위에 있을 때 중력 1
            playerRigidbody.drag = 1;
            playerwalk();
        }
        else
        {
            playerRigidbody.gravityScale = 0; // 물 속에 있을 때 중력 0
            playerRigidbody.drag = 1.5f;
            playerswim();
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
        playerRigidbody.AddForce(Vector3.right * playerInput.move_x);
        playerRigidbody.AddForce(Vector3.up * playerInput.move_y);

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




}