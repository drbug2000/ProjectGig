using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i_input : MonoBehaviour
{
    
    public string inB = "p"; // 인벤토리 창을 위한 입력 버튼 이름

    

    //테스트 버튼 하나
    public bool InB { get; private set; } // 감지된 인벤토리 입력
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() { 
    

        //테스트 버튼
        InB = Input.GetButtonDown(inB);
    }
}
