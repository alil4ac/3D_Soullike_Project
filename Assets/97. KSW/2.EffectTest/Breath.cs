using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    [SerializeField]
    GameObject EnergyPrefab;
    [SerializeField]
    GameObject FireBreathPrefab;
    [SerializeField]
    GameObject skillSpawn;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SpawnEnergyAndFireBreath());
        }
    }

    IEnumerator SpawnEnergyAndFireBreath()
    {
        GameObject energy = Instantiate(EnergyPrefab, skillSpawn.transform.position, Quaternion.identity); //프리팹 생성
        ParticleSystem[] particleSystemsE = energy.GetComponentsInChildren<ParticleSystem>(); //프리팹의 파티클시스템 get
        yield return new WaitForSeconds(3f); //대기(기모으는 시간)
        foreach (ParticleSystem ps in particleSystemsE) //파티클 정지
        {
            ps.Stop();
        }
        Destroy(energy, 2f); //파티클 파괴

        GameObject fireBreath = Instantiate(FireBreathPrefab, skillSpawn.transform.position, Quaternion.Euler(0f, 45f, 0f));
        ParticleSystem[] particleSystemsF = fireBreath.GetComponentsInChildren<ParticleSystem>();
        yield return new WaitForSeconds(5f);
        foreach (ParticleSystem ps in particleSystemsF)
        {
        ps.Stop();
        }
        Destroy(fireBreath, 2f);
        
        yield return null;
    }
}
