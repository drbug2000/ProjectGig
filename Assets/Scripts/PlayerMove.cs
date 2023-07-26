using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public float jumpForce = 700f; // 점프 힘
    public float Speed = 5f;
    private int jumpCount = 0; // 누적 점프 횟수
   
    private bool isGrounded = false; // 바닥에 닿았는지 나타냄
    private bool isDead = false; // 사망 상태

    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private SpriteRenderer playerSpriteRenderer;
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    private bool CurrentSpeed = false;

    private PlayerController playerInput;
    private Animator playerAnimator;
    
    // Start is called before the first frame update
    private void Start()
    {
        // 초기화
        playerInput = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    private void Update()
    {
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && jumpCount<2)
        {
            jumpCount++;

            playerRigidbody.velocity = Vector2.zero;

            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            playerAudio.Play();

        }else if(Input.GetKeyUp(KeyCode.UpArrow) && playerRigidbody.velocity.y > 0 )
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
            playerSpriteRenderer.flipX = false;
            CurrentSpeed = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
            playerSpriteRenderer.flipX = true;
            CurrentSpeed = true;
        }
        else
        {
            CurrentSpeed = false;
        }


        // x = Input.GetAxis("Horizontal");
        /*
        Debug.Log(playerRigidbody.velocity.x);
        if (playerRigidbody.velocity.x == 0)
        {
            CurrentSpeed = false;
        }else
        {
            CurrentSpeed = true;
        }
        */

        
        animator.SetBool("Grounded",isGrounded);
        animator.SetBool("CurrentSpeed", CurrentSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
       if(other.tag == "Dead" && !isDead)
        {
            GameManager.instance.GetDamage(5);

        }

       /*
       if(other.tag == "Coin" && !isDead)
        {

        }
       */
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