using System.Collections;
using UnityEngine;

public class FishHealth : LivingEntity
{
    //������� ����,  �ǰ��� �����ϴ� ��ũ��Ʈ
    //1. Ȱ��ȭ �� ���� �� �ʱ�ȭ
    //2. �ǰ� �� �������� �԰�, ���� ��ȯ�ȴ�.
    //3. �ǰ� �� Hp�� 0 ������ ��� ��� ���·� ��ȯ�Ѵ�.
    //4. ��� �� ��Ȱ��ȭ: playerAttack �Ǵ� �ۻ쿡�� fish.onDeath �̺�Ʈ�� ����
    //5. ��� �� ������ȭ: playerAttack �Ǵ� �ۻ쿡�� fish.onDeath �̺�Ʈ�� ����

    public SpriteRenderer flshSpriteRenderer;

    private void Awake()
    {
        //������Ʈ �Ҵ�: �ǰ� �ÿ� �� ��ȯ
        flshSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected override void OnEnable()
    {
        startingHealth = 50;
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();

    }

    // ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead)
        {
            //���� ������ 1�ʰ� ��ȯ�ϴ� �ڷ�ƾ ����
            StartCoroutine(DamageEffect());
        }

        // LivingEntity�� OnDamage() ����(������ ����)
        base.OnDamage(damage, hitPoint, hitDirection);
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