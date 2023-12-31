using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    //9. Update �޼��忡�� ���ӵ������� ����ȸ��


    private float durationTime = 3; //���ӵ����� ��Ÿ��


    public SpriteRenderer playerSpriteRenderer;
    public PlayerMove playerMove;

    public float HpUpGap;

    public Image hp;
    private WaitForSeconds damage_effect_time;


    private void Awake()
    {
        
        health = 100;
        //������Ʈ �Ҵ�: �ǰ� �ÿ� �� ��ȯ�� ����
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        //������Ʈ �Ҵ�: �ٴٿ� ���� �Ǻ� ����
        playerMove = GetComponent<PlayerMove>();
        damage_effect_time  = new WaitForSeconds(0.3f);

    }

    private void Start()
    {
        GameManager.Instance.shopManager.OxygenUpgrade += HPUp;
    }

    

    protected override void OnEnable()
    {
        
        maxHp = 100;
        startingHealth = 100;
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();
        //�ٴ�, �������� �Ǻ��Ͽ� �Ǹ� ��ų� ä���
        
    }

    private void Update()
    {
        
        durationTime -= Time.unscaledDeltaTime;

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

        if (health <= 0) {
            GameManager.Instance.pauseGame();
            Die();
            GameManager.Instance.playeronDeath();
        }

    }
    

    //�� ��ȯ �ڷ�ƾ
    private IEnumerator DamagedEffect()
    {
        playerSpriteRenderer.material.color = new Color(1f, 168 / 255f, 168 / 255f);
        yield return damage_effect_time; 
        playerSpriteRenderer.material.color = new Color(1f, 1f, 1f);

    }


    //ȸ�� ó��
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity�� RestoreHealth() ���� (ü�� ����)
        base.RestoreHealth(newHealth);
    }

    // ������ ó��
    public override void OnDamage(float damage, GameObject hiter, Vector3 hitPoint, Vector3 hitDirection)
    {
        StartCoroutine(DamagedEffect());
        // LivingEntity�� OnDamage() ����(������ ����)

        base.OnDamage(damage, hiter ,hitPoint, hitDirection);
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

    public void HPUp(){
        maxHp += HpUpGap;
    }


//onDeath()

}


