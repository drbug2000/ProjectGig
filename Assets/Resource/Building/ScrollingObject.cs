using UnityEngine;

// ���� ������Ʈ�� ��� �������� �����̴� ��ũ��Ʈ
public class ScrollingObject : MonoBehaviour
{
    public float speed = 5f; // �̵� �ӵ�

    private void Update()
    {
        // ���� ������Ʈ�� �������� ���� �ӵ��� ���� �̵��ϴ� ó��
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
