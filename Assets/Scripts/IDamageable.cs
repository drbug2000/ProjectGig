using UnityEngine;

public interface IDamageable
{
    //�Է°�: ������ ��, �浹 ����, �浹 ǥ�� ����
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
