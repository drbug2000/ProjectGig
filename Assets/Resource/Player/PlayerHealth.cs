using System.Collections;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    //�÷��̾��� hp�� �����ϴ� ��ũ��Ʈ
    //1. Ȱ��ȭ �� hp �� �ʱ�ȭ
    //2. �ǰ� �� �������� �԰�, ���� ��ȯ�ȴ�.
    //3. �ǰ� �� Hp�� 0 ������ ��� ��� ���·� ��ȯ�Ѵ�.
    //4. ��� ��: ...���� onDeath �̺�Ʈ�� ����
    //5. onDeath: ��Ȱ��ȭ, �κ��丮 ����� ���� �ʱ�ȭ �� ��Ȱ��ȭ
    //6. �ٴٿ� ������ �Ǻ��Ͽ�, hp�� ȸ�� �Ǵ� �����Ѵ�.
    //7. �������� �÷��̾��� hp�� ���׷��̵��� �� �ִ�.

    public SpriteRenderer flshSpriteRenderer;

    private void Awake()
    {
        //������Ʈ �Ҵ�: �ǰ� �ÿ� �� ��ȯ
        flshSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected override void OnEnable()
    {
        startingHealth = 100;
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();

    }

    // ������ ó��
    public override void OnDamage(float damage, GameObject hiter, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead)
        {
            //���� ������ 1�ʰ� ��ȯ�ϴ� �ڷ�ƾ ����
            StartCoroutine(DamageEffect());
        }

        // LivingEntity�� OnDamage() ����(������ ����)
        base.OnDamage(damage, hiter ,hitPoint, hitDirection);
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

