using UnityEngine;

public interface IDamageable
{
    //�Է°�: ������ ��, �浹 ����, �浹 ǥ�� ����
    void OnDamage(float damage, GameObject hiter, Vector3 hitPoint, Vector3 hitNormal);
}