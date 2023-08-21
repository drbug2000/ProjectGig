using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public GameObject gig;
    public float bulletSpeed;
    public Transform Fpos;



    // Start is called before the first frame update
    void Start()
    {
        gig = transform.Find("Gig").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //GameObject tool = Instantiate(toolPrefab, Fpos.position, gameObject.transform.rotation);
            Rigidbody2D rb = gig.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
