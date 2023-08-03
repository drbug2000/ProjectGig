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
        
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //마우스 좌표 카메라 좌표로 변환
        Vector2 dirVec = mousePos - (Vector2)transform.position; //마우스 방향 구함
        transform.up = dirVec.normalized; // 방향벡터를 정규화한 다음 transform.up 벡터에 계속 대입
    }
}
