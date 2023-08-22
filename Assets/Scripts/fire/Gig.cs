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

    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        LR.startWidth = 0.3f;
        LR.endWidth = 0.3f;

        //rb = GetComponent<Rigidbody2D>();
        State = gigState.ready;
        gun = transform.parent.gameObject;
        gunscript = gun.GetComponent<Gun>();
        //Destroy(gameObject, 3f);
        //transform.position = new Vector3(-0.7f, 2,0);
        //Timer = -1f;
        
    }

    void Update()
    {

        

    }

    
    public void onfire()
    {
        isfire = true;
        //Timer = fireTime;
        //StartCoroutine("RollBasck", Speed);
        StartCoroutine("Line");
    }

    public void outfire()
    {
        isfire = false;
        transform.localPosition = new Vector3(-0.7f, 2, 0);
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
    



}
