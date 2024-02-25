using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도

    void Update()
    {
        // 입력값을 받습니다.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 이동 벡터를 계산합니다.
        Vector3 moveVector = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // 플레이어를 이동시킵니다.
        transform.Translate(moveVector);
    }
}
