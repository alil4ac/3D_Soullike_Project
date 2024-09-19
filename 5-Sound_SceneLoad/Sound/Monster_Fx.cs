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
        //생성 시 클립의 정보 변경
        MonsterFx.clip = GameSoundManager.Instance.audioClips[soundName];
        StartCoroutine(_Play());

        //SoundSetting(Volume);
    }

    public void SoundSetting(float Volume = 1f)
    {
        MonsterFx.volume = Volume;
    }

    //생성된 클립이 재생이 완료되면 삭제
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
