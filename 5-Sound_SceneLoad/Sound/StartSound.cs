using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audio;


    private void Awake()
    {
        _audio.volume = 1f * GameSoundManager.Instance.FxVolume;
    }

    private void FixedUpdate()
    {
        if(_audio.volume != 1f * GameSoundManager.Instance.FxVolume)
        {
            _audio.volume = 1f * GameSoundManager.Instance.FxVolume;
        }
        else { return; }
    }
}
