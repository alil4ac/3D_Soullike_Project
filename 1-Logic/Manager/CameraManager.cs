using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraManager : SingleTone<CameraManager>
{
    public Camera MainCam;

    private Vector3 OriginPos;

    private void Awake()
    {
        if(CameraManager.Instance != null && CameraManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetMainCamera()
    {
        MainCam = Camera.main;
    }

    public void Return_Title()
    {
        MainCam = null;
    }


    public void ShakeDoCam(Ease ease, float duration = 0.05f, float magnitudePos = 0.03f, int vibrato = 5)
    {
        OriginPos = MainCam.transform.localPosition;

        Vector3 shakePos = Random.insideUnitSphere;

        MainCam.DOShakePosition(duration, shakePos, vibrato, magnitudePos, true).SetEase(ease);

        MainCam.transform.localPosition = OriginPos;
    }
}
