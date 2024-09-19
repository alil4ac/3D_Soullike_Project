using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Fx : MonoBehaviour
{
    [SerializeField]
    private AudioSource MonsterFx;

    private bool IsDone = false;

    public void SetVolume(float volume)
    {
        MonsterFx.volume = 1f * volume;
    }
    public void StartSound(string soundName, float Volume = 1f)
    {
        MonsterFx.clip = GameSoundManager.Instance.audioClips[soundName];
        StartCoroutine(_Play());
    }

    public void SoundSetting(float Volume = 1f)
    {
        MonsterFx.volume = Volume;
    }

    private IEnumerator _Play()
    {
        MonsterFx.Play();
        while (true)
        {
            if (MonsterFx.isPlaying == false)
            {
                Destroy(this.gameObject);
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
