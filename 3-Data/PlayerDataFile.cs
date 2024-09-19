using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataFile", menuName =("PlayerDataFile"),order = int.MaxValue)]
public class PlayerDataFile : ScriptableObject
{
    #region Status

    [SerializeField]
    protected int _Level;

    public int Level { get { return _Level; }  }

    [SerializeField]
    protected float _Health;

    public float Health { get { return _Health; }  }

    [SerializeField]
    protected float _MaxHealth;
    
    public float MaxHealth { get { return _MaxHealth; }  }

    [SerializeField]
    protected float _ForcePoint;

    public float ForcePoint { get { return _ForcePoint; } }

    [SerializeField]
    protected float _MaxForcePoint;

    public float MaxForcePoint { get { return _MaxForcePoint; } }

    [SerializeField]
    protected float _Stamina;

    public float Stamina { get { return _Stamina; } }

    [SerializeField]
    protected float _MaxStamina;

    public float MaxStamina { get { return _MaxStamina; } }

    [SerializeField]
    protected int _Vta;

    public int Vta { get { return _Vta; } }

    [SerializeField]
    protected int _Mel;

    public int Mel { get { return _Mel; } }

    [SerializeField]
    protected int _Str;

    public int Str { get { return _Str; } }

    [SerializeField]
    protected int _Int;

    public int Int { get { return _Int; } }

    [SerializeField]
    protected int _Agi;

    public int Agi { get { return _Agi; } }

    [SerializeField]
    protected float _Armor;

    public float Armor { get { return _Armor; } }

    [SerializeField]
    protected int _Rune;

    public int Rune { get { return _Rune; } }

    #endregion

    #region ComponentData

    [SerializeField]
    protected Vector3 _Position;

    public Vector3 Position { get { return _Position; } }

    [SerializeField]
    protected Vector3 _Rotation;

    public Vector3 Rotation { get { return _Rotation; } }

    #endregion

    #region PlayerEquip

    [SerializeField]
    protected List<List<int>> _Inventory = new List<List<int>>();

    public List<List<int>> Inventory { get { return _Inventory; } private set { } }

    [SerializeField]
    protected int[] _EquipIndex = new int[(int)ItemColumn.EItemType.END];

    public int[] EquipIndex { get { return _EquipIndex; } private set { } }

    [SerializeField]
    protected List<int> _WeaponSlot =  new List<int>();

    public List<int> WeaponSlot { get { return _WeaponSlot; } private set { } }

    [SerializeField]
    protected List<int> _HelmatSlot = new List<int>();

    public List<int> HelmatSlot { get { return _HelmatSlot; } private set { } }

    [SerializeField]
    protected List<int> _TopArmorSlot = new List<int>();

    public List<int> TopArmorSlot { get { return _TopArmorSlot; } private set { } }

    [SerializeField]
    protected List<int> _GauntletSlot = new List<int>();

    public List<int> GauntletSlot { get { return _GauntletSlot; } private set { } }

    [SerializeField]
    protected List<int> _LegArmorSlot = new List<int>();

    public List<int> LegArmorSlot { get { return _LegArmorSlot; } private set { } }

    #endregion
}
