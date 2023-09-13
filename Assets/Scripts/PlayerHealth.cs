using System.Collections;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    //�÷��̾��� hp�� �����ϴ� ��ũ��Ʈ
    //1. Ȱ��ȭ �� hp �� �ʱ�ȭ
    //2. �ǰ� �� �������� �԰�, ���� ��ȯ�ȴ�.
    //3. �ǰ� �� Hp�� 0 ������ ��� ��� ���·� ��ȯ�Ѵ�.
    //4. ��� ��: ...���� onDeath �̺�Ʈ�� ���� (���� �Ŵ�������?)
    //5. onDeath: ��Ȱ��ȭ, �κ��丮 ����� ���� �ʱ�ȭ �� ��Ȱ��ȭ
    //6. �ٴٿ� ������ �Ǻ��Ͽ�, hp�� ȸ�� �Ǵ� �����Ѵ�.
    //7. �������� �÷��̾��� hp�� ���׷��̵��� �� �ִ�. :
    //PlayerHealth ������Ʈ�� ������ RestoreHealth()�� �����Ѵ�.
    //8. onDeath �޼��带 ����

    
    private int seaDamage = 5;
    private int restorevalue = 10; 

    public SpriteRenderer playerSpriteRenderer;
    public PlayerMove playerMove;
    

    private void Awake()
    {
        //������Ʈ �Ҵ�: �ǰ� �ÿ� �� ��ȯ�� ����
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        //������Ʈ �Ҵ�: �ٴٿ� ���� �Ǻ� ����
        playerMove = GetComponent<PlayerMove>();
    }

    protected override void OnEnable()
    {
        maxHp = 100;
        startingHealth = 100;
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();
        //�ٴ�, �������� �Ǻ��Ͽ� �Ǹ� ��ų� ä���
        //StartCoroutine(OceanDamaging(seaDamage, new Vector3(0, 0, 0), new Vector3(0, 0, 0)));



    }

    //õõ�� ȸ���ϴ� �ڷ�ƾ ó��
    private IEnumerator Restoring(int value)
    {
        while(health < maxHp)
        {
            RestoreHealth(value);
            yield return new WaitForSeconds(4f);
        }
        
    }
    //�ٴٿ��� �������� �԰ų� ȸ���ϴ� �ڷ�ƾ ó��
    private IEnumerator OceanDamaging(float damage, Vector3 hitpoint, Vector3 hitDirection)
    {
        while (!dead)
        {
            if (!playerMove.onboard)
            {
                OnDamage(damage, hitpoint, hitDirection);
                //�� ��ȯ �ڷ�ƾ
                //playerSpriteRenderer.material.color = new Color(1f, 168 / 255f, 168 / 255f);
                //yield return new WaitForSeconds(0.3f);
                //playerSpriteRenderer.material.color = new Color(1f, 1f, 1f);
                //������ ����
                yield return new WaitForSeconds(5f);
            }
            else
            {
                StartCoroutine(Restoring(restorevalue));
            }
        }
        
    }


    //ȸ�� ó��
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity�� RestoreHealth() ���� (ü�� ����)
        base.RestoreHealth(newHealth);
    }

    // ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {

        // LivingEntity�� OnDamage() ����(������ ����)

        base.OnDamage(damage, hitPoint, hitDirection);
    }


    public override void Die()
    {
        //�ִϸ��̼�, ���� ���
        
        //GameManager�� playeronDeath �޼��� ����
        //�� �����ų� ������Ű��
        //you died! ȭ��â�� ����
        // LivingEntity�� Die() ����
        base.Die();

    }
}

//onDeath()




