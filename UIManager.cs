using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : SingleTone<UIManager>
{
    [SerializeField]
    private Canvas UI_Canvas;


    public bool LoadDone = false;

    #region IsActiveCheck

    private bool _IsActiveMenuLayer;

    public bool IsActiveMenuLayer { get { return _IsActiveMenuLayer; } set { _IsActiveMenuLayer = value; } }

    private bool _IsActiveEquipLayer;

    public bool IsActiveEquipLayer { get { return _IsActiveEquipLayer; } set { _IsActiveEquipLayer = value; } }

    private bool _IsActiveEquip;

    public bool IsActiveEquip { get { return _IsActiveEquip; } set { _IsActiveEquip = value; } }

    private bool _IsActiveInventory;

    public bool IsActiveInventory { get { return _IsActiveInventory; } set { _IsActiveInventory = value; } }


    private bool _IsActiveStatusLayer;

    public bool IsActiveStatusLayer { get { return _IsActiveStatusLayer; } set { _IsActiveStatusLayer = value; } }

    private bool _IsActiveLevelUpLayer;

    public bool IsActiveLevelUpLayer { get { return _IsActiveLevelUpLayer; } set { _IsActiveLevelUpLayer = value; } }

    private bool _IsActiveInteraction;

    public bool IsActiveInteraction { get { return _IsActiveInteraction; } set { _IsActiveInteraction = value; } }

    private bool _IsActivePickItem;

    public bool IsActivePickItem { get { return _IsActivePickItem; } set { _IsActivePickItem = value; } }

    private bool _IsActiveSystemLayer;

    public bool IsActiveSystemLayer { get { return _IsActiveSystemLayer; } set { _IsActiveSystemLayer = value; } }


    #endregion


    #region UIResources

    #region InGameResource

    [SerializeField]
    private InterfaceLayer _InterfaceLayer;

    [SerializeField]
    private PlayerStatusBar _PlayerStatusBar;

    [SerializeField]
    private BossHP BossHPSlider;

    [SerializeField]
    private Interaction _Interaction;

    [SerializeField]
    private PickItem _PickItem;

    [SerializeField]
    private YouDie _YouDie;


    [SerializeField]
    private InGameFadeLayer _InGameFadeLayer;

    public bool IsActiveFadeLayer
    {
        get
        {
            if (_InGameFadeLayer == null)
            {
                return false;
            }
            else if (_InGameFadeLayer != null)
            {
                return true;
            }
            else { return false; }
        }
    }

    #region QuickSlots

    [SerializeField]
    private QuickSlot _QuickSlots;


    #endregion

    #endregion

    #region MenuResource

    [SerializeField]
    private MenuLayer _MenuLayer;

    #endregion

    #region InventoryResource

    [SerializeField]
    private EquipMainLayer _EquipMainLayer;

    #endregion

    #region StatusResource

    [SerializeField]
    private StatusLayer _StatusLayer;

    #endregion

    #region LevelUp

    [SerializeField]
    private LevelUpLayer _LevelUpLayer;

    #endregion

    #region System

    [SerializeField]
    private SystemLayer _SystemLayer;

    #endregion

    #region ImageResource

    [SerializeField]
    private Image LoreImage;

    [SerializeField]
    private List<Sprite> WeaponImage;

    [SerializeField]
    private List<Sprite> HelmatImage;

    [SerializeField]
    private List<Sprite> TopArmorImage;

    [SerializeField]
    private List<Sprite> GauntletImage;

    [SerializeField]
    private List<Sprite> LegArmorImage;

    private Dictionary<int, List<Sprite>> _ItemImageResource = new Dictionary<int, List<Sprite>>();

    public Dictionary<int, List<Sprite>> ItemImageResource { get { return _ItemImageResource; } private set { } }

    #endregion

    #endregion

    #region Method


    public void ActiveInterfaceLayer()
    {
        if (GameManager.Instance.IsPaused == false && !IsActiveMenuLayer)
        {
            OpenMenuLayer();
            GameManager.Instance.IsPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else if (_EquipMainLayer.IsActiveInventory && !_EquipMainLayer.IsActiveEquipLayer)
        {
            CloseInventory();
        }

        else if (!_EquipMainLayer.IsActiveInventory && _EquipMainLayer.IsActiveEquipLayer && !IsActiveMenuLayer)
        {
            CloseEquip();
            OpenMenuLayer();
        }

        else if (!IsActiveMenuLayer && IsActiveStatusLayer)
        {
            CloseStatus();
            OpenMenuLayer();
        }

        else if (!IsActiveMenuLayer && IsActiveLevelUpLayer)
        {
            CloseLevelUpLayer();
            OpenMenuLayer();
        }

        else if(!IsActiveMenuLayer && IsActiveSystemLayer)
        {
            CloseSystemLayer();
            OpenMenuLayer();
        }

        else if (IsActiveMenuLayer && !IsActiveLevelUpLayer && !IsActiveStatusLayer && !IsActiveEquipLayer && !IsActiveSystemLayer)
        {
            CloseMenuLayer();
            GameManager.Instance.IsPaused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            return;
        }
    }

    #region MenuLayer

    public void OpenMenuLayer()
    {
        _MenuLayer.gameObject.SetActive(true);
        _MenuLayer.OpenMenu();
    }

    public void CloseMenuLayer()
    {
        _MenuLayer.CloseMenu();
    }

    public void SelectedMenu(int Index)
    {
        switch (Index)
        {
            case 0:
                if (IsActiveEquipLayer == false)
                {
                    CloseMenuLayer();
                    OpenEquip();
                }
                break;
            case 1:
                if (IsActiveStatusLayer == false)
                {
                    CloseMenuLayer();
                    OpenStatus();
                }
                break;

            case 2:
                if (IsActiveLevelUpLayer == false)
                {
                    CloseMenuLayer();
                    OpenLevelUpLayer();
                }
                break;

            case 3:
                if(IsActiveSystemLayer == false)
                {
                    CloseMenuLayer();
                    OpenSystemLayer();
                }
                break;
        }
    }

    #endregion

    #region EquipLayer

    #region EquipInventory

    public void ChangeInvenTab(int Index)
    {
        _EquipMainLayer.ChangeInventoryTab(Index);
    }

    public void SelectItem(int Index)
    {
        _EquipMainLayer.SelectedItem(Index);
    }

    public void OpenEquip()
    {
        _EquipMainLayer.gameObject.SetActive(true);
        _EquipMainLayer.SetEquipImage();
        _EquipMainLayer.SetPlayerData();
        IsActiveEquipLayer = true;
    }

    public void CloseEquip()
    {
        _EquipMainLayer.gameObject.SetActive(false);
        IsActiveEquipLayer = false;
    }

    public void OpenInventory()
    {
        _EquipMainLayer.OpenInventory();
    }

    public void CloseInventory()
    {
        _EquipMainLayer.CloseInventory();
        _EquipMainLayer.SetEquipImage();
    }

    #endregion

    #region ItemOption

    public void SetInventoryItemText(int Index)
    {
        _EquipMainLayer.SetInventoryItemText(Index);
    }

    public void ClearOptionText()
    {
        _EquipMainLayer.ClearOptionText();
    }

    #endregion

    #endregion

    #region Status

    public void OpenStatus()
    {
        _StatusLayer.gameObject.SetActive(true);
        _StatusLayer.OpenStstus();
    }

    public void CloseStatus()
    {
        _StatusLayer.CloseStatus();
    }

    #endregion

    #region LevelUp

    public void OpenLevelUpLayer()
    {
        _LevelUpLayer.gameObject.SetActive(true);
        _LevelUpLayer.OpenLevelLayer();
    }

    public void CloseLevelUpLayer()
    {
        _LevelUpLayer.CloseLevelLayer();
    }

    public void SetLevelStackUp(int Index)
    {
        _LevelUpLayer.StstusUpStack(Index);
    }

    public void SetLevelStackDown(int Index)
    {
        _LevelUpLayer.StatusDownStack(Index);
    }

    public void SelectedLevelUp()
    {
        _LevelUpLayer.StatusUpCompleate();
    }

    #endregion

    #region System

    public void OpenSystemLayer()
    {
        _SystemLayer.gameObject.SetActive(true);
        _SystemLayer.OpenSystemLayer();
    }

    public void CloseSystemLayer()
    {
        _SystemLayer.CloseSystemLayer();
    }

    public void SelectSystemButton(int i)
    {
        switch (i)
        {
            case 0:
                CloseSystemLayer();
                InGameFadeIn(0);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case 3:
                InGameFadeIn(0);
                Invoke(nameof(Quit), 1f);
                break;
        }
    }

    private void Quit()
    {

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else

        Application.Quit();

#endif

    }

#endregion

#region InGameLayer

#region StatusBar

    public void HitPlayer()
    {
        _PlayerStatusBar.HitHP();
    }

    public void UsePotion()
    {
        _PlayerStatusBar.UsePotion();
    }

    public void SetStamina()
    {
        _PlayerStatusBar.UseStamina();
    }

    public void SetStatusValue()
    {
        _PlayerStatusBar.LoadData();
    }

#endregion

#region BossHPBar

    public void StartBossBattle(float MaxHealth, string name)
    {
        BossHPSlider.gameObject.SetActive(true);

        BossHPSlider.StartBossBattle(MaxHealth, name);
    }

    public void EndBossBattle()
    {
        BossHPSlider.EndBossValue();
    }

    public void HitBoss(float health)
    {
        BossHPSlider.HitBoss(health);
    }

#endregion

#region QuickSlot

    public void QuickSlot_ChangeWeapon()
    {
        _QuickSlots.ChangeWeapon();
    }

#endregion

#region exp(rune)
    public void GetRuneData()
    {
        _InterfaceLayer.SetRune();
    }
#endregion

#region PickItem

    // E 줍기 인터페이스 활성화 -> 이미 실행중이면 호출 예외처리
    public void ActivePickItem()
    {
        if (IsActivePickItem == true)
        {
            return;
        }
        else if (IsActiveInteraction == true)
        {
            return;
        }
        else
        {
            _PickItem.gameObject.SetActive(true);
            _PickItem.ActivePickItem();
        }
    }

    // E 줍기 인터페이스 종료
    public void DisablePickItem()
    {
        _PickItem.DisablePickItem();
    }

    public void OpenPickUpItemLayer(int ItemType, int ItemIndex)
    {
        _Interaction.gameObject.SetActive(true);
        _Interaction.OpenTab(ItemType, ItemIndex);
    }

#endregion

#region YouDie

    public void PlayerDie()
    {
        _YouDie.gameObject.SetActive(true);
        _YouDie.PlayerDie();
    }

    public void BossFelled()
    {
        _YouDie.gameObject.SetActive(true);
        _YouDie.EnemyFelled();
    }

#endregion

#region InGameFade

    public void InGameFadeIn(int SceneIndex)
    {
        _InGameFadeLayer.FadeIn(SceneIndex);
    }

    public void InGameFadeOut()
    {
        _InGameFadeLayer.FadeOut();
    }

    public void IntoLoad()
    {
        _InGameFadeLayer.IntoLoad();
    }

#endregion

#endregion

#endregion

    private bool IsDone = false;

    private void Awake()
    {
        if (UIManager.Instance != null && UIManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            Init();
        }
    }

    private void Start()
    {

    }

    private void Init()
    {
        DontDestroyOnLoad(this.gameObject);
        _ItemImageResource.Add((int)ItemColumn.EItemType.Weapon, WeaponImage);
        _ItemImageResource.Add((int)ItemColumn.EItemType.Helmat, HelmatImage);
        _ItemImageResource.Add((int)ItemColumn.EItemType.TopArmor, TopArmorImage);
        _ItemImageResource.Add((int)ItemColumn.EItemType.Gauntlet, GauntletImage);
        _ItemImageResource.Add((int)ItemColumn.EItemType.LegArmor, LegArmorImage);

        UI_Canvas = GameObject.Instantiate(UI_Canvas);
        DontDestroyOnLoad(UI_Canvas);
    }

    public void UIStart()
    {
        _EquipMainLayer = UI_Canvas.GetComponentInChildren<EquipMainLayer>();
        _PlayerStatusBar = UI_Canvas.GetComponentInChildren<PlayerStatusBar>();
        _MenuLayer = UI_Canvas.GetComponentInChildren<MenuLayer>();
        _StatusLayer = UI_Canvas.GetComponentInChildren<StatusLayer>();
        BossHPSlider = UI_Canvas.GetComponentInChildren<BossHP>();
        _InterfaceLayer = UI_Canvas.GetComponentInChildren<InterfaceLayer>();
        _Interaction = UI_Canvas.GetComponentInChildren<Interaction>();
        _QuickSlots = UI_Canvas.GetComponentInChildren<QuickSlot>();
        _InGameFadeLayer = UI_Canvas.GetComponentInChildren<InGameFadeLayer>();
        _LevelUpLayer = UI_Canvas.GetComponentInChildren<LevelUpLayer>();
        _PickItem = UI_Canvas.GetComponentInChildren<PickItem>();
        _YouDie = UI_Canvas.GetComponentInChildren<YouDie>();
        _SystemLayer = UI_Canvas.GetComponentInChildren<SystemLayer>();
        _EquipMainLayer.UIStart();
        _PlayerStatusBar.LoadData();
        _MenuLayer.UIStart();
        BossHPSlider.UIStart();
        _StatusLayer.UIStart();
        _InterfaceLayer.UIStart();
        _Interaction.UIStart();
        _QuickSlots.UIStart();
        _LevelUpLayer.UIStart();
        _PickItem.UIStart();
        _SystemLayer.UIStart();
        _InGameFadeLayer.UIStart();
        _YouDie.UIStart();
        InGameFadeOut();

        LoadDone = true;
    }

    public void Return_Title()
    {
        DestroyImmediate(UI_Canvas.gameObject);
        DestroyImmediate(this.gameObject);
    }

}
