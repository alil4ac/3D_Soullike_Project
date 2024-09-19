using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Fx : MonoBehaviour
{
    [SerializeField]
    private AudioSource Selected;

    [SerializeField]
    private AudioSource Equip;

    [SerializeField]
    private AudioSource Selected2;

    [SerializeField]
    private AudioSource LevelUp;

    public void SetVolume(float volume)
    {
        Selected.volume = volume;
        Equip.volume = volume;
        Selected2.volume = volume;
        LevelUp.volume = volume;
    }
    public void ManagerStart()
    {
        Selected.loop = false;
        Selected.playOnAwake = false;
        Selected.Stop();
        Equip.loop = false;
        Equip.playOnAwake = false;
        Equip.Stop();
        Selected2.loop = false;
        Selected2.playOnAwake = false;
        Selected2.Stop();
        LevelUp.loop = false;
        LevelUp.playOnAwake = false;
        LevelUp.Stop();


        Selected.clip = GameSoundManager.Instance.audioClips["click"];

        Equip.clip = GameSoundManager.Instance.audioClips["equip"];

        Selected2.clip = GameSoundManager.Instance.audioClips["click2"];

        LevelUp.clip = GameSoundManager.Instance.audioClips["levelup"];
    }

    public void SelectedSound()
    {
        Selected.Play();
    }

    public void EquipSound()
    {
        Equip.Play();
    }

    public void Selected2Sound()
    {
        Selected2.Play();
    }
    
    public void LevelupSound()
    {
        LevelUp.Play();
    }

}
