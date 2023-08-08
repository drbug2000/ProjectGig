using UnityEngine;

public interface IDamageable
{
    //입력값: 데미지 값, 충돌 지점, 충돌 표면 방향
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
