using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    GameObject FireBallPrefab;
    [SerializeField]
    GameObject FireChargePrefab;
    [SerializeField]
    GameObject skillSpawn;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(SpawnFireBall());
        }
    }

    IEnumerator SpawnFireBall()
    {
        Instantiate(FireChargePrefab, skillSpawn.transform.position, Quaternion.identity); // FireChargePrefab 생성
        GameObject fireBall = Instantiate(FireBallPrefab, skillSpawn.transform.position, Quaternion.identity); // FireBallPrefab 생성

        //이 부분부터
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = (player.transform.position - skillSpawn.transform.position).normalized; // 플레이어 방향으로 정규화된 벡터 계산
        Rigidbody fireBallRigidbody = fireBall.GetComponent<Rigidbody>();
        float initialSpeed = 1f; // 초기 속도
        float acceleration = 10f; // 가속도

        while (fireBall != null)
        {
            fireBallRigidbody.velocity = direction * initialSpeed; // 방향에 따른 속도 적용
            initialSpeed += acceleration * Time.deltaTime; // 속도 증가

            yield return null;
        }
        //이 부분까지 플레이어 방향을 향해 발사하는 코드. 테스트하느라 넣은거라 이 부분을 몬스터 패턴 매커니즘에 맞게 수정해야함

    }
}
