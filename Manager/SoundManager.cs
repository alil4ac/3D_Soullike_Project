using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : SingleTone<SoundManager>
{
    private void Awake()
    {
        if (SoundManager.Instance != null && SoundManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
