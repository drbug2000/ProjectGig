using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string movehorizontalsName = "Horizontal"; // 앞뒤 움직임을 위한 입력축 이름
    public string moveverticcalName = "Vertical"; // 좌우 회전을 위한 입력축 이름
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    //public string reviveButtonName = "Revive"; // 부활을 위한 입력 버튼 이름
    //public string inventoryButtonName = "Inventory"; // 인벤토리 창을 위한 입력 버튼 이름
    public string shopButtonName = "Shop"; //상점 창을 위한 입력 버튼 이름

    // 값 할당은 내부에서만 가능
    public float move_x { get; private set; } // 감지된 움직임 입력값
    public float move_y { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    //public bool revive { get; private set; } // 감지된 부활 입력값
    //public bool inventory { get; private set; } // 감지된 인벤토리 입력값
    public bool jump { get; private set; }
    public bool shop { get; private set; }

    public bool ConSturn { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        ConSturn = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        // if (GameManager.instance != null && GameManager.instance.isGameover)
        // {
        //     move = 0;
        //     rotate = 0;
        //     fire = false;
        //     reload = false;
        //     return;
        // }

        // move에 관한 입력 감지
        move_x = Input.GetAxis(movehorizontalsName);
        // rotate에 관한 입력 감지
        move_y = Input.GetAxis(moveverticcalName);
        // fire에 관한 입력 감지
        fire = Input.GetButton(fireButtonName);
        //Input.GetButtonDown("Fire1")
        jump = Input.GetButtonDown("Jump");
        // reload에 관한 입력 감지
        //revive = Input.GetButtonDown(reviveButtonName);
        // Inventory에 관한 입력 감지
        //inventory = Input.GetButtonDown(inventoryButtonName);

        shop = Input.GetButtonDown(shopButtonName);
    }

    public void SetConSturn(bool s)
    {
        ConSturn = s;
    }
}
