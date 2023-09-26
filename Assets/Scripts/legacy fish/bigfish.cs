using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class bigfish : fpatrol
{
    // Start is called before the first frame update

    

    public override void Start()
    {
        base.Start();
        mass = 1f;
        drag = 0.2f;
        gravity = 0.01f;

        fishRigidbody.mass = mass;
        fishRigidbody.gravityScale = gravity;
        fishRigidbody.drag = drag;

        waitTime = 0.3f;

        maxY = -20;
        minY = -30;
    }

    // Update is called once per frame
    /*
    public override void Update()
    {
        
    }
    */
    public override void setNewSpot()
    {
        bool wayRight;
        if (dir.x>0)
        {
            wayRight = true;
        }
        else
        {
            wayRight = false;
        }

        //20%확률로 방향전환
        if ( ! wayRight ^ (Random.Range(0, 10) <= turnPercent)) 
        {
            //오른쪽 이동
            moveSpot.position = new Vector2(Random.Range(transform.position.x, maxX), Random.Range(minY,maxY));
        }else
        {
            //왼쪽으로 이동
            moveSpot.position = new Vector2(Random.Range(minX,transform.position.x), Random.Range(minY, maxY));

        }
        
        waitTime = startWaitTime;
        Debug.Log(moveSpot.position);
    }
}
