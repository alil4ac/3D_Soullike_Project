using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Balrond3PersonMovements;
public class PlayerController : MonoBehaviour
{
    public Transform lowTarget;

    public Vector3 targetPos;

    private Vector3 _colSize;

    [SerializeField]
    private bool _isSlope;

    private bool _CanAttackReady = true;

    public bool CanAttackReady { get { return _CanAttackReady; } set { _CanAttackReady = value; } }

    public bool IsSlope { get { return _isSlope; } }

    [SerializeField]
    private float _slopeCheckDistance = 1f;

    [SerializeField]
    private float _downRayDistance = 0.5f;

    private float _slopeDownAngle;

    private float _slopeDownAngleOld;

    private float _slopeSideAngle;

    private Vector3 _slopeNormalPerp;

    public Vector3 SlopeNormalPerp { get { return _slopeNormalPerp; } }

    private Vector3 _hitPos;

    public Vector3 HitPos { get { return _hitPos; } }

    #region Variables

    //  Int Type

    private int StateIndex;

    public EState Index { get { return (EState)StateIndex; } }

    //  float Type

    [SerializeField]
    private float _targetingDistance = 10f;

    public float TargetingDistance { get { return _targetingDistance; } }

    // bool Type

    private bool _useInventory = false;

    public bool UseInventory { get { return _useInventory; } private set { _useInventory = value; } }

    private bool _isSmash = false;

    public bool IsSmash { get { return _isSmash; } private set { _isSmash = value; } }

    [SerializeField]
    private bool _isTarget = false;

    public bool IsTarget { get { return _isTarget; } private set { _isTarget = value; } }

    // Vector Type

    private Vector3 _targetPos;

    public Vector3 TargetPos { get { return _targetPos; } private set { _targetPos = value; } }

    // Component Type

    [SerializeField]
    private RuntimeAnimatorController[] _playerAnimator;

    private PlayerWeapon _pWeapon;

    public PlayerWeapon PWeapon { get { return _pWeapon; } private set { _pWeapon = value; } }

    private PlayerEquip _Equip;

    public PlayerEquip Equip { get { return _Equip; } private set { _Equip = value; } }

    private PlayerData _data;

    public PlayerData Data { get { return _data; } private set { Data = value; } }

    [SerializeField]
    private PhysicMaterial _fullFriction;

    public PhysicMaterial FullFirction { get { return _fullFriction; } private set { _fullFriction = value; } }

    [SerializeField]
    private PhysicMaterial _normalFriction;

    public PhysicMaterial NormalFriction { get { return _normalFriction; } private set { _normalFriction = value; } }

    private Animator _anim;
    public Animator Anim { get { return _anim; } private set { _anim = value; } }

    private CapsuleCollider _col;

    public CapsuleCollider Col { get { return _col; } private set { _col = value; } }

    private Rigidbody _rb;

    public Rigidbody RB { get { return _rb; } private set { _rb = value; } }

    private Camera _m_Cam;

    public Camera m_Cam { get { return _m_Cam; } private set { _m_Cam = value; } }

    [SerializeField]
    private PlayerDataFile _DataFile;

    public PlayerDataFile DataFile { get { return _DataFile; } private set { _DataFile = value; } }

    // Readonly Type

    private readonly int AnimatorParam = Animator.StringToHash("Param");

    #endregion

    #region Properties

    public enum EState
    {
        Movement = 0,
        Crouch = 1,
        Jump = 2,
        LAction = 3,
        RAction = 4,
        Drink = 5,
        Roll = 6,
        Buff = 7,
        Skill = 8,
        Hit = 9,
        Dead = 10,
    }

    private StateMachine<PlayerController> statemachine;

    private Dictionary<EState, IState<PlayerController>> m_state = new Dictionary<EState, IState<PlayerController>>();

    #endregion

    #region Method

    public void Return_Title()
    {
        DestroyImmediate(m_Cam.GetComponentInParent<Balrond3pCameraFollow>().gameObject);
        DestroyImmediate(this.gameObject);
    }


    public void ChangeState(EState estate)
    {
        StateIndex = (int)estate;

        _anim.SetInteger(AnimatorParam, StateIndex);

        statemachine.ChangeState(m_state[estate]);
    }

    public void RestartScene()
    {
        _anim.SetBool("IsTarget", false);

        IsTarget = false;

        lowTarget = null;

        this.gameObject.layer = LayerMask.NameToLayer("PlayerHit");

        Data.Restart();

        ChangeState(EState.Movement);
        _anim.runtimeAnimatorController = _playerAnimator[_Equip.Inventory[0][_Equip.EquipIndex[0]]];


        UIManager.Instance.SetStatusValue();

        UIManager.Instance.GetRuneData();
    }

    public void ChangeEquip(int Index)
    {
        _Equip.ChangeEquip(Index);
        if (ItemManager.Instance.SelectedIndex == (int)ItemColumn.EItemType.Weapon)
        {
            _pWeapon.ChangeWeaponn(_Equip.Inventory[0][Index]);
            _anim.runtimeAnimatorController = _playerAnimator[_Equip.Inventory[0][Index]];
            SetTarget();
            UIManager.Instance.QuickSlot_ChangeWeapon();
        }
    }

    #region Targetting

    private void SetTarget()
    {
        if (IsTarget)
        {
            Anim.SetBool("IsTarget", true);
        }
        else { Anim.SetBool("IsTarget", false); }
    }

    public void ResetTarget(Transform[] transforms)
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i] == lowTarget)
            {
                _isTarget = false;
                Anim.SetBool("IsTarget", false);
                lowTarget = null;
                m_Cam.GetComponentInParent<Balrond3pMainCamera>().IsTargetting = false;
                m_Cam.GetComponentInParent<Balrond3pMainCamera>().target_enemy = null;
            }
        }
    }

    private void SetTargeting()
    {
        if (!_isTarget)
        {


            float distance = TargetingDistance;

            foreach (Transform enemy in GameManager.Instance.TargetTransform)
            {

                if (enemy != null)
                {
                    float newdis = Vector3.Distance(enemy.transform.position, this.transform.position);
                    if (newdis < distance)
                    {
                        distance = newdis;
                        lowTarget = enemy.transform;
                    }
                }
            }
            if (lowTarget != null)
            {
                m_Cam.GetComponentInParent<Balrond3pMainCamera>().target_enemy = lowTarget;
                m_Cam.GetComponentInParent<Balrond3pMainCamera>().IsTargetting = true;
                _isTarget = true;
                _anim.SetBool("IsTarget", true);
                Debug.LogWarning("Set Targetting available");
            }
            else
            {
                _anim.SetBool("IsTarget", false);
                Debug.LogWarning("Failed Targetting");
            }
        }
        else
        {
            _isTarget = false;
            _anim.SetBool("IsTarget", false);
            lowTarget = null;
            m_Cam.GetComponentInParent<Balrond3pMainCamera>().target_enemy = null;
            m_Cam.GetComponentInParent<Balrond3pMainCamera>().IsTargetting = false;
            Debug.LogWarning("Set Targetting Disable");
        }
    }

    private void CastingTarget()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        float targetdis = Vector3.Distance(lowTarget.transform.position, _col.center);
        Vector3 targetnormal = lowTarget.transform.position - this.transform.position;
        targetnormal.Normalize();
        ray.origin = _col.center;
        ray.direction = targetnormal;

        if (!Physics.Raycast(ray.origin, ray.direction, out hit, targetdis, LayerMask.GetMask("Enemy")))
        {
            _isTarget = false;
        }
    }

    #endregion

    #region Slope

    public void CheckSlope()
    {
        Vector3 CheckPos = new Vector3(this.transform.position.x, this.transform.position.y + _col.height / 2, this.transform.position.z);

        SlopeCheckVertical(CheckPos);
    }

    private void SlopeCheckHorizontal(Vector3 CheckPos)
    {
        RaycastHit slopeHitFront;
        RaycastHit slopeHitBack;

        if (Physics.Raycast(CheckPos, transform.right, out slopeHitFront, _slopeCheckDistance, LayerMask.GetMask("Ground")))
        {
            _isSlope = true;

            _slopeSideAngle = Vector3.Angle(slopeHitFront.normal, Vector3.up);
        }
        else if (Physics.Raycast(CheckPos, -transform.right, out slopeHitBack, _slopeCheckDistance, LayerMask.GetMask("Ground")))
        {
            _isSlope = true;

            _slopeSideAngle = Vector3.Angle(slopeHitBack.normal, Vector3.up);
        }
        else
        {
            _isSlope = false;

            _slopeSideAngle = 0.0f;
        }
    }


    private void SlopeCheckVertical(Vector3 CheckPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(CheckPos, Vector3.down, out hit, _slopeCheckDistance, LayerMask.GetMask("Ground")))
        {
            //Debug.Log(hit.collider.gameObject.name);

            _slopeNormalPerp = hit.normal;

            _slopeDownAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (_slopeDownAngle != _slopeDownAngleOld)
            {
                _isSlope = true;
            }
            else if (_slopeDownAngle == _slopeDownAngleOld)
            {
                _isSlope = false;
            }

            _slopeDownAngleOld = _slopeDownAngle;

            Debug.DrawLine(CheckPos, hit.normal + CheckPos, Color.yellow, 1f);
        }
        else
        {
            Debug.DrawLine(CheckPos, (Vector3.down * _slopeCheckDistance) + CheckPos, Color.red, 1f);
        }
    }

    #endregion

    #region AnimationEvent

    public void StartNormalAttack()
    {
        _pWeapon.SetTrail();
        _pWeapon.NAtkSound();

    }

    public void EndNormalAttack()
    {
        _CanAttackReady = true;
        _pWeapon.EndAttack();
    }

    public void StartSAttack(int i)
    {
        _pWeapon.SetTrail_Arua();
        
        if (i == 0)
        {
            _pWeapon.SAtkSound();
        }
        else if (i == 2)
        {
            GameSoundManager.Instance.SetPlayerFx("sword2", _pWeapon.CurrentWeapon.transform);
        }
        else if (i == 3)
        {
            GameSoundManager.Instance.SetPlayerFx("katana3", _pWeapon.CurrentWeapon.transform);
        }
        else if (i == 4)
        {
            GameSoundManager.Instance.SetPlayerFx("katana4", _pWeapon.CurrentWeapon.transform);
        }
        else if (i == 5)
        {
            GameSoundManager.Instance.SetPlayerFx("hammer3", _pWeapon.CurrentWeapon.transform);
        }
        else if (i == 6)
        {
            GameSoundManager.Instance.SetPlayerFx("Explosion15", _pWeapon.CurrentWeapon.transform);
        }
    }

    public void UsePotion()
    {
        StartCoroutine(UsePotionValue());
        GameSoundManager.Instance.SetPlayerFx("flask-of-crimson-tears", this.transform);
        UIManager.Instance.UsePotion();
    }

    private IEnumerator UsePotionValue()
    {
        while (true)
        {
            Data.Health += 1f;

            if (Data.Health >= Data.MaxHealth)
            {
                Data.Health = Data.MaxHealth;
                yield break;
            }
            else if (Index == EState.Hit)
            {
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    #endregion

    #region Hit

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.layer == LayerMask.NameToLayer("Ignore"))
        {
            return;
        }

        else
        {

            _hitPos = other.transform.position - this.transform.position;

            _hitPos.Normalize();
            ///몬스터로부터 피격- 2023.06.23 양해인 작성
            if (other.gameObject.CompareTag("EnemyAtk"))
            {
                Data.Health -= CharacterManager.Instance.HitDamageValue(other.GetComponentInParent<Enemy>().EDamage);

                IsSmash = false;
                GameSoundManager.Instance.SetPlayerFx("hit", this.transform);

                if (Data.Health <= 0f)
                {
                    ChangeState(EState.Dead);
                }

                else if (Data.Health > 0f && (StateIndex == 0 || StateIndex == 5))
                {
                    ChangeState(EState.Hit);
                }
            }
            else if (other.gameObject.CompareTag("EnemySAtk"))
            {
                if (other.GetComponentInParent<FireBallExplosion>())
                {
                    //Data.Health -= other.GetComponent<FireBallExplosion>().FireBallDamage;

                    Data.Health -= CharacterManager.Instance.HitDamageValue(other.GetComponent<FireBallExplosion>().FireBallDamage);
                }
                else
                {
                    //Data.Health -= other.GetComponentInParent<Enemy>().ESDamage;

                    Data.Health -= CharacterManager.Instance.HitDamageValue(other.GetComponentInParent<Enemy>().ESDamage);
                }

                GameSoundManager.Instance.SetPlayerFx("hit", this.transform);

                IsSmash = true;
                if (Data.Health <= 0f)
                {
                    ChangeState(EState.Dead);
                }

                else if (Data.Health > 0f && (StateIndex == 0 || StateIndex == 5))
                {
                    ChangeState(EState.Hit);
                }
            }
            UIManager.Instance.HitPlayer();

        }
    }

    #endregion

    #region UI

    private void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && this.StateIndex == 0)
        {
            UIManager.Instance.ActiveInterfaceLayer();
        }
    }

    #endregion

    private void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetTargeting();
        }
        //if (lowTarget == null)
        //{
        //    ResetTarget();
        //}


        OpenMenu();
    }

    #endregion

    private void Init()
    {
        if (CharacterManager.Instance.Controller != null && CharacterManager.Instance.Controller != this)
        {
            DestroyImmediate(this.gameObject);
        }

        #region ComponentSet

        _data = this.GetComponent<PlayerData>();

        _Equip = this.GetComponent<PlayerEquip>();

        _pWeapon = this.GetComponent<PlayerWeapon>();

        _anim = this.GetComponent<Animator>();

        _rb = this.GetComponent<Rigidbody>();

        _col = this.GetComponent<CapsuleCollider>();

        CharacterManager.Instance.InitializedPlayer(this, _pWeapon, _Equip, _data);

        #endregion

        #region CommonSet

        _colSize = _col.center;

        #endregion

        #region StateMachineSet

        m_state.Add(EState.Movement, new Movement());

        m_state.Add(EState.LAction, new LAtcion());

        m_state.Add(EState.RAction, new RAction());

        m_state.Add(EState.Drink, new Drink());

        m_state.Add(EState.Roll, new Roll());

        m_state.Add(EState.Buff, new Buff());

        m_state.Add(EState.Hit, new PlayerHit());

        m_state.Add(EState.Dead, new PlayerDead());

        statemachine = new StateMachine<PlayerController>(this, m_state[EState.Movement]);

        #endregion

    }
    private void StartSet()
    {
        _data.LoadData(CharacterManager.Instance.DataFile);

        Equip.LoadData(CharacterManager.Instance.DataFile);

        _pWeapon.StartSet(_Equip.CallWeaponList(), _Equip.Inventory[0][_Equip.EquipIndex[0]]);

        _anim.runtimeAnimatorController = _playerAnimator[_Equip.Inventory[0][_Equip.EquipIndex[0]]];

        m_Cam = Camera.main;

        m_Cam.GetComponentInParent<Balrond3pCameraFollow>().target = this.transform;

        m_Cam.GetComponentInParent<Balrond3pCameraFollow>().SetStart();

        CameraManager.Instance.SetMainCamera();

        DontDestroyOnLoad(m_Cam.GetComponentInParent<Balrond3pCameraFollow>().gameObject);
        DontDestroyOnLoad(this.gameObject);

        UIManager.Instance.UIStart();
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        StartSet();
    }
    private void Update()
    {
        statemachine.HandleInput();

        statemachine.LogicUpdate();

        OnUpdate();
    }

    private void FixedUpdate()
    {
        statemachine.PhysicsUpdate();
    }

    private void LateUpdate()
    {
        statemachine.OnLateUpdate();
    }
}
