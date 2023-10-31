using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class test_Vec : MonoBehaviour
{

    private Vector3 Vec_A;
    public Vector3 Vec_B;
    private Vector3 Vec_C = new Vector3(1.2f, 1.3f, 0);
    public Vector3 Vec_D = new Vector3(1.4f, 1.5f, 0);


    // Start is called before the first frame update
    void Start()
    {
        Vec_A = new Vector3(-0.2f, -0.3f, 0);
        Vec_B = new Vector3(-0.4f, -0.5f, 0);
        //Debug.Log("Vec A : " + Vec_A);
        //Debug.Log("Vec B : " + Vec_B);
        //Debug.Log("Vec C : " + Vec_C);
        //Debug.Log("Vec D : " + Vec_D);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Vec A : " + Vec_A);
        //Debug.Log("Vec B : " + Vec_B);
        //Debug.Log("Vec C : " + Vec_C);
        //Debug.Log("Vec D : " + Vec_D);
    }
}
