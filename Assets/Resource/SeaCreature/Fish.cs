using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{


    public FishTail fishtail;
    public FState currentState;
    public GameObject target;
    public GameObject awaytarget;

    //������ ���� ����
    // public float startWaitTime;
    //public float waitTime;
    //public Transform moveSpot;

    //public SpriteRenderer Renderer;
    //public Rigidbody2D fishRigidbody;


    //�̵������� ��� ����
    //public Vector2 dir;

    //����� Ư�� ���� ����
    public float mass;//����
    public float drag;//����
    public float gravity;//�޴� �߷�
    public float speed;

    public float MaxSpeed;
    public float MinSpeed;
    //���� ����
    public float RoamBoxMaxX;
    public float RoamBoxMinX;
    public float RoamBoxMaxY;
    public float RoamBoxMinY;
    //Spot���� 
    public float SpotRangeBig;
    public float SpotRangeSmall;


    //public int turnPercent;

    // Start is called before the first frame update
    public void Start()
    {
        fishtail = GetComponent<FishTail>();

    }

    // Update is called once per frame
    void Update()
    {
        currentState.stateUpdate();


    }

    public void SetState(FState nextState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = nextState;

        currentState.OnEnter(this,fishtail);

    }
    public void DefaultState()
    {

    }
}
