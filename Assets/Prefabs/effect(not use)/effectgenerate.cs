using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectgenerate : MonoBehaviour
{
    // Start is called before the first frame update

    public float velocity;
    private GameObject[] bloodArr;
    public GameObject bloodParticle;
    public Rigidbody2D trackRD;

    public int bloodNUM;//nuber of pool
    public Vector2 particle_duration;
    public Vector2 particle_size;
    public int particle_angle;
    public Vector2 particle_number;//number of once
    public Vector2 particle_drag;

    public float bloodActSec;
    public float bloodNumPerSec;

    

    void Start()
    {
        
        bloodArr = new GameObject[bloodNUM];
        generateBloodPool();


        ActiveBlood(13, 3);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void generateBloodPool()
    {
        //RigidBody2D tempRD;
        for (int i = 0; i < bloodNUM; i++) {
            bloodArr[i] = Instantiate(bloodParticle, transform.position, Quaternion.identity);
            //tempRD = bloodArr[int].GetComponent<RigidBody2D>();
            //tempRD.velocity = 
            bloodArr[i].SetActive(false);

        }
    }


    public void ActiveBlood(float duration, float degree )
    {
        bloodActSec = duration;
        bloodNumPerSec = degree;

        StartCoroutine("bloodAct");


    }

    IEnumerator bloodAct()
    {
        float Timer;
        Timer = bloodActSec;
        Rigidbody2D tempRD;
        int i = 0;//arry index
        int k = 0;//number of one time
        while ( Timer>0 )
        {
            k = (int)Random.Range(particle_number.x , particle_number.y);
            while (k>0) {

                if (!bloodArr[i].activeSelf)
                {
                    
                    tempRD = bloodArr[i].GetComponent<Rigidbody2D>();
                    bloodArr[i].GetComponent<Transform>().position = trackRD.GetComponent<Transform>().position;
                    tempRD.velocity = trackRD.velocity + spreadAngle(trackRD.velocity);
                    tempRD.drag = Random.Range(particle_drag.x, particle_drag.y);
                    bloodArr[i].SetActive(true);
                    k--;
                }
                i++;
                if (i >= bloodNUM)
                {
                    i = 0;
                }
                
            }
            Timer -= 1 / bloodNumPerSec;
            yield return new WaitForSeconds(1/bloodNumPerSec);
        }


    }

    IEnumerator bloodParticleLife(GameObject blood, float duration)
    {
        float particleTimer = 0;
        while (particleTimer > 0 )
        {


            particleTimer -= 1 / bloodNumPerSec;
            yield return new WaitForSeconds(1 / bloodNumPerSec);
        }

        blood.SetActive(false);

    }

    public Vector2 spreadAngle(Vector2 trackVelocity)
    {
        if (particle_angle <= 90)
        {
            return Random.Range(-1, 1) * Vector2.up * particle_angle / 90;
        }
        return Random.Range(-1, 1) * Vector2.up * particle_angle / 90;

    } 
}
