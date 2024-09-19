using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balrond3PersonMovements
{

    public class Balrond3personCameraCollision : MonoBehaviour
    {
        private Vector3 dollyDir;
        private Vector3 dollyDirAdjusted;
        private Balrond3pCameraFollow follow;
        private Balrond3pMainCamera cam;

        void Awake()
        {
            follow = transform.parent.parent.GetComponent<Balrond3pCameraFollow>();
            cam = transform.parent.GetComponent<Balrond3pMainCamera>();
            dollyDir = transform.parent.localPosition;
        }
        void FixedUpdate()
        {
            Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * follow.maxDistance);

            if (-follow.maxDistance < transform.localPosition.z)
            {
                transform.localPosition -= new Vector3(0, 0, follow.smooth * Time.deltaTime);
            }
        }
    }
}