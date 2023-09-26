using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    //플레이어의 hp를 관리하는 스크립트
    //1. 활성화 시 hp 값 초기화
    //2. 피격 시 데미지를 입고, 색이 변환된다.
    //3. 피격 시 Hp가 0 이하일 경우 사망 상태로 변환한다.
    //4. 사망 시: ...에서 onDeath 이벤트를 구독 (게임 매니저에서?)
    //5. onDeath: 비활성화, 인벤토리 비롯한 정보 초기화 후 재활성화
    //6. 바다와 육지를 판별하여, hp가 회복 또는 감소한다.
    //7. 상점에서 플레이어의 hp를 업그레이드할 수 있다. :
    //PlayerHealth 컴포넌트를 가져와 RestoreHealth()를 실행한다.
    //8. onDeath 메서드를 구현
    //9. Update 메서드에서 지속데미지와 지속회복


    private float durationTime = 3; //지속데미지 쿨타임


    public SpriteRenderer playerSpriteRenderer;
    public PlayerMove playerMove;

    Image hp;

    private void Awake()
    {
        //컴포넌트 할당: 피격 시에 색 변환을 위함
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        //컴포넌트 할당: 바다와 육지 판별 위함
        playerMove = GetComponent<PlayerMove>();
        hp = GetComponent<Image>();
    }


    protected override void OnEnable()
    {
        maxHp = 100;
        startingHealth = 100;
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();
        //바다, 육지임을 판별하여 피를 깎거나 채운다
        
    }

    private void Update()
    {
        durationTime -= Time.deltaTime;

        if (!playerMove.onboard && durationTime <= 0 && !dead)
        {
            OnDamage(3, null,Vector3.zero, Vector3.zero );
            durationTime = 3;
        }

        if (playerMove.onboard && durationTime <= 0 && !dead && health < maxHp)
        {
            RestoreHealth(10);
            durationTime = 3;
        }
        hp.fillAmount = health / maxHp;

    }
    

    //색 변환 코루틴
    private IEnumerator DamagedEffect()
    {
        playerSpriteRenderer.material.color = new Color(1f, 168 / 255f, 168 / 255f);
        yield return new WaitForSeconds(0.3f);
        playerSpriteRenderer.material.color = new Color(1f, 1f, 1f);

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
        StartCoroutine(DamagedEffect());
        // LivingEntity의 OnDamage() 실행(데미지 적용)

        base.OnDamage(damage, hiter ,hitPoint, hitDirection);
    }
    

    public override void Die()
    {
        //애니메이션, 사운드 재생
        
        //GameManager의 playeronDeath 메서드 실행
        //돈 빼가거나 유지시키기
        //you died! 화면창에 띄우기
        // LivingEntity의 Die() 실행
        base.Die();

    }


//onDeath()

}


