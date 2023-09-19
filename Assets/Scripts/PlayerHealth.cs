using System.Collections;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    //플레이어의 hp를 관리하는 스크립트
    //1. 활성화 시 hp 값 초기화
    //2. 피격 시 데미지를 입고, 색이 변환된다.
    //3. 피격 시 Hp가 0 이하일 경우 사망 상태로 변환한다.
    //4. 사망 시: ...에서 onDeath 이벤트를 구독
    //5. onDeath: 비활성화, 인벤토리 비롯한 정보 초기화 후 재활성화
    //6. 바다와 육지를 판별하여, hp가 회복 또는 감소한다.
    //7. 상점에서 플레이어의 hp를 업그레이드할 수 있다.

    private int maxHp = 100;
    private int seaDamage = 5;
    private int restorevalue = 10; 

    public SpriteRenderer flshSpriteRenderer;
    public PlayerMove playerMove;
   

    private void Awake()
    {
        //컴포넌트 할당: 피격 시에 색 변환을 위함
        flshSpriteRenderer = GetComponent<SpriteRenderer>();
        //컴포넌트 할당: 바다와 육지 판별 위함
        playerMove = GetComponent<PlayerMove>();
    }

    protected override void OnEnable()
    {
        startingHealth = 100;
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();

    }

    private void Update()
    {
        //바다, 육지 확인하여 피를 깎는다
        if (playerMove.onboard != true)
        {
            OnDamage(seaDamage,null, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        }
        else
        {

            if (dead == false)
            {
                if (health >= maxHp)
                {
                    health = maxHp;
                }
                else
                {
                    Restoring(restorevalue);            
                }
            }
            
        }


    }
    //천천히 회복하는 코루틴 처리
    private IEnumerator Restoring(int value)
    {
        RestoreHealth(value);
        yield return new WaitForSeconds(4f);
    }


    //회복 처리
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity의 RestoreHealth() 실행 (체력 증가)
        base.RestoreHealth(newHealth);
    }

    // 데미지 처리
    public override void OnDamage(float damage, GameObject hiter, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead)
        {
            //붉은 색으로 1초간 변환하는 코루틴 실행
            StartCoroutine(DamageEffect());
        }

        // LivingEntity의 OnDamage() 실행(데미지 적용)
        base.OnDamage(damage, hiter ,hitPoint, hitDirection);
    }
    //색 변환 코루틴 구현
    private IEnumerator DamageEffect()
    {
        flshSpriteRenderer.material.color = new Color(1f, 168 / 255f, 168 / 255f);
        yield return new WaitForSeconds(2f);
        flshSpriteRenderer.material.color = new Color(1f, 1f, 1f);
    }
    


    public override void Die()
    {
        // LivingEntity의 Die() 실행
        base.Die();
    }
}

