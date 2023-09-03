using System.Collections;
using UnityEngine;
using System;

public class FishHealth : LivingEntity
{
    //������� ����,  �ǰ��� �����ϴ� ��ũ��Ʈ
    //1. Ȱ��ȭ �� ���� �� �ʱ�ȭ
    //2. �ǰ� �� �������� �԰�, ���� ��ȯ�ȴ�.
    //3. �ǰ� �� Hp�� 0 ������ ��� ��� ���·� ��ȯ�Ѵ�.
    //4. ��� �� ��Ȱ��ȭ: playerAttack �Ǵ� �ۻ쿡�� fish.onDeath �̺�Ʈ�� ����
    //5. ��� �� ������ȭ: playerAttack �Ǵ� �ۻ쿡�� fish.onDeath �̺�Ʈ�� ����
    private FishClass fish;
    private SpriteRenderer flshSpriteRenderer;
    Action Dead;

    private void Awake()
    {
        //������Ʈ �Ҵ�: �ǰ� �ÿ� �� ��ȯ
        flshSpriteRenderer = GetComponent<SpriteRenderer>();
        fish = GetComponent<FishClass>();
        Dead += fish.OnDeath;

    }

    protected override void OnEnable()
    {
        
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();
        startingHealth = fish.startHP;

    }

    // ������ ó��
    public override void OnDamage(float damage, GameObject hiter , Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead)
        {
            //���� ������ 1�ʰ� ��ȯ�ϴ� �ڷ�ƾ ����
            StartCoroutine(DamageEffect());
        }

        // LivingEntity�� OnDamage() ����(������ ����)
        base.OnDamage(damage, hiter,hitPoint, hitDirection);

        if (dead)
        {
            fish.target = hiter;
        }
    }
    //�� ��ȯ �ڷ�ƾ ����
    private IEnumerator DamageEffect()
    {
        flshSpriteRenderer.material.color = new Color(1f, 168 / 255f, 168 / 255f);
        yield return new WaitForSeconds(1f);
        flshSpriteRenderer.material.color = new Color(1f, 1f, 1f);
    }


    public override void Die()
    {
        // LivingEntity�� Die() ����
        base.Die();
    }
}