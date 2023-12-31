using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{


    public FishTail fishtail;
    public FState currentState;
    public GameObject target;
    public GameObject awaytarget;

    //움직임 관련 변수
    // public float startWaitTime;
    //public float waitTime;
    //public Transform moveSpot;

    //public SpriteRenderer Renderer;
    //public Rigidbody2D fishRigidbody;


    //이동방향을 담는 벡터
    //public Vector2 dir;

    //물고기 특성 관련 변수
    public float mass;//무게
    public float drag;//저항
    public float gravity;//받는 중력
    public float speed;

    public float MaxSpeed;
    public float MinSpeed;
    //유영 범위
    public float RoamBoxMaxX;
    public float RoamBoxMinX;
    public float RoamBoxMaxY;
    public float RoamBoxMinY;
    //Spot범위 
    public float SpotRangeBig;
    public float SpotRangeSmall;

    public float RoamWaitTime;


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
