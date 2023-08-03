using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float Distance = 15f;
    public Vector2 MousePosition;
    public Camera Camera;
    public void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void Ray()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, MousePosition, Distance);

    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 Ŭ�� ��
        {
            MousePosition = Input.mousePosition; //���콺 Ŭ�� ��ġ��
            MousePosition = Camera.main.ScreenToWorldPoint(MousePosition); //���� ��ǥ ��ġ������ ��ȯ
            

            Debug.DrawRay(transform.position, MousePosition.normalized * Distance, Color.red,0.5f);
            Ray();
        }
    }
}
