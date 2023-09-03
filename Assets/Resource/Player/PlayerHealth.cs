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

    public SpriteRenderer flshSpriteRenderer;

    private void Awake()
    {
        //컴포넌트 할당: 피격 시에 색 변환
        flshSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected override void OnEnable()
    {
        startingHealth = 100;
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();

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
        yield return new WaitForSeconds(1f);
        flshSpriteRenderer.material.color = new Color(1f, 1f, 1f);
    }


    public override void Die()
    {
        // LivingEntity의 Die() 실행
        base.Die();
    }
}

