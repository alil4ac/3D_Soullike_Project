using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManager : SingleTone<CharacterManager>
{
    [SerializeField]
    private PlayerDataFile _DataFile;

    public PlayerDataFile DataFile { get { return _DataFile; } private set { _DataFile = value; } }

    #region PlayerResource

    [SerializeField]
    private PlayerController playerController;

    public PlayerController Controller { get { return playerController; } private set { playerController = value; } }

    [SerializeField]
    private PlayerWeapon playerWeapon;

    public PlayerWeapon Weapon { get { return playerWeapon; } private set { playerWeapon = value; } }

    [SerializeField]
    private PlayerEquip playerEquip;

    public PlayerEquip Equip { get { return playerEquip; } private set { playerEquip = value; } }

    [SerializeField]
    private PlayerData playerData;
    public PlayerData _PlayerData => playerData;

    public PlayerData Data { get { return playerData; } private set { playerData = value; } }

    #endregion

    #region Method

    public void InitializedPlayer(PlayerController controller, PlayerWeapon weapon, PlayerEquip equip, PlayerData data)
    {
        playerController = controller;

        playerWeapon = weapon;

        playerEquip = equip;

        playerData = data;
    }

    public void SetDataFile(PlayerDataFile DataSetFile)
    {
        DataFile = DataSetFile;
    }

    public void ChangeEquip(int i)
    {
        playerController.ChangeEquip(i);
        GameSoundManager.Instance.PlayEquip();
    }


    public float Return_MeleeATK()
    {
        float ATK = ItemManager.Instance.WeaponData[Equip.Inventory[0][Equip.EquipIndex[0]]].Return_MeleeATK(ItemManager.Instance.WeaponData[Equip.Inventory[0][Equip.EquipIndex[0]]], Data);

        return (Mathf.Floor(ATK * 10f) / 10f);
    }

    public float Return_MagicATK()
    {
        float ATK = ItemManager.Instance.WeaponData[Equip.Inventory[0][Equip.EquipIndex[0]]].Return_MagicATK(ItemManager.Instance.WeaponData[Equip.Inventory[0][Equip.EquipIndex[0]]], Data);

        return (Mathf.Floor(ATK * 10f) / 10f);
    }

    public float Return_MixATK()
    {
        float ATK = ItemManager.Instance.WeaponData[Equip.Inventory[0][Equip.EquipIndex[0]]].Return_MixATK(ItemManager.Instance.WeaponData[Equip.Inventory[0][Equip.EquipIndex[0]]], Data);

        return (Mathf.Floor(ATK * 10f) / 10f);
    }

    public float HitReduce()
    {
        float defCorrect = 1000;
        float hitReduce = (Data.Armor / (Data.Armor + defCorrect) * 100);
        return hitReduce;
    }

    public float HitDamageValue(float Mob_DMG)
    {
        float hitDamage = Mob_DMG * (1 - (HitReduce() / 100));
        return hitDamage;
    }

    public void SetLevelUp(int[] statusup, int NeedRune, int stack)
    {
        Data.SetLevelUp(statusup, NeedRune, stack);
    }

    public void GetItem(int itemType, int ItemIndex)
    {
        Equip.GetItem(itemType, ItemIndex);
    }

    public void GetRune(int rune)
    {
        Data.Rune+=rune;
        UIManager.Instance.GetRuneData();
    }

    public void ReleaseDeadTarget(Transform[] transforms)
    {
        Controller.ResetTarget(transforms);
    }

    public void RestartScene()
    {
        Controller.RestartScene();
    }

    public void Return_Title()
    {
        Controller.Return_Title();
        Controller = null;
        Data = null;
        Weapon = null;
        Equip = null;
    }

    #endregion

    private void Awake()
    {
        if(CharacterManager.Instance != null && CharacterManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
