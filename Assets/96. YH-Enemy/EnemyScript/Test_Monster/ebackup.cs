using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ebackup : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Rigidbody rb;

    Animator anim;

    [SerializeField]
    float enemyHP;
    [SerializeField]
    float enemyMaxHP = 400f;

    public float EnemyHP
    {
        get => enemyHP;
        set
        {
            if (enemyHP != value)
            {
                enemyHP = value;

                if (State != States.Die && enemyHP <= 0)
                {
                    Update_Dead();
                }

                enemyHP = Mathf.Clamp(enemyHP, 0, enemyMaxHP);

            }
        }
    }

    public float EnemyMaxHP => enemyMaxHP;

    public enum States
    {
        Idle = 0,
        Move,
        Attack,
        Die
    }
    States state = States.Idle;

    Action stateUpdate;
    States State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;

                switch (state)
                {
                    case States.Idle:
                        anim.SetTrigger("isIdle");
                        stateUpdate = Update_Idle;
                        break;
                    case States.Move:
                        anim.SetTrigger("isMove");
                        stateUpdate = Update_Move;
                        break;
                    case States.Attack:
                        stateUpdate = Update_Attack;
                        break;
                    case States.Die:
                        anim.SetTrigger("isDead");
                        stateUpdate = Update_Dead;
                        break;
                    default:
                        break;
                }
            }


        }
    }


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        enemyHP = enemyMaxHP;
        state = States.Idle;
    }

    private void FixedUpdate()
    {
        stateUpdate();
    }





    void Update_Idle()
    {

    }

    void Update_Move()
    {


    }

    void Update_Attack()
    {

    }

    void Update_Dead()
    {


    }

    public void Die()
    {
        State = States.Die;

    }
}
