using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i_ground : MonoBehaviour,I_face
{


    protected Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    public i_input playerInput;
    int Speed;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<i_input>();
        playerRigidbody = GetComponent<Rigidbody2D>();

    }

    public void move()
    {
        if (i_input.InB)
        {
            transform.Translate(Vector3.right * Speed *  Time.deltaTime);
        }
    }
}
