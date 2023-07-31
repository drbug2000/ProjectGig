using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i_move : MonoBehaviour
{

    protected Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    public i_input playerInput;

   

    public I_face Ground;
    public I_face Swim;
    public I_face Mode;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<i_input>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        Ground = GetComponent<i_ground>();
        Swim = GetComponent<i_swim>();
        Mode = Ground;
    }

    // Update is called once per frame
    void Update()
    {
        Mode.move();
    }

    void Mchange()
    {
        Mode = Swim;
    }
}
