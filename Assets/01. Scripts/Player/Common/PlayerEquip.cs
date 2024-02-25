using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEquip : MonoBehaviour
{
    [SerializeField]
    private List<List<int>> _Inventory = new List<List<int>>();

    public List<List<int>> Inventory { get { return _Inventory; } private set { _Inventory = value; } }

    [SerializeField]
    private int[] _EquipIndex = new int[(int)ItemColumn.EItemType.LegArmor];

    public int[] EquipIndex { get { return _EquipIndex; } }

    private List<int> WeaponSlot = new List<int>();

    private List<int> HelmatSlot = new List<int>();

    private List<int> TopArmorSlot = new List<int>();

    private List<int> GauntletSlot = new List<int>();

    private List<int> LegArmorSlot = new List<int>();

    [SerializeField]
    private List<GameObject> Weapon = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Helmat = new List<GameObject>();

    [SerializeField]
    private List<GameObject> TopArmor = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Gauntlet = new List<GameObject>();

    [SerializeField]
    private List<GameObject> LegArmor = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Cloth = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Boots = new List<GameObject>();

    [SerializeField]
    private GameObject Hair;

    private List<List<GameObject>> _EquipTable = new List<List<GameObject>>();

    public List<List<GameObject>> EquipTable { get { return _EquipTable; } private set { _EquipTable = value; } }

    #region Method

    public List<GameObject> CallWeaponList()
    {
        return _EquipTable[0];
    }

    public void GetItem(int ItemType, int ItemIndex)
    {
        _Inventory[ItemType].Add(ItemIndex);
        GameSoundManager.Instance.SetPlayerFx("new-item", transform);
    }

    public void DropItem(int ItemType, int ItemIndex)
    {
        _Inventory[ItemType].Remove(ItemIndex);
    }

    public void ChangeEquip(int Index)
    {
        int ItemIndex = ItemManager.Instance.SelectedIndex;

        _EquipTable[ItemIndex][Inventory[ItemIndex][_EquipIndex[ItemIndex]]].SetActive(false);
        _EquipTable[ItemIndex][Inventory[ItemIndex][Index]].SetActive(true);
        _EquipIndex[ItemIndex] = Index;
        CheckCommonEquip(Inventory[ItemIndex][Index], ItemIndex);

        if (ItemIndex >= (int)ItemColumn.EItemType.Helmat && ItemIndex <= (int)ItemColumn.EItemType.LegArmor)
        {
            float armor =
                ItemManager.Instance.HelmatData[Inventory[(int)ItemColumn.EItemType.Helmat][EquipIndex[(int)ItemColumn.EItemType.Helmat]]].ArmorPoint +
                ItemManager.Instance.TopArmorData[Inventory[(int)ItemColumn.EItemType.TopArmor][EquipIndex[(int)ItemColumn.EItemType.TopArmor]]].ArmorPoint +
                ItemManager.Instance.GauntletData[Inventory[(int)ItemColumn.EItemType.Gauntlet][EquipIndex[(int)ItemColumn.EItemType.Gauntlet]]].ArmorPoint +
                ItemManager.Instance.LegArmorData[Inventory[(int)ItemColumn.EItemType.LegArmor][EquipIndex[(int)ItemColumn.EItemType.LegArmor]]].ArmorPoint;

            CharacterManager.Instance.Data.Armor = Mathf.Floor(armor * 10f) / 10f;
        }

    }

    private void CheckCommonEquip(int Index, int ItemIndex)
    {
        switch (ItemIndex)
        {
            case 0:
                break;
            case 1:
                ChangeHair(Index);
                break;
            case 2:
                ChangeCloth(Index);
                break;
            case 3:
                break;
            case 4:
                ChangeBoots(Index);
                break;
        }
    }

    private void ChangeHair(int Index)
    {
        if (Index == 1)
        {
            Hair.gameObject.SetActive(true);
        }
        else
        {
            Hair.gameObject.SetActive(false);
        }
    }

    private void ChangeCloth(int Index)
    {
        for (int i = 0; i < Cloth.Count; i++)
        {
            Cloth[i].SetActive(false);
        }
        Cloth[Index].SetActive(true);
    }

    public void ChangeBoots(int Index)
    {
        for (int i = 0; i < Boots.Count; i++)
        {
            Boots[i].SetActive(false);
        }
        Boots[Index].SetActive(true);
    }

    public void LoadData(PlayerDataFile Data)
    {
        for (int i = 0; i < Data.EquipIndex.Length; i++)
        {
            EquipIndex[i] = Data.EquipIndex[i];
        }

        for(int i = 0; i < Data.WeaponSlot.Count; i++)
        {
            WeaponSlot.Add(Data.WeaponSlot[i]);
        }

        for (int i = 0; i < Data.HelmatSlot.Count; i++)
        {
            HelmatSlot.Add(Data.HelmatSlot[i]);
        }

        for (int i = 0; i < Data.TopArmorSlot.Count; i++)
        {
            TopArmorSlot.Add(Data.TopArmorSlot[i]);
        }

        for (int i = 0; i < Data.GauntletSlot.Count; i++)
        {
            GauntletSlot.Add(Data.GauntletSlot[i]);
        }

        for (int i = 0; i < Data.LegArmorSlot.Count; i++)
        {
            LegArmorSlot.Add(Data.LegArmorSlot[i]);
        }



        //WeaponSlot = Data.WeaponSlot;

        //HelmatSlot = Data.HelmatSlot;

        //TopArmorSlot = Data.TopArmorSlot;

        //GauntletSlot = Data.GauntletSlot;

        //LegArmorSlot = Data.LegArmorSlot;

        SetInventoryList();

        SetEquipLoad();
    }


    private void SetInventoryList()
    {
        _Inventory.Clear();

        _Inventory.Add(WeaponSlot);
        _Inventory.Add(HelmatSlot);
        _Inventory.Add(TopArmorSlot);
        _Inventory.Add(GauntletSlot);
        _Inventory.Add(LegArmorSlot);

    }

    private void SetEquipLoad()
    {
        for (int i = 0; i < EquipIndex.Length; i++)
        {
            ItemManager.Instance.SelectedIndex = i;
            for (int j = 0; j < EquipTable[i].Count; j++)
            {
                EquipTable[i][j].gameObject.SetActive(false);
            }
            ChangeEquip(EquipIndex[i]);
        }
    }

    private void Init()
    {
        EquipTable.Add(Weapon);
        EquipTable.Add(Helmat);
        EquipTable.Add(TopArmor);
        EquipTable.Add(Gauntlet);
        EquipTable.Add(LegArmor);
    }

    #endregion

    private void Awake()
    {
        Init();
    }
}
