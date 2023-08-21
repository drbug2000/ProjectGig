using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gig : MonoBehaviour
{

    private GameObject gun;
    private Gun gunscript;
    private Rigidbody2D rb;

    public enum gigState { fire, hit, rollback, ready };
    public gigState State;
    private float timer;
    private float Timer
    {
        get { return timer; }
        set
        {
            timer = value;
            if (timer < 0)
            {
                Timer = gunscript.fireTime;
                switch (State)
                {
                    case gigState.fire:
                    case gigState.hit:
                        State = gigState.rollback;
                        break;
                    case gigState.rollback:
                        State = gigState.ready;
                        break;
                    case gigState.ready:
                        break;
                    default:
                        Debug.Log("Timer set error"+State);
                        break;

                }
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        State = gigState.ready;
        gun = transform.parent.gameObject;
        gunscript = gun.GetComponent<Gun>();
        //Destroy(gameObject, 3f);
        //transform.position = new Vector3(-0.7f, 2,0);
        Timer = -1f;
        
    }

    void Update()
    {
        if(State== gigState.ready)
        {
            return;

        }else if(State == gigState.hit)
        {

        }else if(State == gigState.fire)
        {
            rb.velocity = transform.up * gunscript.bulletSpeed;
        }else if (State == gigState.rollback)
        {
            rb.velocity = -1* transform.up * gunscript.bulletSpeed;
        }


    }


    public void fire(float fireTime,float Speed)
    {
        State = gigState.fire;
        //Timer = fireTime;
        StartCoroutine("RollBasck", Speed);
    }

    IEnumerator RollBasck(float speed)
    {
        Debug.Log("Rollback corutine");
        Debug.Log("re speed"+speed);
        //Debug.Log("timer" + Timer);
        //발사
        while (State == gigState.fire )
        {
            Timer -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("fire end");
        State = gigState.rollback;
        //Timer = fireTime;

        while (State == gigState.rollback)
        {
            Timer -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("rollback end");
        //rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        ready();
        //yield return new WaitForSeconds(fireTime);
        //돌아오기

        //다시 ready 상태로


    }

    public void ready()
    {
        State = gigState.ready;
        gunscript.ready();
        transform.localPosition= new Vector3(-0.7f, 2, 0);
        rb.velocity = Vector2.zero;
    }
}
