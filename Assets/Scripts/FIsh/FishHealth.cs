using System.Collections;
using UnityEngine;
using System;

public class FishHealth : LivingEntity
{
    //물고기의 생명,  피격을 관리하는 스크립트
    //1. 활성화 시 생명 값 초기화
    //2. 피격 시 데미지를 입고, 색이 변환된다.
    //3. 피격 시 Hp가 0 이하일 경우 사망 상태로 변환한다.
    //4. 사망 시 비활성화: playerAttack 또는 작살에서 fish.onDeath 이벤트를 구독
    //5. 사망 시 아이템화: playerAttack 또는 작살에서 fish.onDeath 이벤트를 구독
    private FishClass fish;
    private SpriteRenderer flshSpriteRenderer;
    private WaitForSeconds corrutine_time;
    //Action Dead;

    private void Awake()
    {
        //컴포넌트 할당: 피격 시에 색 변환
        flshSpriteRenderer = GetComponent<SpriteRenderer>();
        fish = GetComponent<FishClass>();
        onDeath += fish.OnDeath;
        //startingHealth = fish.FishHP;
        corrutine_time = new WaitForSeconds(0.7f);

    }

    protected override void OnEnable()
    {
        
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();

        //사망후 피격 스프라이트 정상화
        flshSpriteRenderer.material.color = new Color(1f, 1f, 1f);
        
        //health = fish.startHP;

    }

    // 데미지 처리
    public override void OnDamage(float damage, GameObject hiter , Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead)
        {
            //붉은 색으로 1초간 변환하는 코루틴 실행
            StartCoroutine(DamageEffect());
        }
        else
        {
            //이미 죽어있는 물고기를 공격했을 때
            //처음 물고기를 죽였을 때와의 차이는 fishclass에서 처리
            Die();
        }
        
        // LivingEntity의 OnDamage() 실행(데미지 적용)
        base.OnDamage(damage, hiter,hitPoint, hitDirection);

        if (dead)
        {
            fish.target = hiter;
        }
    }
    //색 변환 코루틴 구현
    private IEnumerator DamageEffect()
    {
        flshSpriteRenderer.material.color = new Color(1f, 168 / 255f, 168 / 255f);
        yield return corrutine_time;
        flshSpriteRenderer.material.color = new Color(1f, 1f, 1f);
    }


    public override void Die()
    {
        // LivingEntity의 Die() 실행
        base.Die();
    }
}