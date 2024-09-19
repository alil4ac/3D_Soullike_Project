using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.IO;

public class EquipMainLayer : MonoBehaviour
{
    #region ChildComponents


    #region EquipInventory


    [SerializeField]
    private ItemSlot[] ItemSlots;

    [SerializeField]
    private InventoryTab[] InventoryTabs;

    [SerializeField]
    private Image WeaponImage;

    [SerializeField]
    private Image HelmatImage;

    [SerializeField]
    private Image TopArmorImage;

    [SerializeField]
    private Image GauntletImage;

    [SerializeField]
    private Image LegArmorImage;

    [SerializeField]
    private TextMeshProUGUI EquipText;

    #endregion

    #region ItemOption

    [SerializeField]
    private Image _ItemIcon;

    public Image ItemIcon { get { return _ItemIcon; } set { _ItemIcon = value; } }


    [SerializeField]
    private TextMeshProUGUI ItemName;

    [SerializeField]
    private TextMeshProUGUI ItemType;

    [SerializeField]
    private TextMeshProUGUI MeleeATK;

    [SerializeField]
    private TextMeshProUGUI MagicATK;

    [SerializeField]
    private TextMeshProUGUI BonusSTR;

    [SerializeField]
    private TextMeshProUGUI BonusINT;

    [SerializeField]
    private TextMeshProUGUI SideEffect;

    [SerializeField]
    private TextMeshProUGUI ATK_Armor;

    [SerializeField]
    private TextMeshProUGUI Melee_Armor;

    [SerializeField]
    private TextMeshProUGUI Magic_Armor;

    #endregion

    #region PlayerData

    [SerializeField]
    private TextMeshProUGUI Level;

    [SerializeField]
    private TextMeshProUGUI Rune;

    [SerializeField]
    private TextMeshProUGUI Vta;

    [SerializeField]
    private TextMeshProUGUI Mel;

    [SerializeField]
    private TextMeshProUGUI Str;

    [SerializeField]
    private TextMeshProUGUI Int;

    [SerializeField]
    private TextMeshProUGUI Agi;

    [SerializeField]
    private TextMeshProUGUI Health;

    [SerializeField]
    private TextMeshProUGUI ForcePoint;

    [SerializeField]
    private TextMeshProUGUI Stamina;

    [SerializeField]
    private TextMeshProUGUI Armor;

    #endregion


    #endregion

    #region Layers

    [SerializeField]
    private GameObject _EquipLayer;

    public bool ActiveEquipLayer { get { return _EquipLayer.activeInHierarchy; } }

    [SerializeField]
    private GameObject _InventoryLayer;

    public bool ActiveinventoryLayer { get { return _InventoryLayer.activeInHierarchy; } }

    #endregion

    #region Variables


    [SerializeField]
    private Sprite NullImage;

    private Color DefaultColor = new Color(0.8588235f, 0.8667861f, 0.9137255f, 1f);

    public bool IsActiveInventory { get { return _InventoryLayer.activeInHierarchy; } }

    public bool IsActiveEquipLayer { get { return _EquipLayer.activeInHierarchy; } }

    #endregion

    #region Method

    public void UIStart()
    {
        _InventoryLayer.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    #region EquipInventory

    public void ChangeInventoryTab(int Index)
    {
        if (ActiveinventoryLayer == false) { _InventoryLayer.gameObject.SetActive(true); }

        if (_EquipLayer.gameObject.activeInHierarchy) { _EquipLayer.gameObject.SetActive(false); }

        ItemManager.Instance.SelectedIndex = Index;

        for (int i = 0; i < InventoryTabs.Length; i++)
        {
            if (i != Index)
            {
                InventoryTabs[i].ChangeTab();
            }
        }
        InventoryTabs[Index].SelectedTab();

        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].DisableIcon();
        }

        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (i < CharacterManager.Instance.Equip.Inventory[Index].Count)
            {
                int val = CharacterManager.Instance.Equip.Inventory[Index][i];

                ItemSlots[i].ActiveIcon(UIManager.Instance.ItemImageResource[Index][val]);


                if (i == CharacterManager.Instance.Equip.EquipIndex[Index])
                {
                    ItemSlots[i].EquipItem();
                }
            }
            else
            {
                ItemSlots[i].DisableIcon();
            }
        }
    }

    public void SelectedItem(int Index)
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (i != Index)
            {
                ItemSlots[i].ChangeSelect();
            }
        }
    }


    public void SetEquipImage()
    {
        WeaponImage.sprite = ItemManager.Instance.WeaponData[CharacterManager.Instance.Equip.Inventory
            [(int)ItemColumn.EItemType.Weapon][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.Weapon]]].ItemImage;
        HelmatImage.sprite = ItemManager.Instance.HelmatData[CharacterManager.Instance.Equip.Inventory
            [(int)ItemColumn.EItemType.Helmat][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.Helmat]]].ItemImage;
        TopArmorImage.sprite = ItemManager.Instance.TopArmorData[CharacterManager.Instance.Equip.Inventory
            [(int)ItemColumn.EItemType.TopArmor][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.TopArmor]]].ItemImage;
        GauntletImage.sprite = ItemManager.Instance.GauntletData[CharacterManager.Instance.Equip.Inventory
            [(int)ItemColumn.EItemType.Gauntlet][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.Gauntlet]]].ItemImage;
        LegArmorImage.sprite = ItemManager.Instance.LegArmorData[CharacterManager.Instance.Equip.Inventory
            [(int)ItemColumn.EItemType.LegArmor][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.LegArmor]]].ItemImage;
    }

    public void OpenInventory()
    {
        _InventoryLayer.gameObject.SetActive(true);
        _EquipLayer.gameObject.SetActive(false);

        UIManager.Instance.IsActiveInventory = true;
        UIManager.Instance.IsActiveEquip = false;
    }

    public void CloseInventory()
    {
        _InventoryLayer.gameObject.SetActive(false);
        _EquipLayer.gameObject.SetActive(true);

        ClearOptionText();
        UIManager.Instance.IsActiveInventory = false;
        UIManager.Instance.IsActiveEquip = true;
    }

    #endregion

    #region ItemOption

    public void SetEquipItemText(int Index)
    {
        if(Index == (int)ItemColumn.EItemType.Weapon)
        {
            int val = CharacterManager.Instance.Equip.EquipIndex[Index];

            ItemName.text = ItemManager.Instance.WeaponData[val].ItemName;
            ItemType.text = ItemManager.Instance.WeaponData[val].ItemType;
            MeleeATK.text = ItemManager.Instance.WeaponData[val].MeleeATK.ToString();
            MagicATK.text = ItemManager.Instance.WeaponData[val].MagicATK.ToString();
            BonusSTR.text = ItemManager.Instance.WeaponData[val].BonusSTR;
            BonusINT.text = ItemManager.Instance.WeaponData[val].BonusINT;
            SideEffect.text = ItemManager.Instance.WeaponData[val].SideEffect;
        }
    }

    public void SetInventoryItemText(int Index)
    {
        if (ItemManager.Instance.SelectedIndex == (int)ItemColumn.EItemType.Weapon)
        {
            ATK_Armor.text = "공격력";
            Melee_Armor.text = "물리";
            Magic_Armor.text = "마법";

            int val = CharacterManager.Instance.Equip.Inventory[0][Index];
            ItemIcon.sprite = ItemManager.Instance.WeaponData[val].ItemImage;
            ItemName.text = ItemManager.Instance.WeaponData[val].ItemName;
            ItemType.text = ItemManager.Instance.WeaponData[val].ItemType;
            MeleeATK.text = ItemManager.Instance.WeaponData[val].MeleeATK.ToString();
            MagicATK.text = ItemManager.Instance.WeaponData[val].MagicATK.ToString();
            BonusSTR.text = ItemManager.Instance.WeaponData[val].BonusSTR;
            BonusINT.text = ItemManager.Instance.WeaponData[val].BonusINT;
            SideEffect.text = ItemManager.Instance.WeaponData[val].SideEffect;

            int equipval = CharacterManager.Instance.Equip.Inventory[0][CharacterManager.Instance.Equip.EquipIndex[0]];

            #region ColorChange

            #region ItemOption

            if (ItemManager.Instance.WeaponData[val].MeleeATK > ItemManager.Instance.WeaponData[equipval].MeleeATK)
            {
                MeleeATK.color = Color.green;
            }
            else if(ItemManager.Instance.WeaponData[val].MeleeATK < ItemManager.Instance.WeaponData[equipval].MeleeATK)
            {
                MeleeATK.color = Color.red;
            }
            else if(ItemManager.Instance.WeaponData[val].MeleeATK == ItemManager.Instance.WeaponData[equipval].MeleeATK)
            {
                MeleeATK.color = DefaultColor;
            }
            else { return; }

            if (ItemManager.Instance.WeaponData[val].MagicATK > ItemManager.Instance.WeaponData[equipval].MagicATK)
            {
                MagicATK.color = Color.green;
            }
            else if (ItemManager.Instance.WeaponData[val].MagicATK < ItemManager.Instance.WeaponData[equipval].MagicATK)
            {
                MagicATK.color = Color.red;
            }
            else
            {
                MagicATK.color = DefaultColor;
            }

            #endregion

            #region PlayerData

            if (ItemManager.Instance.WeaponData[val].BonusSTRValue < ItemManager.Instance.WeaponData[equipval].BonusSTRValue)
            {
                BonusSTR.color = Color.red;
            }
            else if(ItemManager.Instance.WeaponData[val].BonusSTRValue > ItemManager.Instance.WeaponData[equipval].BonusSTRValue)
            {
                BonusSTR.color = Color.green;
            }
            else
            {
                BonusSTR.color = DefaultColor;
            }

            if(ItemManager.Instance.WeaponData[val].BonusINTValue < ItemManager.Instance.WeaponData[equipval].BonusINTValue)
            {
                BonusINT.color = Color.red;
            }
            else if(ItemManager.Instance.WeaponData[val].BonusINTValue > ItemManager.Instance.WeaponData[equipval].BonusINTValue)
            {
                BonusINT.color = Color.green;
            }
            else
            {
                BonusINT.color = DefaultColor;
            }

            #endregion

            #endregion

        }

        else if(ItemManager.Instance.SelectedIndex >= (int)ItemColumn.EItemType.Helmat &&
                ItemManager.Instance.SelectedIndex <= (int)ItemColumn.EItemType.LegArmor)
        {
            ATK_Armor.text = "방어력";
            Melee_Armor.text = "방어";
            Magic_Armor.text = "";
            MagicATK.text = "";

            if(ItemManager.Instance.SelectedIndex == (int)ItemColumn.EItemType.Helmat)
            {
                int val = CharacterManager.Instance.Equip.Inventory[(int)ItemColumn.EItemType.Helmat][Index];
                int equipval = CharacterManager.Instance.Equip.Inventory[(int)ItemColumn.EItemType.Helmat][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.Helmat]];

                ItemIcon.sprite = ItemManager.Instance.HelmatData[val].ItemImage;
                ItemName.text = ItemManager.Instance.HelmatData[val].ItemName;
                ItemType.text = ItemManager.Instance.HelmatData[val].ItemType;
                MeleeATK.text = ItemManager.Instance.HelmatData[val].ArmorPoint.ToString();

                if(ItemManager.Instance.HelmatData[val].ArmorPoint > ItemManager.Instance.HelmatData[equipval].ArmorPoint)
                {
                    MeleeATK.color = Color.green;
                }
                else if(ItemManager.Instance.HelmatData[val].ArmorPoint < ItemManager.Instance.HelmatData[equipval].ArmorPoint)
                {
                    MeleeATK.color = Color.red;
                }
                else
                {
                    MeleeATK.color = DefaultColor;
                }
            }

            if (ItemManager.Instance.SelectedIndex == (int)ItemColumn.EItemType.TopArmor)
            {
                int val = CharacterManager.Instance.Equip.Inventory[(int)ItemColumn.EItemType.TopArmor][Index];
                int equipval = CharacterManager.Instance.Equip.Inventory[(int)ItemColumn.EItemType.TopArmor][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.TopArmor]];

                ItemIcon.sprite = ItemManager.Instance.TopArmorData[val].ItemImage;
                ItemName.text = ItemManager.Instance.TopArmorData[val].ItemName;
                ItemType.text = ItemManager.Instance.TopArmorData[val].ItemType;
                MeleeATK.text = ItemManager.Instance.TopArmorData[val].ArmorPoint.ToString();

                if (ItemManager.Instance.TopArmorData[val].ArmorPoint > ItemManager.Instance.TopArmorData[equipval].ArmorPoint)
                {
                    MeleeATK.color = Color.green;
                }
                else if (ItemManager.Instance.TopArmorData[val].ArmorPoint < ItemManager.Instance.TopArmorData[equipval].ArmorPoint)
                {
                    MeleeATK.color = Color.red;
                }
                else
                {
                    MeleeATK.color = DefaultColor;
                }
            }
            if (ItemManager.Instance.SelectedIndex == (int)ItemColumn.EItemType.Gauntlet)
            {
                int val = CharacterManager.Instance.Equip.Inventory[(int)ItemColumn.EItemType.Gauntlet][Index];
                int equipval = CharacterManager.Instance.Equip.Inventory[(int)ItemColumn.EItemType.Gauntlet][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.Gauntlet]];

                ItemIcon.sprite = ItemManager.Instance.GauntletData[val].ItemImage;
                ItemName.text = ItemManager.Instance.GauntletData[val].ItemName;
                ItemType.text = ItemManager.Instance.GauntletData[val].ItemType;
                MeleeATK.text = ItemManager.Instance.GauntletData[val].ArmorPoint.ToString();

                if (ItemManager.Instance.GauntletData[val].ArmorPoint > ItemManager.Instance.GauntletData[equipval].ArmorPoint)
                {
                    MeleeATK.color = Color.green;
                }
                else if (ItemManager.Instance.GauntletData[val].ArmorPoint < ItemManager.Instance.GauntletData[equipval].ArmorPoint)
                {
                    MeleeATK.color = Color.red;
                }
                else
                {
                    MeleeATK.color = DefaultColor;
                }
            }
            if (ItemManager.Instance.SelectedIndex == (int)ItemColumn.EItemType.LegArmor)
            {
                int val = CharacterManager.Instance.Equip.Inventory[(int)ItemColumn.EItemType.LegArmor][Index];
                int equipval = CharacterManager.Instance.Equip.Inventory[(int)ItemColumn.EItemType.LegArmor][CharacterManager.Instance.Equip.EquipIndex[(int)ItemColumn.EItemType.LegArmor]];

                ItemIcon.sprite = ItemManager.Instance.LegArmorData[val].ItemImage;
                ItemName.text = ItemManager.Instance.LegArmorData[val].ItemName;
                ItemType.text = ItemManager.Instance.LegArmorData[val].ItemType;
                MeleeATK.text = ItemManager.Instance.LegArmorData[val].ArmorPoint.ToString();

                if (ItemManager.Instance.LegArmorData[val].ArmorPoint > ItemManager.Instance.LegArmorData[equipval].ArmorPoint)
                {
                    MeleeATK.color = Color.green;
                }
                else if (ItemManager.Instance.LegArmorData[val].ArmorPoint < ItemManager.Instance.LegArmorData[equipval].ArmorPoint)
                {
                    MeleeATK.color = Color.red;
                }
                else
                {
                    MeleeATK.color = DefaultColor;
                }
            }
        }
    }

    public void ClearOptionText()
    {
        ATK_Armor.text = "공격력";
        Melee_Armor.text = "물리";
        Magic_Armor.text = "마법";

        ItemIcon.sprite = NullImage;
        ItemName.text = "아이템이름";
        ItemType.text = "공격타입";
        MeleeATK.text = "--";
        MagicATK.text = "--";
        BonusSTR.text = "--";
        BonusINT.text = "--";
        SideEffect.text = "--";

        Str.text = CharacterManager.Instance.Data.Str.ToString();

        Int.text = CharacterManager.Instance.Data.Int.ToString();

        #region ColorChange

        MeleeATK.color = DefaultColor;
        MagicATK.color = DefaultColor;

        BonusSTR.color = DefaultColor;
        BonusINT.color = DefaultColor;

        Str.color = DefaultColor;
        Int.color = DefaultColor;

        #endregion
    }


    #endregion

    #region PlayerData

    public void SetPlayerData()
    {
        Level.text = CharacterManager.Instance.Data.Level.ToString();

        Rune.text = CharacterManager.Instance.Data.Rune.ToString();

        Vta.text = CharacterManager.Instance.Data.Vta.ToString();

        Mel.text = CharacterManager.Instance.Data.Mel.ToString();

        Str.text = CharacterManager.Instance.Data.Str.ToString();

        Int.text = CharacterManager.Instance.Data.Int.ToString();

        Agi.text = CharacterManager.Instance.Data.Agi.ToString();

        Health.text = (Mathf.Floor(CharacterManager.Instance.Data.Health * 10f) / 10f) + " / " + (Mathf.Floor(CharacterManager.Instance.Data.MaxHealth * 10f) / 10f);

        ForcePoint.text = (Mathf.Floor(CharacterManager.Instance.Data.MaxForcePoint * 10f) / 10f) + " / " + (Mathf.Floor(CharacterManager.Instance.Data.MaxForcePoint * 10f) / 10f);

        Stamina.text = (Mathf.Floor(CharacterManager.Instance.Data.MaxStamina * 10f) / 10f).ToString();

        Armor.text = CharacterManager.Instance.Data.Armor.ToString();
    }

    #endregion

    #endregion
}
