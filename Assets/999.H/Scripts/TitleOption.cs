using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class TitleOption : MonoBehaviour
{
    #region Component Type

    [SerializeField]
    private GameObject OptionPanel;

    [SerializeField]
    private GameObject SliderList;

    private Slider[] VolumeSlider;

    [SerializeField]
    private TMP_Dropdown ScreenSize;

    [SerializeField]
    private GameObject ButtonList;

    private Toggle[] MuteButton;

    [SerializeField]
    private GameObject OptionTextList;

    private TextMeshProUGUI[] OptionText;

    private bool FullScreenOn = true;

    private bool _IsActived = false;

    public bool IsActived { get { return _IsActived; } }

    #endregion

    #region Methods
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider fxVolumeSlider;

    private void Start()
    {
        // Slider 초기값 설정
        masterVolumeSlider.value = GameSoundManager.Instance.MasterVolume;
        bgmVolumeSlider.value = GameSoundManager.Instance.BGMVolume;
        fxVolumeSlider.value = GameSoundManager.Instance.FxVolume;

        // Slider에 이벤트 연결
        masterVolumeSlider.onValueChanged.AddListener(GameSoundManager.Instance.SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(GameSoundManager.Instance.SetBGMVolume);
        fxVolumeSlider.onValueChanged.AddListener(GameSoundManager.Instance.SetFXVolume);
    }
    private void Init()
    {
        MuteButton = ButtonList.GetComponentsInChildren<Toggle>();
        VolumeSlider = SliderList.GetComponentsInChildren<Slider>();
        OptionText = OptionTextList.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (Toggle btn in MuteButton)
        {
            btn.image.color = new Color(1, 1, 1, 0);
        }
        foreach(Slider slider in VolumeSlider)
        {
            Image[] img = slider.GetComponentsInChildren<Image>();
            foreach(Image imm in img)
            {
                imm.color = new Color(1, 1, 1, 0);
            }
        }
        foreach(TextMeshProUGUI Txt in OptionText)
        {
            Txt.color = new Color(1, 1, 1, 0);
        }
        OptionPanel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        OptionPanel.SetActive(false);
    }

    public void SetActiveOption()
    {
        OptionPanel.SetActive(true);
        _SetActiveOption();
    }

    private void _SetActiveOption()
    {
        OptionPanel.GetComponent<Image>().DOFade(0.863f, 1f);
        foreach(Toggle tg in MuteButton)
        {
            Image[] img = tg.GetComponentsInChildren<Image>();
            foreach(Image imm in img)
            {
                imm.DOFade(1f, 1f);
            }
        }
        foreach (Slider slider in VolumeSlider)
        {
            Image[] img = slider.GetComponentsInChildren<Image>();
            foreach (Image imm in img)
            {
                imm.DOFade(1f, 1f);
            }
        }
        foreach (TextMeshProUGUI Txt in OptionText)
        {
            Txt.DOColor(new Color(1f, 1f, 1f), 1f);
        }
        ScreenSize.image.DOFade(1f, 1f);
        ScreenSize.GetComponentInChildren<TextMeshProUGUI>().DOFade(1f, 1f);
    }

    public void SetDiableOption()
    {
        _SetDiableOption();
    }

    private void _SetDiableOption()
    {
        OptionPanel.GetComponent<Image>().DOFade(0, 1f);
        foreach (Toggle tg in MuteButton)
        {
            Image[] img = tg.GetComponentsInChildren<Image>();
            foreach (Image imm in img)
            {
                imm.DOFade(0f, 1f);
            }
        }
        foreach (Slider slider in VolumeSlider)
        {
            Image[] img = slider.GetComponentsInChildren<Image>();
            foreach (Image imm in img)
            {
                imm.DOFade(0, 1f);
            }
        }
        foreach (TextMeshProUGUI Txt in OptionText)
        {
            Txt.DOFade(0, 1f);
        }
        ScreenSize.image.DOFade(0, 1f);
        ScreenSize.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 1f);
    }

    public void VolumMute(int Index)
    {
        if (_IsActived == false)
            return;

        switch (Index)
        {
            case 0:
                Debug.Log("MMasterVolum " + MuteButton[Index].isOn);
                GameSoundManager.Instance.SetMasterVolume(MuteButton[Index].isOn ? 0f : 1f);
                break;
            case 1:
                Debug.Log("BGMVolum " + MuteButton[Index].isOn);
                GameSoundManager.Instance.SetBGMVolume(MuteButton[Index].isOn ? 0f : 1f);
                break;
            case 2:
                Debug.Log("FXVolum " + MuteButton[Index].isOn);
                GameSoundManager.Instance.SetFXVolume(MuteButton[Index].isOn ? 0f : 1f);
                break;
            case 3:
                FullScreenOn = !FullScreenOn;
                if (FullScreenOn)
                {
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                }
                else
                {
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                }
                break;
        }
    }

    public void ScreenClick(int index)
    {
        if (_IsActived == false)
            return;

        if (index == 0)
        {
            Screen.SetResolution(1920, 1080, FullScreenOn);
        }
        if (index == 1)
        {
            Screen.SetResolution(1600, 900, FullScreenOn);
        }
        if (index == 2)
        {
            Screen.SetResolution(1280, 720, FullScreenOn);
        }
    }

    #endregion

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if(IsActived && Input.GetKeyDown(KeyCode.Escape))
        {
            SetDiableOption();
        }

        if(OptionPanel.gameObject.activeInHierarchy && !IsActived)
        {
            if (OptionPanel.GetComponent<Image>().color.a >= 0.8f)
            {
                _IsActived = true;
                Debug.Log("IsDone");
            }
        }
        else if(OptionPanel.gameObject.activeInHierarchy && IsActived)
        {
            if (OptionPanel.GetComponent<Image>().color.a == 0f)
            {
                _IsActived = false;
                this.GetComponent<Title>().ReturnMain();
                OptionPanel.SetActive(false);
            }
        }

    }
}
