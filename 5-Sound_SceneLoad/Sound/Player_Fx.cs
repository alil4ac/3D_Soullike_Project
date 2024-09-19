using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fx : MonoBehaviour
{
    [SerializeField]
    private AudioSource PlayerFx;

    private bool IsDone = false;

    public void SetVolume(float volume)
    {
        PlayerFx.volume = 1f * volume;
    }
    public void StartSound(string soundName)
    {
        PlayerFx.clip = GameSoundManager.Instance.audioClips[soundName];
        StartCoroutine(_Play());
    }

    public void SoundSetting(float Volume = 1f)
    {
        PlayerFx.volume = Volume * GameSoundManager.Instance.MasterVolume * GameSoundManager.Instance.FxVolume;
    }

    private IEnumerator _Play()
    {
        PlayerFx.Play();
        while (true)
        {
            if(PlayerFx.isPlaying == false)
            {
                Destroy(this.gameObject);
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
