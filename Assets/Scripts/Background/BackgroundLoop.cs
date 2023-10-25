using UnityEngine;

// ���� ������ �̵��� ����� ������ ������ ���ġ�ϴ� ��ũ��Ʈ
public class BackgroundLoop : MonoBehaviour
{
    private float width; // ����� ���� ����

    private void Awake()
    {
        // ���� ���̸� �����ϴ� ó��
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
    }

    private void Update()
    {
        // ���� ��ġ�� �������� �������� width �̻� �̵������� ��ġ�� ����
        if (transform.position.x <= -3*width)
        {
            Reposition();
        }
    }

    // ��ġ�� �����ϴ� �޼���
    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 6f, 0);
        transform.position = (Vector2)transform.position + offset;

    }
}