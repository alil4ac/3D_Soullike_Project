using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balrond3PersonMovements
{
    public class Balrond3pCameraFollow : MonoBehaviour
    {

        [Header("Target to follow")]
        public Transform target;
        [Header("Target's height")]
        public float setTargetHeight;
        [Header("Distance")]
        public float maxDistance = 2.0f;
        public float minDistance = 1.0f;
        [Header("Zoom speed")]
        public float smooth = 10.0f;

        // Start is called before the first frame update



        // 카메라 흔들림 관련 변수들
        private bool isShaking = false;
        private float shakeIntensity;
        private float shakeDuration;
        private float shakeTimer;
        private Vector3 originalPosition;

        public void SetStart()
        {
            transform.position = target.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (isShaking)
            {
                ShakeCamera();
            }
        }

        public void ShakeCamera(float intensity, float duration)
        {
            if (!isShaking)
            {
                originalPosition = transform.position;
                shakeIntensity = intensity;
                shakeDuration = duration;
                shakeTimer = 0f;
                isShaking = true;
            }
        }

        private void ShakeCamera()
        {
            if (shakeTimer < shakeDuration)
            {
                float shakeAmount = shakeIntensity * Mathf.Pow(1f - (shakeTimer / shakeDuration), 2f);

                float offsetX = Random.Range(-shakeAmount, shakeAmount);
                float offsetY = Random.Range(-shakeAmount, shakeAmount);
                float offsetZ = Random.Range(-shakeAmount, shakeAmount);

                transform.position = originalPosition + new Vector3(offsetX, offsetY, offsetZ);

                shakeTimer += Time.deltaTime;
            }
            else
            {
                isShaking = false;
                transform.position = originalPosition;
            }
        }

        private void FixedUpdate()
        {
            transform.position = target.position;
            transform.position += new Vector3(0, setTargetHeight, 0);
        }





        //다른 스크립트에서 참조해서 쓰는법
        //Balrond3pCameraFollow cameraFollow;  <- 이거 선언하고



        //cameraFollow = FindObjectOfType<Balrond3pCameraFollow>(); <- 스타트에 이거 선언하고




        //cameraFollow.ShakeCamera(intensity, duration); <- 이 메서드를 인자로 강도랑 지속시간받아서 쓰면됨
    }
}
