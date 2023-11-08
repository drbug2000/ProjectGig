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

    // 입자 개수를 조절하는 함수
    public void SetParticleCount(GameObject effect, int count)
    {
        if (effect.GetComponent<ParticleSystem>() != null)
        {
            var main = effect.GetComponent<ParticleSystem>().main;
            main.maxParticles = count;
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

    //global position
    public GameObject AddEffect(float secTime, /*int Count, float intensity,*/Vector2 position  )
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

        StartCoroutine(OnEffect(_effect,secTime));
        return _effect;
    }

    public void AddEffectLocal(float secTime, Vector2 Pos)
    {
        GameObject _effect;
        _effect = AddEffect(secTime, transform.position);
        _effect.transform.localPosition = Pos;
    }

    public void SetMainLocal(Vector2 localPos)
    {
        mainEffect.transform.localPosition = localPos;
    }

    IEnumerator OnEffect(GameObject effect, float time)
    {
        Debug.Log("effect start");
        Debug.Log("effect pos:" + effect.transform.position);
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