﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balrond3PersonMovements
{
    public class Balrond3pMainCamera : MonoBehaviour
    {
        private Transform target;
        private float rotationSmoothing = 0.1f;
        private float distanceToTarget;
        float velocityX = 0.0f;
        float velocityY = 0.0f;
        float rotationYAxis = 0.0f;
        float rotationXAxis = 0.0f;

        public float followSpd = 120f;
        public float zoomSpeed = 0.5f;
        public float distance = 0;
        public float minDistance = 0;
        public float maxDistance = 7;

        private Balrond3pCameraFollow follow;

        public Transform target_enemy;

        public bool IsTargetting = false;

        void Start()
        {
            follow = transform.parent.GetComponent<Balrond3pCameraFollow>();
            setBasePosition();
        }

        void FixedUpdate()
        {
            if(GameManager.Instance.IsPaused == false)
            {
                rotation();
            }
        }
        void setBasePosition()
        {
            target = transform.parent.transform;
            distanceToTarget = Vector3.Distance(target.position, transform.position) + follow.maxDistance;
            Vector3 angles = transform.eulerAngles;
            rotationYAxis = angles.y;
            rotationXAxis = angles.x;
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }
        }
        void rotation()
        {
            if (target)
            {
                if (IsTargetting&&target_enemy!=null)
                {
                    Vector3 dir = target_enemy.position - transform.position;
                    dir.Normalize();
                    velocityX += Vector3.Dot(transform.right, dir) * Time.deltaTime * followSpd;
                }
                else
                {
                    velocityX += rotationSmoothing * 60 * Input.GetAxis("Mouse X") * distanceToTarget * 10 * 0.02f;
                }

                rotationYAxis += velocityX;
                rotationXAxis -= velocityY;
                rotationXAxis = ClampAngle(rotationXAxis, -90f, 90f);
                Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
                Quaternion rotation = toRotation;
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distanceToTarget);
                Vector3 position = rotation * negDistance + (target.position);
                transform.rotation = rotation;
                transform.position = position;
                velocityX = Mathf.Lerp(velocityX, 0, rotationSmoothing * 20);
                velocityY = Mathf.Lerp(velocityY, 0, rotationSmoothing * 20);
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
