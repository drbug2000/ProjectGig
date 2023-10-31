using System;
using UnityEngine;

// ����ü ���� ������Ʈ�� ����
// Health , OnDamage, Die, restoreHealth, onDeath �̺�Ʈ�� ����


public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth; // ���� ü��
    public float maxHp;
    public float health;//{ get; protected set; } // ���� ü��
    public bool dead { get; protected set; } // ��� ����
    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ

    // ����ü�� Ȱ��ȭ�ɶ� ���¸� ����
    protected virtual void OnEnable()
    {
        // ������� ���� ���·� ����
        dead = false;
        // ü���� ���� ü������ �ʱ�ȭ
        
        health = startingHealth;
    }

    // �������� �Դ� ���
    public virtual void OnDamage(float damage, GameObject hiter, Vector3 hitPoint, Vector3 hitNormal)
    {
        // ��������ŭ ü�� ����
        health -= damage;
        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            // �̹� ����� ��� ü���� ȸ���� �� ����
            return;
        }
         health += newHealth;
            
    }


    // ��� ó��
    public virtual void Die()
    {
        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        if (onDeath != null)
        {
            onDeath();
        }

        // ��� ���¸� ������ ����
        dead = true;
    }
}