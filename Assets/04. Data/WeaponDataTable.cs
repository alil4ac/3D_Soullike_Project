using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "WeaponDataTable", menuName = ("WeaponDataTable"), order = int.MaxValue)]
public class WeaponDataTable : ScriptableObject
{
    [SerializeField]
    private Sprite _ItemImage;

    public Sprite ItemImage { get { return _ItemImage; } }

    [SerializeField]
    private string _ItemName;

    public string ItemName { get { return _ItemName; } }

    [SerializeField]
    private string _ItemType;

    public string ItemType { get { return _ItemType; } }

    [SerializeField]
    private float _MeleeATK;

    public float MeleeATK { get { return _MeleeATK; } }

    [SerializeField]
    private float _MagicATK;

    public float MagicATK { get { return _MagicATK; } }

    [SerializeField]
    private string _BonusSTR;

    public string BonusSTR { get { return _BonusSTR; } }

    [SerializeField]
    private int _BonusSTRValue;

    public int BonusSTRValue { get { return _BonusSTRValue; } }

    [SerializeField]
    private string _BonusINT;

    [SerializeField]
    private int _BonusINTValue;

    public int BonusINTValue { get { return _BonusINTValue; } }

    public string BonusINT { get { return _BonusINT; } }

    [SerializeField]
    private float _Weapon_DMG;

    public float Weapon_DMG { get { return _Weapon_DMG; } }

    [SerializeField]
    private string _SideEffect;

    public string SideEffect { get { return _SideEffect; } }


    public float Return_MixATK(WeaponDataTable CurrentWeapon, PlayerData Status)
    {
        //float dmg = MeleeATK + MagicATK + (MeleeATK / 100 * BonusSTRValue) + (MeleeATK / 100 * BonusINTValue) + (MagicATK / 100 * BonusSTRValue) + (MagicATK / 100 * BonusINTValue) +
        //    (MeleeATK / 100 * PlayerSTR / 10) + (MeleeATK / 100 * PlayerINT / 10) + (MagicATK / 100 * PlayerSTR / 10) + (MagicATK / 100 * PlayerINT / 10);
        //return dmg;
        float ATK = CurrentWeapon.MeleeATK + CurrentWeapon.MagicATK + (CurrentWeapon.MeleeATK * 0.01f * CurrentWeapon.BonusSTRValue) +
                    (CurrentWeapon.MeleeATK * 0.01f * CurrentWeapon.BonusINTValue) + (CurrentWeapon.MagicATK * 0.01f * CurrentWeapon.BonusSTRValue) +
                    (CurrentWeapon.MagicATK * 0.01f * CurrentWeapon._BonusINTValue) + (CurrentWeapon.MeleeATK * 0.01f * (Status.Str * 0.1f)) +
                    (CurrentWeapon.MeleeATK * 0.01f * (Status.Int * 0.1f)) + (CurrentWeapon.MagicATK * 0.01f * (Status.Str * 0.1f)) +
                    (CurrentWeapon.MagicATK * 0.01f * (Status.Int * 0.1f));
        return ATK;
    }

    public float Return_MeleeATK(WeaponDataTable CurrentWeapon, PlayerData Status)
    {
        float ATK = CurrentWeapon.MeleeATK + (CurrentWeapon.MeleeATK * 0.01f * CurrentWeapon.BonusSTRValue) + (CurrentWeapon.MeleeATK * 0.01f * (Status.Str * 0.1f));
        return ATK;
    }

    public float Return_MagicATK(WeaponDataTable CurrentWeapon, PlayerData Status)
    {
        float ATK = CurrentWeapon.MagicATK + (CurrentWeapon.MagicATK * 0.01f * CurrentWeapon.BonusINTValue) + (MagicATK * 0.01f * (Status.Int * 0.1f));
        return ATK;
    }
}
