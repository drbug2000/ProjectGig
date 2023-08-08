using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishClass : MonoBehaviour
{


    public FishFin fishfin;
    public FishState currentState;
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

    public float RoamWaitTime;


    //public int turnPercent;

    // Start is called before the first frame update
    public virtual void Start()
    {
        fishfin = GetComponent<FishFin>();

    }

    // Update is called once per frame
    public virtual void Update()
    {
        currentState.stateUpdate();
    }

    public void SetState(FishState nextState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = nextState;

        currentState.OnEnter(this,this.fishfin);

    }
    public virtual void DefaultState()
    {

    }
}
