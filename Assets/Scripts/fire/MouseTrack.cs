using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrack : MonoBehaviour
{
    // 메인 카메라를 받아올 변수
    private Camera _camera;

    void Start()
    {
        // 메인카메라를 한 번 받아 저장한다.
        _camera = Camera.main;
    }

    void Update()
    {
        // ScreenToWorldPoint() 함수를 이용해 마우스의 좌표를 게임 좌표로 변환한다.
        // 2D게임이기에 Vector2로 변환
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        // (마우스 위치 - 오브젝트 위치)로 마우스의 방향을 구한다.
        Vector2 dirVec = mousePos - (Vector2)transform.position;

        // 방향벡터를 정규화한 다음 transform.up 벡터에 계속 대입
        transform.up = dirVec.normalized;
    }
}
