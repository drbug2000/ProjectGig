using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleController : MonoBehaviour
{

    public GameObject effectSystemPrefab;
    public GameObject mainEffect;
    public bool mainActiveFlag = false;//현재 main effect 실행 시 새로운 effect 생성 
    // Start is called before the first frame update
    void Start()
    {
        //effectSystem = gameObject;
        
        mainEffect = Instantiate(effectSystemPrefab, transform);
        TurnOffEffectSystem(mainEffect);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMainEffect()
    {
        TurnOnEffectSystem(mainEffect);
        mainActiveFlag = false;
    }
    public void StartMainEffect(float time)
    {
        StartCoroutine(OnEffect(mainEffect, time));
        mainActiveFlag = true;
    }
    public void EndMainEffect()
    {
        TurnOffEffectSystem(mainEffect);
        mainActiveFlag = false;
    }
    public void EndSubEffect() {
        StopCoroutine("OnEffect");
    }

    public void TurnOnEffectSystem(GameObject effect)
    {
        if (effect != null)
        {
            effect.SetActive(true);
            effect.GetComponent<ParticleSystem>().Play();
            
        }
    }

    // 이 함수는 Effect System을 비활성화합니다.
    public void TurnOffEffectSystem(GameObject effect)
    {
        if (effect != null)
        {
            effect.SetActive(false);
            //effect.Stop();
            
        }
    }

    public void SetEffectSize(GameObject effect, float size)
    {
        if (effect.GetComponent<ParticleSystem>() != null)
        {
            Debug.Log("size in effect : 0"+size);
            var EF = effect.GetComponent<ParticleSystem>();
            var EF_main = EF.main;
            EF_main.maxParticles = (int)(size * size * 100);
            EF_main.startLifetime = size / 4 + 1;
            EF_main.startSpeed = (int)(size * 1.2) + 7;
            var EF_shape = EF.shape;
            //EF = effect.GetComponent<ParticleSystem>().shape;
            EF_shape.radius = (int)(size * 1.5 + 1.5);
            var EF_emission = EF.emission;
            //EF = effect.GetComponent<ParticleSystem>().emission;
            EF_emission.rateOverTime = (int)(size * 100);
        }
        else
        {
            Debug.Log("we got null. flag : " + mainActiveFlag);
        }

    }

    // 입자 개수를 조절하는 함수
    public void SetParticleCount(GameObject effect, int count)
    {
        if (effect.GetComponent<ParticleSystem>() != null)
        {
            var main = effect.GetComponent<ParticleSystem>().main;
            main.maxParticles = count;
            //main.
        }
    }

    // 강도를 조절하는 함수
    public void SetIntensity(GameObject effect, float intensity)
    {
        if (effect.GetComponent<ParticleSystem>() != null)
        {
            var main = effect.GetComponent<ParticleSystem>().main;
            main.startLifetimeMultiplier = intensity;
        }
    }

    //public GameObject AddEffect(float secTime, Vector2 position,);


    //global position
    public GameObject AddEffect(float secTime,Vector2 position,float size=-1)//,int Count=-1, float intensity=-1)
    {
        GameObject _effect;
        if (!mainActiveFlag)
        {
            _effect = mainEffect;
            mainActiveFlag = true;
        }
        else
        {
            _effect = Instantiate(effectSystemPrefab, transform);//position, Quaternion.identity);
        }
        /*
        if(Count > 0)
        {
            SetParticleCount(_effect, Count);
        }
        if (intensity > 0)
        {
            SetIntensity(_effect, intensity);
        }
        */
        if (size >= 0)
        {
            SetEffectSize(_effect, size);
        }
        else { Debug.Log("not set size:"+size); }
        
        SetPosition(_effect, position);
        StartCoroutine(OnEffect(_effect,secTime));
        return _effect;
    }

    public void AddEffectLocal(float secTime, Vector2 Pos)
    {
        GameObject _effect;
        _effect = AddEffect(secTime, transform.position);
        SetPositionLocal(_effect, Pos);
        //_effect.transform.localPosition = Pos;
    }



    public void SetMainLocal(Vector2 localPos)
    {
        mainEffect.transform.localPosition = localPos;
    }


    public void SetPosition(GameObject effect, Vector2 Pos)
    {
        effect.transform.position =new( Pos.x,0);
    }
    public void SetPositionLocal(GameObject effect, Vector2 localPos)
    {
        effect.transform.localPosition = localPos;
    }


    IEnumerator OnEffect(GameObject effect, float time)
    {
        //Debug.Log("effect start");
        //Debug.Log("effect pos:" + effect.transform.position);
        float Timer = time;
        
        TurnOnEffectSystem(effect);

        if (time == null)
        {
            yield break;
        }
        while (Timer > 0)
        {
            Timer -= Time.deltaTime;


            yield return null;
        }

        TurnOffEffectSystem(effect);
        if(!System.Object.ReferenceEquals(mainEffect, effect))
        {
            Destroy(effect);
        }
        else
        {
            mainActiveFlag = false;
        }
        


    }


}