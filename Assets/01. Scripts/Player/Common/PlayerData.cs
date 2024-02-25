using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    #region PlayerData

    [SerializeField]
    private int _Level;

    public int Level { get { return _Level; } set { _Level = value; } }

    [SerializeField]
    private float _Health;

    public float Health { get { return _Health; } set { _Health = value; } }

    [SerializeField]
    private float _MaxHealth;

    public float MaxHealth { get { return _MaxHealth; } set { _MaxHealth = value; } }

    [SerializeField]
    private float _ForcePoint;

    public float ForcePoint { get { return _ForcePoint; } set { _ForcePoint = value; } }

    [SerializeField]
    private float _MaxForcePoint;

    public float MaxForcePoint { get { return _MaxForcePoint; } set { _MaxForcePoint = value; } }

    [SerializeField]
    private float _Stamina;

    public float Stamina { get { return _Stamina; } set { _Stamina = value; } }

    [SerializeField]
    private float _MaxStamina;

    public float MaxStamina { get { return _MaxStamina; } set { _MaxStamina = value; } }

    [SerializeField]
    private int _Vta;

    public int Vta { get { return _Vta; } set { _Vta = value; } }

    [SerializeField]
    private int _Mel;

    public int Mel { get { return _Mel; } set { _Mel = value; } }

    [SerializeField]
    private int _Str;

    public int Str { get { return _Str; } set { _Str = value; } }

    [SerializeField]
    private int _Int;

    public int Int { get { return _Int; } set { _Int = value; } }

    [SerializeField]
    private int _Agi;

    public int Agi { get { return _Agi; } set { _Agi = value; } }

    [SerializeField]
    private float _Armor;

    public float Armor { get { return _Armor; } set { _Armor = value; } }

    [SerializeField]
    private int _Rune;

    public int Rune { get { return _Rune; } set { _Rune = value; } }

    #endregion

    public void LoadData(PlayerDataFile Data)
    {
        Level = Data.Level;
        MaxHealth = Data.MaxHealth;
        MaxForcePoint = Data.MaxForcePoint;
        MaxStamina = Data.MaxStamina;
        Health = Data.Health;
        ForcePoint = Data.ForcePoint;
        Stamina = Data.Stamina;
        Vta = Data.Vta;
        Mel = Data.Mel;
        Str = Data.Str;
        Int = Data.Int;
        Agi = Data.Agi;
        Armor = Data.Armor;
        Rune = Data.Rune;
    }

    public void Restart()
    {
        Health = MaxHealth;
        ForcePoint = MaxForcePoint;
        Stamina = MaxStamina;

        Rune = 0;
    }

    public void SetLevelUp(int[] statusup, int NeedRune, int stack)
    {
        Level += stack;

        Vta += statusup[0];
        Mel += statusup[1];
        Str += statusup[3];
        Int += statusup[4];
        Agi += statusup[2];

        Rune -= NeedRune;

        UIManager.Instance.GetRuneData();

        MaxHealth = Vta * 10f;
        MaxForcePoint = Mel * 3f;
        MaxStamina = Agi * 10f;
        Stamina = MaxStamina;

        UIManager.Instance.SetStatusValue();
    }
}