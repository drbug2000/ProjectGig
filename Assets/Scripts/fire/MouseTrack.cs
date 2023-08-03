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
        
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //���콺 ��ǥ ī�޶� ��ǥ�� ��ȯ
        Vector2 dirVec = mousePos - (Vector2)transform.position; //���콺 ���� ����
        transform.up = dirVec.normalized; // ���⺤�͸� ����ȭ�� ���� transform.up ���Ϳ� ��� ����
    }
}
