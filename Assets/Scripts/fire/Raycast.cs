using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float Distance = 15f;
    public Vector2 MousePosition;
    public Camera Camera;
    public Transform fpos; //발사 위치
    Vector2 dir;
    public void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void Ray()
    {
        RaycastHit2D hit = Physics2D.Raycast(fpos.position, dir, Distance);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 클릭 시
        {
            
            MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition); //카메라 좌표로 변환
            dir = transform.position;
            dir = MousePosition - dir;
            Debug.DrawRay(fpos.position, dir.normalized * Distance, Color.red,0.5f); //레이저 빔
            
            Ray();
        }
    }
}
