using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gig : MonoBehaviour
{

    private GameObject gun;
    private Gun gunscript;
    private Rigidbody2D rb;
    LineRenderer LR;

    public enum gigState { fire,  ready };
    public gigState State;

    private float timer;
    public bool isfire=false;

    private float gigdamage;

    IDamageable AttTarget;

    WaitForSecondsRealtime waitforsecondsforrealtime = new WaitForSecondsRealtime(0.5f);

    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        LR.startWidth = 0.1f;
        LR.endWidth = 0.1f;

        //rb = GetComponent<Rigidbody2D>();
        State = gigState.ready;
        gun = transform.parent.gameObject;
        gunscript = gun.GetComponent<Gun>();
        //Destroy(gameObject, 3f);
        //transform.position = new Vector3(-0.7f, 2,0);
        //Timer = -1f;

        gigdamage = gunscript.gigDamage;
        
    }
    
    public void onfire()
    {
        isfire = true;
        //Time.timeScale = 0.6f;
        //Timer = fireTime;
        //StartCoroutine("RollBasck", Speed);
        StartCoroutine("Line");
    }

    public void outfire()
    {
        isfire = false;
        transform.localPosition = new Vector3(-0.7f, 2, 0);
        //Debug.Log("outfire");
    }
    
    IEnumerator Line()
    {
        LR.enabled = true;
        while (isfire)
        {
            LR.SetPosition(0, gun.transform.Find("firePoint").gameObject.transform.position);
            LR.SetPosition(1, transform.position);

            yield return null;
        }
        LR.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("gig : Hit somthing");
        if (collision.gameObject.tag == "fish" && isfire)
        {
            gunscript.Hit();
            Debug.Log("gig : Hit fish");
            AttTarget = collision.gameObject.GetComponent<IDamageable>();
            //�ӽ� ����
            AttTarget.OnDamage(gigdamage, gameObject, Vector2.zero, Vector2.zero);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("gig : Hit somthing(trigger)"+ other);
        if (other.gameObject.tag == "fish" && isfire)
        {
            gunscript.Hit();
            AttTarget = other.gameObject.GetComponent<IDamageable>();
            //�ӽ� ����
            AttTarget.OnDamage(gigdamage, gameObject, Vector2.zero, Vector2.zero);
        }

    }



    public void updateDamage()
    {
        gigdamage = gunscript.gigDamage;
    }
}
