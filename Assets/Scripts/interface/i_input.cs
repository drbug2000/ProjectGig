using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i_input : MonoBehaviour
{
    
    public string inB = "p"; // �κ��丮 â�� ���� �Է� ��ư �̸�

    

    //�׽�Ʈ ��ư �ϳ�
    public bool InB { get; private set; } // ������ �κ��丮 �Է�
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() { 
    

        //�׽�Ʈ ��ư
        InB = Input.GetButtonDown(inB);
    }
}
