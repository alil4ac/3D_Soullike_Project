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

        private bool isShaking = false;
        private float shakeIntensity;
        private float shakeDuration;
        private float shakeTimer;
        private Vector3 originalPosition;

        public void SetStart()
        {
            transform.position = target.position;
        }

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
    }
}
