using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundAnimCtrl : MonoBehaviour
{
    GameSoundManager soundManager;

    private void Awake()
    {
        soundManager=GameSoundManager.Instance;
    }
   

    public void GhoulAtkSound()
    {
        soundManager.SetMonsterFx("zombie8", this.transform);
    }

    public void WolfAtkSound()
    {
        soundManager.SetMonsterFx("dog2", this.transform);
    }

    public void WereWolfAtkSound()
    {
        soundManager.SetMonsterFx("Monster9", this.transform);
    }

    public void GriffonAtkSound()
    {
        soundManager.SetMonsterFx("dinosaur 11", this.transform);
    }

    public void DemonAtkSound()
    {
        soundManager.SetMonsterFx("devil 12", this.transform);
    }

    public void DragonideAtkSound()
    {
        soundManager.SetMonsterFx("dinosaur 10", this.transform);
    }

    public void WyvernAtkSound()
    {
        soundManager.SetMonsterFx("dinosaur 9", this.transform);
    }

    public void DragonAtkSound()
    {
        soundManager.SetMonsterFx("Dragon15", this.transform);
    }


}
