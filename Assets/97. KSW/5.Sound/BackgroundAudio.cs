using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    private AudioSource _Audio_BGM;

    [SerializeField]
    private AudioSource _Run;

    [SerializeField]
    private AudioSource _TitleUiSound;

    public AudioSource Audio_BGM { get { return _Audio_BGM; } set { _Audio_BGM = value; } }
    public void SetVolume(float volume)
    {
        _Audio_BGM.volume = 1f * volume;
    }
    public void SetTitleVolume(float volume)
    {
        _TitleUiSound.volume = 1f * volume;
    }
    public void PlayerMove()
    {
        if(_Run.isPlaying == false)
            _Run.Play();
    }

    public void SetRunVolume(float Volume)
    {
        _Run.volume = 1f * Volume;
    }

    public void PlayerStop()
    {
        _Run.Stop();
    }
    public void TitleUiSound()
    {
        _TitleUiSound.Play();
    }
    

    public void ManagerStart()
    {
        _Audio_BGM = this.GetComponent<AudioSource>();
        _Audio_BGM.Stop();
        _Audio_BGM.loop = false;
    }

    public void StartTitle()
    {
        _Audio_BGM.clip = GameSoundManager.Instance.audioClips["01. Elden Ring"];
        _Audio_BGM.loop = true;
        _Audio_BGM.Play();
    }

    public void StartLoadScene()
    {
        //로딩씬 BGM 연결, Stop() -> Play()

        VolumeDown();

        _Audio_BGM.clip = null;
    }

    public void VolumeDown()
    {
        StartCoroutine(_VolumeDown());
    }

    private IEnumerator _VolumeDown()
    {
        while(true)
        {
            _Audio_BGM.volume -= 0.1f;

            if(_Audio_BGM.volume <= 0f)
            {
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void VolumeUp()
    {
        StartCoroutine(_VolumeUp());
    }

    private IEnumerator _VolumeUp()
    {
        while (true)
        {
            _Audio_BGM.volume += 0.01f;

            if(_Audio_BGM.volume >= GameSoundManager.Instance.BGMVolume * 1f)
            {
                _Audio_BGM.volume = GameSoundManager.Instance.BGMVolume * 1f;

                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void SetStageBGM(int SceneIndex)
    {
        switch (SceneIndex)
        {
            case 1:
                _Audio_BGM.clip = GameSoundManager.Instance.audioClips["04. Limgrave"];
                VolumeUp();
                _Audio_BGM.Play();
                break;

            case 2:
                _Audio_BGM.clip = GameSoundManager.Instance.audioClips["06. Tunnels"];
                VolumeUp();
                _Audio_BGM.Play();
                break;

            case 3:
                _Audio_BGM.clip = GameSoundManager.Instance.audioClips["13. Caelid"];
                VolumeUp();
                _Audio_BGM.Play();
                break;

            case 4:
                _Audio_BGM.clip = GameSoundManager.Instance.audioClips["27. Elphael"];
                VolumeUp();
                _Audio_BGM.Play();
                break;

            case 5:
                _Audio_BGM.clip = GameSoundManager.Instance.audioClips["03. Character Creation"];
                VolumeUp();
                _Audio_BGM.Play();
                break;

        }
    }
}