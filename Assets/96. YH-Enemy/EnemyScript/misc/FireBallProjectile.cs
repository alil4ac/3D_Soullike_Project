using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    GameObject player;

    Vector3 direction;

    Rigidbody fireBallRigidbody;



    float initialSpeed = 1f; // 초기 속도
    float acceleration = 10f; // 가속도
    float hommingPower = 1.0f; //
    float hommingRange = 5f;


    bool isHomming = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fireBallRigidbody = GetComponent<Rigidbody>();

        direction = (player.transform.position - transform.position).normalized; // 플레이어 방향으로 정규화된 벡터 계산

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (player != null && isHomming)
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);


            direction = (player.transform.position - transform.position).normalized; // 플레이어 방향으로 정규화된 벡터 계산
            Vector3 desiredDirection = Vector3.RotateTowards(transform.forward, direction, hommingPower * Time.fixedDeltaTime, 0.0f);
            fireBallRigidbody.rotation = Quaternion.LookRotation(desiredDirection);




            if (playerDistance < hommingRange)
            {
                isHomming = false;
                //fireBallRigidbody.velocity = transform.forward * initialSpeed; // 방향에 따른 속도 적용
                player = null;

                Debug.Log("호밍 해제");

            }
        }

        fireBallRigidbody.velocity = direction * initialSpeed; // 방향에 따른 속도 적용
        initialSpeed += acceleration * Time.fixedDeltaTime; // 속도 증가
    }


}
