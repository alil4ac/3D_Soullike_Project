using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    [SerializeField]
    GameObject IgnitionPrefab;


    private bool canCreateIgnition = true; // IgnitionPrefab 재생성 가능 여부를 나타내는 변수


    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireBreath"))//파이어브레스랑 부딪쳤을때가 아니라 범위안에 들어왔을때로 조건바꿔야함
        {
            if (canCreateIgnition)
            {
                GameObject ignition = Instantiate(IgnitionPrefab, transform.position, Quaternion.identity); // 프리팹 생성
                ParticleSystem[] particleSystemsE = ignition.GetComponentsInChildren<ParticleSystem>(); // 프리팹의 파티클시스템 get
                canCreateIgnition = false; // 재생성 방지
                yield return new WaitForSeconds(4f); // 일정 시간 대기
                canCreateIgnition = true; // 재생성 가능
                foreach (ParticleSystem ps in particleSystemsE) // 파티클 정지
                {
                    ps.Stop();
                }
                Destroy(ignition, 2f); // 파티클 파괴
            }
        }
    }
}
