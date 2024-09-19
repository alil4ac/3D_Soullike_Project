using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject _currentWeapon;

    public GameObject CurrentWeapon { get { return _currentWeapon; } set { _currentWeapon = value; } }

    [SerializeField]
    private EffectHolders _effectHolder;

    public EffectHolders EffectHolder { get { return _effectHolder; } private set { _effectHolder = value; } }

    [SerializeField]
    private GameObject WeaponHolder_R;

    [SerializeField]
    private GameObject _skillPrefab;

    public GameObject SkilPrefab { get { return _skillPrefab; } set { _skillPrefab = value; } }

    [SerializeField]
    private PSMeshRendererUpdater _aruaPrefab;

    public PSMeshRendererUpdater AruaPrefab { get { return _aruaPrefab; } set { _aruaPrefab = value; } }

    [SerializeField]
    private XftWeapon.XWeaponTrail _trail;

    public XftWeapon.XWeaponTrail WeaponTrail { get { return _trail; } set { _trail = value; } }

    #region WeaponList

    [SerializeField]
    private List<GameObject> _weaponList;

    public List<GameObject> WeaponList { get { return _weaponList; } private set { _weaponList = value; } }

    #endregion

    #endregion

    #region Proterties

    private Collider _weaponCol;

    public Collider WeaponCol { get { return _weaponCol; } set { _weaponCol = value; } }

    #endregion

    #region State

    private WeaponState<PlayerWeapon> weaponstate;

    private Dictionary<int, IWeapon<PlayerWeapon>> m_weapon = new Dictionary<int, IWeapon<PlayerWeapon>>();

    #endregion

    #region Method

    public void ChangeWeaponn(int Index)
    {
        weaponstate.ChangeWeapon(m_weapon[Index]);
    }

    public void StartAttack()
    {
        weaponstate.StartAttack();
    }

    public void EndAttack()
    {
        weaponstate.EndAttack();
    }

    public void WeaponSkill()
    {
        weaponstate.WeaponSkill();
    }

    public void WeaponArua()
    {
        weaponstate.WeaponArua();
    }

    public void SetTrail()
    {
        weaponstate.SetTrail();
        weaponstate.StartAttack();
    }

    public void SetTrail_Arua()
    {
        weaponstate.SetTrail();
        weaponstate.WeaponArua();
        weaponstate.StartAttack();
    }

    public void NAtkSound()
    {
        weaponstate.NAtkSound();
    }

    public void SAtkSound()
    {
        weaponstate.SAtkSound();
    }

    #endregion

    private void Awake()
    {
        m_weapon.Add((int)ItemColumn.EWeapon.Hammer, new Hammer());

        m_weapon.Add((int)ItemColumn.EWeapon.LongSword, new LongSword());

        m_weapon.Add((int)ItemColumn.EWeapon.Katana, new Katana());

        EffectHolder = GameObject.Instantiate(EffectHolder);

        DontDestroyOnLoad(EffectHolder.gameObject);
    }

    public void SetWeaponList(List<GameObject> list)
    {
        _weaponList = list;
    }

    public void StartSet(List<GameObject> list, int Index)
    {
        _weaponList = list;

        weaponstate = new WeaponState<PlayerWeapon>(this, m_weapon[Index]);
    }

    private void Start()
    {

    }
}
