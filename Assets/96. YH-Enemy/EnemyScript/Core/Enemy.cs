using FIMSpace.FLook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.HighDefinition;
using static UnityEngine.ParticleSystem;

//락온포인트 tranform 배열 찾게 awke 해주기
public class Enemy : MonoBehaviour
{
    #region 변수
    //Statement Index
    int eStateIndex;
    //read-only
    private readonly int AnimatorParam = Animator.StringToHash("Param");


    [Header("스텟")]
    [SerializeField]
    float currentHp;
    public float CurrentHP
    {
        get => currentHp;
        set
        {
            if (currentHp != value)
            {
                currentHp = value;

                if (!isDead && currentHp <= 0)
                {
                    isDead = true;
                    GameManager.Instance.ClearTargetTranform(lockOnPoints);
                    this.gameObject.layer = 30;
                    EnemyChangeState(EnemyState.OnHit);

                    //EnemyChangeState(EnemyState.Die);

                    //onHealthChange?.Invoke(hp/maxHP);
                }

                else if (isDead == false && currentHp > 0)
                {

                    isDead = false;


                    if (anim.GetCurrentAnimatorStateInfo(0).IsTag("SAtk"))
                    {
                        return;
                    }

                    else
                    {
                        EnemyChangeState(EnemyState.OnHit);
                    }

                }





                currentHp = Mathf.Clamp(currentHp, 0, maxhp);
            }
        }
    }

    [SerializeField]
    float maxhp;
    public float MaxHp => maxhp;

    [SerializeField]
    float eDamage;
    public float EDamage => eDamage;

    [SerializeField]
    float eSDamage;
    public float ESDamage => eSDamage;

    int exp;

    [SerializeField]
    EnemyData enemyData;
    public EnemyData EnemyData { get { return enemyData; } private set { enemyData = value; } }


    [Header("상태 관련")]

    [SerializeField]
    float targetDistance;
    public float Targetdistance
    {
        get { return targetDistance; }
        set
        {
            if (targetDistance != value)
            {
                targetDistance = value;
            }

            //targetDistance = Mathf.Clamp(targetDistance,0,EnemyData.SightRange);
        }
    }

    float distanceThreshold = 0.0025f; // 오차 범위 설정
    public float DistanceThreshold => distanceThreshold;

    //bool
    //bool isAttacking=false;
    //public bool IsAttacking => isAttacking;


    bool isBattle = false;
    public bool IsBattle { get { return isBattle; } private set { isBattle = value; } }


    private bool isTargeting = false;
    public bool IsTargeting { get { return isTargeting; } private set { isTargeting = value; } }

    [SerializeField]
    bool isDragon = false;
    public bool IsDragon { get { return isDragon; } private set { isDragon = value; } }

    [SerializeField]
    bool isDead = false;
    public bool IsDead => isDead;

    [SerializeField]
    bool isBoss = false;
    public bool IsBoss => isBoss;

    bool isSatk = false;
    public bool IsSatk { get { return isSatk; } set { isSatk = value; } }

    bool isJumping = false;
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }

    [Header("타이머 관련")]
    [SerializeField]
    float sAtkTimer = 0;
    public float SAtkTimer => sAtkTimer;

    float sAtkTerm = 5f;
    public float SatkTerm => sAtkTerm;

    [SerializeField]
    float jumpBackTimer = 0;
    public float JumpBackTimer => jumpBackTimer;

    [SerializeField]
    float jumpBackTerm = 5f;
    public float JumpBackTerm => jumpBackTerm;


    Vector3 targetPos;
    public Vector3 TargetPos { get { return targetPos; } private set { targetPos = value; } }

    //components
    Animator anim;
    public Animator Anim { get { return anim; } private set { anim = value; } }

    Rigidbody rb;
    public Rigidbody Rb { get { return rb; } private set { rb = value; } }




    NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } private set { agent = value; } }

    Transform chaseTarget;
    public Transform ChaseTarget { get { return chaseTarget; } private set { chaseTarget = value; } }

    private Transform[] lockOnPoints;
    public Transform[] LockOnPoints { get { return lockOnPoints; } private set { lockOnPoints = value; } }

    FLookAnimator lookAnimator;
    public FLookAnimator FLookAnimator { get { return lookAnimator; } private set { lookAnimator = value; } }

    [Header("---드래곤 관련---")]


    [SerializeField]
    GameObject FireBallPrefab;
    //[SerializeField]
    //GameObject FireChargePrefab;
    [SerializeField]
    GameObject skillSpawn;

    ParticleSystem[] breathParticles;
    ParticleSystem[] chargeParticles;
    DustEffect[] dustEffects;

    [Header("드랍아이템")]
    [SerializeField]
    GameObject dropItem;

    NormalEnemyHPUI normalEnemyHPUI;

    public enum EnemyState
    {
        Idle = 0, //대기 상태.
        Move = 1, //일단 이동(플레이어 추적 스테이트 따로 만들면 단순 이동)
        Attack = 2, //공격
        SAttack = 3, //강공격
        Die = 4, //사망
        OnHit = 5,
        FallBack = 6,
        BossBackJump = 7
    }


    private EStateMachine<Enemy> eStateMachine;

    private Dictionary<EnemyState, IEState<Enemy>> e_state = new Dictionary<EnemyState, IEState<Enemy>>();

    #endregion

    #region enemy type
    //public enum EnemyType
    //{
    //    Ghoul=0,
    //    Wolf,
    //    WereWolf,
    //    Demon,
    //    Griffon,
    //    Golem,
    //    Dragonide,
    //    Wyvern,
    //    Dragon,

    //}
    #endregion


    #region method
    public void EnemyChangeState(EnemyState enemyState)
    {
        eStateIndex = (int)enemyState;

        anim.SetInteger(AnimatorParam, eStateIndex);

        eStateMachine.EnemyChangeState(e_state[enemyState]);

        Debug.LogWarning("Enemy State Change to Next : " + enemyState);
    }

    private void SetTageting()
    {

    }


    public void OnSpawn()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        lookAnimator = GetComponent<FLookAnimator>();

        normalEnemyHPUI = this.GetComponentInChildren<NormalEnemyHPUI>();

        if (isDragon)
        {
            breathParticles = GameObject.Find("FlameThrowerTriPos").GetComponentsInChildren<ParticleSystem>();
            chargeParticles = GameObject.Find("ChargeBreathEffect").GetComponentsInChildren<ParticleSystem>();

            dustEffects = FindObjectsOfType<DustEffect>();
        }

        maxhp = enemyData.EHP;
        currentHp = maxhp;

        eDamage = enemyData.EDamage;
        eSDamage = enemyData.ESDamage;

        exp = enemyData.Exp;

        FindLockOnPoints();

        foreach (Transform lockOnPoint in lockOnPoints)
        {
            GameManager.Instance.TargetTransform.Add(lockOnPoint);
        }

        isBattle = false; //생성 시 전투상태 초기화

        #region ADDState
        e_state.Add(EnemyState.Idle, new EnemyIdle());
        e_state.Add(EnemyState.Move, new EnemyMovement());
        e_state.Add(EnemyState.Attack, new EnemyAttack());
        e_state.Add(EnemyState.SAttack, new EnemySAttack());
        e_state.Add(EnemyState.Die, new EnemyOnDeath());
        e_state.Add(EnemyState.FallBack, new EnemyFallBack());
        e_state.Add(EnemyState.OnHit, new EnemyOnHit());
        e_state.Add(EnemyState.BossBackJump, new EnemyBossBackJump());
        #endregion
        eStateMachine = new EStateMachine<Enemy>(this, e_state[EnemyState.Idle]);
    }

    #endregion

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        if (isDragon == true && SearchPlayer())
        {
            if (isSatk == false)
            {
                sAtkTimer += Time.deltaTime;

            }

            if ((Mathf.Abs(Targetdistance - EnemyData.CloseSightRange) > DistanceThreshold) && Targetdistance <= EnemyData.CloseSightRange)
            {

                if (isJumping == false)
                    jumpBackTimer += Time.deltaTime;
            }

        }



        eStateMachine.EnemyMoveInput();     //적의 이동
        eStateMachine.EnemyLogicInput();    //적의 공격



    }

    private void FixedUpdate()
    {
        eStateMachine.EnemyPhysicsUpdate(); //적의 물리
    }

    private void LateUpdate()
    {
        eStateMachine.EnemyOnLateupdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        //플레이어 공격으로부터 데미지 받음 테스트
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAtk"))  //tag를 레이어로
        {
            if (!isDead)
            {

                CurrentHP -= CharacterManager.Instance.Return_MeleeATK();
                GameSoundManager.Instance.SetMonsterFx("Sword_Damage_01", this.transform);

                if (isBoss == true)//boss
                {
                    UIManager.Instance.HitBoss(currentHp);
                }
                else
                {
                    normalEnemyHPUI.Hit(currentHp);

                }
                //Debug.LogWarning($"몬스터 {name} 의 체력 : {currentHp}\n플레이어 공격력 : {CharacterManager.Instance.Return_MeleeATK()}");
            }
        }
    }

    /// <summary>
    /// 범위 안의 플레이어 찾는 함수
    /// </summary>
    /// <returns></returns>
    public bool SearchPlayer()
    {
        bool result = false;
        chaseTarget = null;

        Collider[] collider = Physics.OverlapSphere(transform.position, enemyData.SightRange, LayerMask.GetMask("PlayerHit", "Ignore"));
        //플레이어 레이어 넘버:9


        if (collider.Length > 0)
        {
            Vector3 playerPos = collider[0].gameObject.transform.position; //플레이어 위치



            Vector3 toPlayerDir = playerPos - transform.position;

            targetDistance = toPlayerDir.magnitude;

            if (targetDistance < enemyData.CloseSightRange)
            {
                chaseTarget = collider[0].transform;
                result = true;
            }
            else
            {

                if (IsSightAngle(toPlayerDir))
                {
                    if (!IsSightBlocked(toPlayerDir))
                    {
                        SendBossInfoToUI();

                        chaseTarget = collider[0].transform;
                        result = true;
                    }
                }
            }
        }

        return result;
    }

    //시야각 조건을 만드는 함수
    bool IsSightAngle(Vector3 toTargetDir)
    {
        float angle = Vector3.Angle(transform.forward, toTargetDir);    // forward 벡터와 플레어어로 가는 방향 벡터의 사이각 구하기
        return (enemyData.SightHalfAngle > angle);
    }

    //시야 안 장애물을 판단하는 함수
    bool IsSightBlocked(Vector3 toTargetDir)
    {
        bool result = true;
        int layerMask = ~(1 << gameObject.layer);
        RaycastHit[] hits = Physics.RaycastAll(transform.position + transform.up * 0.5f, toTargetDir, enemyData.SightRange, layerMask);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player") && hit.collider.gameObject != gameObject && !IsChildCollider(hit.collider.gameObject)) //자기 자신과 자신의 자식 오브젝트를 무시
            {
                result = false;
                break;
            }
        }

        return result;
    }

    //시야 안 장애물울 구하는 함수 중 자식 오브젝트를 예외처리 하는 함수
    bool IsChildCollider(GameObject obj)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            if (obj == collider.gameObject)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// SAtk(특수 공격) 타이머 리셋
    /// </summary>
    public void ResetSTimer()
    {
        sAtkTimer = 0f;
    }

    /// <summary>
    /// (보스) 점프백 타이머 리셋
    /// </summary>
    public void ResetJumpBackTimer()
    {
        jumpBackTimer = 0f;
    }

    #region Animation event

    void DragonStartBreath()
    {
        if (isDead == false)
        {
            for (int i = 0; i < breathParticles.Length; i++)
            {
                breathParticles[i].Play();
            }
            GameSoundManager.Instance.SetMonsterFx("Dragon8", this.transform);
        }
    }

    public void DragonStopBreath()
    {
        for (int i = 0; i < breathParticles.Length; i++)
        {
            breathParticles[i].Stop();
        }
    }

    void DragonChargeBreath()
    {
        for (int i = 0; i < chargeParticles.Length; i++)
        {
            chargeParticles[i].Play();
        }
            GameSoundManager.Instance.SetMonsterFx("Dragon1", this.transform);
    }

    void PlayDustEffect()
    {
        foreach (DustEffect dustEffect in dustEffects)
        {
            dustEffect.PlayEffect();
        }
    }

    void DragonFireBall()
    {

        GameObject fireBall = Instantiate(FireBallPrefab, skillSpawn.transform.position, Quaternion.identity); // FireBallPrefab 생성
        GameSoundManager.Instance.SetMonsterFx("Dragon5", this.transform);
    }


    /// <summary>
    /// 사망 신호 송출. 전투 상태 정지용
    /// </summary>
    void SendDieSign()
    {
        if (isBoss == true && isBattle == true)
        {
            isBattle = false;
        }
        ///보스 클리어 판넬 띄울 타이밍

    }

    #endregion

    /// <summary>
    /// UI에 보스 정보 보내주는 함수
    /// </summary>
    void SendBossInfoToUI()
    {
        if (isBattle == false && isDead == false)
        {
            isBattle = true;
            Debug.LogWarning("isBattle : " + isBattle);

            if (isBoss == true)
            {
                UIManager.Instance.StartBossBattle(maxhp, this.name);

            }
            else if (isBoss == false && normalEnemyHPUI != null)
            {
                normalEnemyHPUI.StartBattle();
            }

        }
    }

    /// <summary>
    /// 사망시 실행 함수
    /// </summary>
    public void OnDie()
    {
        agent.enabled = false;
        rb.isKinematic = true;
        isBattle = false;

        CharacterManager.Instance.GetRune(this.exp);

        int dropChance = UnityEngine.Random.Range(0, 100);

        if (dropChance < 50)
        {
            GameObject dropedItem = Instantiate(dropItem, transform.position, Quaternion.identity);

        }



        if (isBoss)
        {
            GameSoundManager.Instance.SetMonsterFx("enemy-felled", this.transform);
            UIManager.Instance.BossFelled();
            GameManager.Instance.IsBossFelled = true;
        }
        //else
        //{
        //    GameSoundManager.Instance.SetMonsterFx("elden-ring-death", this.transform);
        //}

        if (isDragon == true)
        {
            if (lookAnimator != null)
            {
                lookAnimator.enabled = false;
            }

            DragonStopBreath();
        }



        Destroy(this.gameObject, 5f);
    }




    /// <summary>
    /// 보유한 락온포인트 트랜스폼 찾는 함수
    /// </summary>
    private void FindLockOnPoints()
    {
        LockOnPoint[] lockOnPointComponents = GetComponentsInChildren<LockOnPoint>(/*true*/);
        lockOnPoints = new Transform[lockOnPointComponents.Length];

        for (int i = 0; i < lockOnPointComponents.Length; i++)
        {
            lockOnPoints[i] = lockOnPointComponents[i].transform;
            //Debug.Log(this.name + " LockOnPoint[" + i + "]: " + lockOnPoints[i]);
        }

        //Debug.Log(this.name + "LockOnPoints Count: " + lockOnPoints.Length);
    }


    /// <summary>
    /// 딜레이 부여용 코루틴
    /// </summary>
    /// <returns>초</returns>
    IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(1f);
    }


    /// <summary>
    /// 디버깅 체크용 기즈모
    /// </summary>
    private void OnDrawGizmos()
    {

#if UNITY_EDITOR
        Handles.color = Color.green;        // 기본적으로 녹색
        Handles.DrawWireDisc(transform.position, transform.up, enemyData.SightRange);     // 시야 반경만큼 원 그리기

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.up, enemyData.MeleeAtkRange);     // 근접공격범위 반경만큼 원 그리기


        if (SearchPlayer()) // 플레이어가 보이는지 여부에 따라 색상 지정
        {
            Handles.color = Color.red;      // 보이면 빨간색
        }
        else
        {
            Handles.color = Color.blue;

        }
        Vector3 forward = transform.forward * enemyData.SightRange;                               // 앞쪽 방향으로 시야 범위만큼 가는 벡터
        Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // 중심선 그리기

        Quaternion q1 = Quaternion.AngleAxis(-enemyData.SightHalfAngle, transform.up);// up벡터를 축으로 반시계방향으로 sightHalfAngle만큼 회전
        Quaternion q2 = Quaternion.AngleAxis(enemyData.SightHalfAngle, transform.up); // up벡터를 축으로 시계방향으로 sightHalfAngle만큼 회전

        Handles.DrawLine(transform.position, transform.position + q1 * forward);    // 중심선을 반시계방향으로 회전시켜서 그리기
        Handles.DrawLine(transform.position, transform.position + q2 * forward);    // 중심선을 시계방향으로 회전시켜서 그리기

        Handles.DrawWireArc(transform.position, transform.up, q1 * forward, enemyData.SightHalfAngle * 2, enemyData.SightRange);  // 호 그리기



        // 근접 시야 처리
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.up, enemyData.CloseSightRange);

        //네비매쉬 스토핑디스턴스
        //Handles.color = Color.red;
        //Handles.DrawWireDisc(transform.position, transform.up, sDistance);
#endif
    }



}
