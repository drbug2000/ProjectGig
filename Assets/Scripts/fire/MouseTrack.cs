using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrack : MonoBehaviour
{
    // ���� ī�޶� �޾ƿ� ����
    private Camera _camera;

    void Start()
    {
        // ����ī�޶� �� �� �޾� �����Ѵ�.
        _camera = Camera.main;
    }

    void Update()
    {
        // ScreenToWorldPoint() �Լ��� �̿��� ���콺�� ��ǥ�� ���� ��ǥ�� ��ȯ�Ѵ�.
        // 2D�����̱⿡ Vector2�� ��ȯ
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        // (���콺 ��ġ - ������Ʈ ��ġ)�� ���콺�� ������ ���Ѵ�.
        Vector2 dirVec = mousePos - (Vector2)transform.position;

        // ���⺤�͸� ����ȭ�� ���� transform.up ���Ϳ� ��� ����
        transform.up = dirVec.normalized;
    }
}
