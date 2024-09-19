using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemColumn
{
    public enum EItemType
    {
        Weapon = 0,
        Helmat = 1,
        TopArmor = 2,
        Gauntlet = 3,
        LegArmor = 4,
        END,
    }

    public enum EWeapon
    {
        Hammer = 0,
        LongSword = 1,
        Katana = 2,
        END,
    }

    public enum EHelamt
    {
        Knight_Heavy = 0,
        Knight_Light = 1,
        Knight_Hood = 2,
        END,
    }

    public enum ETopArmor
    {
        Knight_Heavy = 0,
        Knight_Light = 1,
        Knight_Hood= 2,
        END,
    }

    public enum EGauntlet
    {
        Knight_Heavy = 0,
        Knight_Light = 1,
        Knight_Hood = 2,
        END,
    }

    public enum ELegArmor
    {
        Knight_Heavy = 0,
        Knight_Light = 1,
        Knight_Hood = 2,
        END,
    }
}

public class ItemManager : SingleTone<ItemManager>
{
    public int SelectedIndex = 0;

    public int ItemTabIndex = 0;

    public WeaponDataTable[] WeaponData;

    public ArmorDataTable[] HelmatData;

    public ArmorDataTable[] TopArmorData;

    public ArmorDataTable[] GauntletData;

    public ArmorDataTable[] LegArmorData;

    public List<ArmorDataTable[]> ArmorDataList = new List<ArmorDataTable[]>();

    #region Mathod

    private void Init()
    {
        DontDestroyOnLoad(this.gameObject);
        ArmorDataList.Add(HelmatData);
        ArmorDataList.Add(TopArmorData);
        ArmorDataList.Add(GauntletData);
        ArmorDataList.Add(LegArmorData);
    }

    #endregion


    private void Awake()
    {
        if(ItemManager.Instance != null && ItemManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            Init();
        }
    }
}
