using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0f;

    private void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
    }

    private void Start()
    {
        originalPosition = cameraTransform.localPosition;
    }

    public void ShakeStart()
    {
        StartCoroutine(Shake());
        currentShakeDuration = shakeDuration;
    }

    public IEnumerator Shake()
    {
        while(currentShakeDuration > 0)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        cameraTransform.localPosition = originalPosition;
    }

}
