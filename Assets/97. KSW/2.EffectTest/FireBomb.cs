using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : MonoBehaviour
{
    [SerializeField]
    GameObject FireBombPrefab;
    [SerializeField]
    GameObject FireBombExplosionPrefab;
    [SerializeField]
    float minRadius = 5f; // 최소 반지름
    [SerializeField]
    float maxRadius = 10f; // 최대 반지름

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 확인
        {
            SpawnFireBombs(5); // 원형 범위 내에 5개의 FireBombPrefab 생성
        }
    }

    void SpawnFireBombs(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius); // 원형 범위 내의 랜덤한 위치 계산
            Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

            GameObject fireBomb = Instantiate(FireBombPrefab, spawnPosition, Quaternion.identity); // FireBombPrefab을 생성 위치에 생성

            StartCoroutine(TriggerExplosion(fireBomb));
        }
    }

    IEnumerator TriggerExplosion(GameObject fireBomb)
    {
        yield return new WaitForSeconds(4.7f); 

        Instantiate(FireBombExplosionPrefab, fireBomb.transform.position, Quaternion.identity); // FireBombExplosionPrefab을 원래 FireBombPrefab이 있던 자리에 생성

        Destroy(fireBomb); // FireBombPrefab 파괴
    }
}
