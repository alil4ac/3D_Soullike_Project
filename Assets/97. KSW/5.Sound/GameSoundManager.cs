using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : SingleTone<GameSoundManager>
{
    public float MasterVolume = 1f;

    public float FxVolume = 1f;

    public float BGMVolume = 1f;

    // 사운드를 저장할 Dictionary
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [SerializeField]
    private BackgroundAudio BackgroundMx;

    [SerializeField]
    private UI_Fx _UIFx;

    [SerializeField]
    private Player_Fx _PlayerFx;

    [SerializeField]
    private Monster_Fx _MonserFx;

    #region Method

    #region UI_Fx

    public void SelectedSound()
    {
        _UIFx.SelectedSound();
    }
    

    public void EquipSound()
    {
        _UIFx.EquipSound();
    }




    #endregion

    #region PlayerFx

    public void SetPlayerFx(string soundName, Transform SpawnPos)
    {
        Player_Fx PlayerFX = GameObject.Instantiate(_PlayerFx, SpawnPos, true);
        PlayerFX.StartSound(soundName);
    }
    public void PlayerMove()
    {
        BackgroundMx.PlayerMove();
    }

    public void PlayerStop()
    {
        BackgroundMx.PlayerStop();
    }
    public void TitleUiSound()
    {
        BackgroundMx.TitleUiSound();
    }

    public void PlayEquip()
    {
        _UIFx.EquipSound();
    }

    public void PlayClick()
    {
        _UIFx.SelectedSound();
    }

    public void PlayClick2()
    {
        _UIFx.Selected2Sound();
    }
    public void PlayLevelupSound()
    {
        _UIFx.LevelupSound();
    }

    #endregion



    #region MonsterFx

    public void SetMonsterFx(string soundName, Transform SpawnPos)
    {
        Monster_Fx MonsterFX = GameObject.Instantiate(_MonserFx, SpawnPos, true);
        MonsterFX.StartSound(soundName);
    }

    #endregion

    #endregion
    #region Volume

    // MasterVolume 조정 메서드
    public void SetMasterVolume(float volume)
    {
        MasterVolume = volume;

        // MasterVolume을 각 사운드에 반영
        BackgroundMx.SetVolume(MasterVolume);
        _UIFx.SetVolume(MasterVolume);
        _PlayerFx.SetVolume(MasterVolume);
        _MonserFx.SetVolume(MasterVolume);
        BackgroundMx.SetTitleVolume(MasterVolume);
    }

    // BGMVolume 조정 메서드
    public void SetBGMVolume(float volume)
    {
        BGMVolume = volume;

        // BGMVolume을 BackgroundMx에 반영
        BackgroundMx.SetVolume(BGMVolume);
    }

    // FXVolume 조정 메서드
    public void SetFXVolume(float volume)
    {
        FxVolume = volume;

        // FXVolume을 _UIFx, _PlayerFx, _MonsterFx에 반영
        _UIFx.SetVolume(FxVolume);
        _PlayerFx.SetVolume(FxVolume);
        _MonserFx.SetVolume(FxVolume);
        BackgroundMx.SetTitleVolume(FxVolume);
        BackgroundMx.SetRunVolume(FxVolume);
    }

    #endregion
    private void Init()
    {
        #region Init

        BackgroundMx = GameObject.Instantiate(BackgroundMx);
        DontDestroyOnLoad(BackgroundMx.gameObject);
        #endregion

        AddSound("01. Elden Ring", Resources.Load<AudioClip>("Sounds/01. Elden Ring"));
        AddSound("03. Character Creation", Resources.Load<AudioClip>("Sounds/03. Character Creation"));
        AddSound("04. Limgrave", Resources.Load<AudioClip>("Sounds/04. Limgrave"));
        AddSound("06. Tunnels", Resources.Load<AudioClip>("Sounds/06. Tunnels"));
        AddSound("13. Caelid", Resources.Load<AudioClip>("Sounds/13. Caelid"));
        AddSound("27. Elphael", Resources.Load<AudioClip>("Sounds/27. Elphael"));
        AddSound("38. Dragon", Resources.Load<AudioClip>("Sounds/38. Dragon"));
        AddSound("40. Red Wolf of Radagon", Resources.Load<AudioClip>("Sounds/40. Red Wolf of Radagon"));
        AddSound("42. Godskin Apostles", Resources.Load<AudioClip>("Sounds/42. Godskin Apostles"));
        AddSound("bonfire3", Resources.Load<AudioClip>("Sounds/bonfire3"));
        AddSound("bonfire6", Resources.Load<AudioClip>("Sounds/bonfire6"));
        AddSound("click", Resources.Load<AudioClip>("Sounds/click"));
        AddSound("devil 1", Resources.Load<AudioClip>("Sounds/devil 1"));
        AddSound("devil 4", Resources.Load<AudioClip>("Sounds/devil 4"));
        AddSound("devil 7", Resources.Load<AudioClip>("Sounds/devil 7"));
        AddSound("devil 9", Resources.Load<AudioClip>("Sounds/devil 9"));
        AddSound("devil 12", Resources.Load<AudioClip>("Sounds/devil 12"));
        AddSound("devil 14", Resources.Load<AudioClip>("Sounds/devil 14"));
        AddSound("dinosaur 1", Resources.Load<AudioClip>("Sounds/dinosaur 1"));
        AddSound("dinosaur 4", Resources.Load<AudioClip>("Sounds/dinosaur 4"));
        AddSound("dinosaur 6", Resources.Load<AudioClip>("Sounds/dinosaur 6"));
        AddSound("dinosaur 7", Resources.Load<AudioClip>("Sounds/dinosaur 7"));
        AddSound("dinosaur 9", Resources.Load<AudioClip>("Sounds/dinosaur 9"));
        AddSound("dinosaur 10", Resources.Load<AudioClip>("Sounds/dinosaur 10"));
        AddSound("dinosaur 11", Resources.Load<AudioClip>("Sounds/dinosaur 11"));
        AddSound("dinosaur 12", Resources.Load<AudioClip>("Sounds/dinosaur 12"));
        AddSound("dinosaur 14", Resources.Load<AudioClip>("Sounds/dinosaur 14"));
        AddSound("dog2", Resources.Load<AudioClip>("Sounds/dog2"));
        AddSound("dog11", Resources.Load<AudioClip>("Sounds/dog11"));
        AddSound("Dragon1", Resources.Load<AudioClip>("Sounds/Dragon1"));
        AddSound("Dragon3", Resources.Load<AudioClip>("Sounds/Dragon3"));
        AddSound("Dragon5", Resources.Load<AudioClip>("Sounds/Dragon5"));
        AddSound("Dragon8", Resources.Load<AudioClip>("Sounds/Dragon8"));
        AddSound("Dragon11", Resources.Load<AudioClip>("Sounds/Dragon11"));
        AddSound("Dragon12", Resources.Load<AudioClip>("Sounds/Dragon12"));
        AddSound("Dragon14", Resources.Load<AudioClip>("Sounds/Dragon14"));
        AddSound("Dragon15", Resources.Load<AudioClip>("Sounds/Dragon15"));
        AddSound("elden-ring-death", Resources.Load<AudioClip>("Sounds/elden-ring-death"));
        AddSound("enemy-felled", Resources.Load<AudioClip>("Sounds/enemy-felled"));
        AddSound("equip", Resources.Load<AudioClip>("Sounds/equip"));
        AddSound("Explosion12", Resources.Load<AudioClip>("Sounds/Explosion12"));
        AddSound("Explosion15", Resources.Load<AudioClip>("Sounds/Explosion15"));
        AddSound("flask-of-crimson-tears", Resources.Load<AudioClip>("Sounds/flask-of-crimson-tears"));
        AddSound("game-start-sound-effect", Resources.Load<AudioClip>("Sounds/game-start-sound-effect"));
        AddSound("hit", Resources.Load<AudioClip>("Sounds/hit"));
        AddSound("Monster1", Resources.Load<AudioClip>("Sounds/Monster"));
        AddSound("Monster9", Resources.Load<AudioClip>("Sounds/Monster9"));
        AddSound("Monster13", Resources.Load<AudioClip>("Sounds/Monster13"));
        AddSound("new-item", Resources.Load<AudioClip>("Sounds/new-item"));
        AddSound("new-location", Resources.Load<AudioClip>("Sounds/new-location"));
        AddSound("roll", Resources.Load<AudioClip>("Sounds/roll"));
        AddSound("running 1", Resources.Load<AudioClip>("Sounds/running 1"));
        AddSound("swing1", Resources.Load<AudioClip>("Sounds/swing1"));
        AddSound("swing2", Resources.Load<AudioClip>("Sounds/swing2"));
        AddSound("swing3", Resources.Load<AudioClip>("Sounds/swing3"));
        AddSound("swing4", Resources.Load<AudioClip>("Sounds/swing4"));
        AddSound("swing5", Resources.Load<AudioClip>("Sounds/swing5"));
        AddSound("sword1", Resources.Load<AudioClip>("Sounds/sword1"));
        AddSound("sword2", Resources.Load<AudioClip>("Sounds/sword2"));
        AddSound("sword3", Resources.Load<AudioClip>("Sounds/sword3"));
        AddSound("katana1", Resources.Load<AudioClip>("Sounds/katana1"));
        AddSound("katana2", Resources.Load<AudioClip>("Sounds/katana2"));
        AddSound("katana3", Resources.Load<AudioClip>("Sounds/katana3"));
        AddSound("katana4", Resources.Load<AudioClip>("Sounds/katana4"));
        AddSound("hammer1", Resources.Load<AudioClip>("Sounds/hammer1"));
        AddSound("hammer2", Resources.Load<AudioClip>("Sounds/hammer2"));
        AddSound("hammer3", Resources.Load<AudioClip>("Sounds/hammer3"));
        AddSound("Sword_Damage_01", Resources.Load<AudioClip>("Sounds/Sword_Damage_01"));
        AddSound("warming-stone-sound-effect", Resources.Load<AudioClip>("Sounds/warming-stone-sound-effect"));
        AddSound("wolf1", Resources.Load<AudioClip>("Sounds/wolf1"));
        AddSound("you-died-sound-effect", Resources.Load<AudioClip>("Sounds/you-died-sound-effect"));
        AddSound("zombie4", Resources.Load<AudioClip>("Sounds/zombie4"));
        AddSound("zombie8", Resources.Load<AudioClip>("Sounds/zombie8"));
        AddSound("zombie10", Resources.Load<AudioClip>("Sounds/zombie10"));
        AddSound("click2", Resources.Load<AudioClip>("Sounds/click2"));
        AddSound("levelup", Resources.Load<AudioClip>("Sounds/levelup"));
    }

    private void Awake()
    {
        if(GameSoundManager.Instance != null && GameSoundManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
    }

    private void Start()
    {
        //각 오디오 소스의 컴포넌트를 Manager에 연결
        _UIFx = BackgroundMx.GetComponentInChildren<UI_Fx>();


        //연결된 오디오 소스의 스크립트의 시작 세팅 함수 호출
        BackgroundMx.ManagerStart();
        _UIFx.ManagerStart();

        SetMasterVolume(1f);
        SetBGMVolume(1f);
        SetFXVolume(1f);
    }

    // 사운드 추가
    private void AddSound(string soundName, AudioClip soundClip)
    {
        if (!audioClips.ContainsKey(soundName))
        {
            audioClips.Add(soundName, soundClip);
        }
    }


    public void SetStageBGM(int SceneIndex)
    {
        BackgroundMx.SetStageBGM(SceneIndex);
    }

    public void SetLoadingBGM()
    {
        BackgroundMx.StartLoadScene();
    }


    //타이틀에서 페이드아웃이 끝나면 재생
    public void StartTitle()
    {
        BackgroundMx.StartTitle();
        BackgroundMx.VolumeUp();
    }

}