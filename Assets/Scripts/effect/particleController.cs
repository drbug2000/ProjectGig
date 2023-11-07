using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleController : MonoBehaviour
{

    public GameObject effectSystem;
    // Start is called before the first frame update
    void Start()
    {
        effectSystem = gameObject;
        TurnOffEffectSystem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOnEffectSystem()
    {
        if (effectSystem != null)
        {
            effectSystem.SetActive(true);
        }
    }

    // 이 함수는 Effect System을 비활성화합니다.
    public void TurnOffEffectSystem()
    {
        if (effectSystem != null)
        {
            effectSystem.SetActive(false);
        }
    }

    // 입자 개수를 조절하는 함수
    public void SetParticleCount(int count)
    {
        if (GetComponent<ParticleSystem>() != null)
        {
            var main = GetComponent<ParticleSystem>().main;
            main.maxParticles = count;
        }
    }

    // 강도를 조절하는 함수
    public void SetIntensity(float intensity)
    {
        if (GetComponent<ParticleSystem>() != null)
        {
            var main = GetComponent<ParticleSystem>().main;
            main.startLifetimeMultiplier = intensity;
        }
    }
}