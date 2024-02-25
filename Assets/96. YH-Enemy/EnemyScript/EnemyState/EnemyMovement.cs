using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : IEState<Enemy>
{


    //적 이동 구현

    /// <summary>
    /// 스테이트 진입 시 한번만 실행
    /// </summary>
    /// <param name="Send"></param>
    public void Enter(Enemy Send)
    {
        Send.Agent.isStopped = false;
        

        if (Send.IsDragon && Send.FLookAnimator != null)
        {
            Send.FLookAnimator.enabled = true;
        }
        else
        {

        }
    }

    /// <summary>
    /// 키입력 등의 로직 실행 (Update)
    /// </summary>
    /// <param name="Send"></param>
    public void EnemyMoveInput(Enemy Send)
    {


    }

    /// <summary>
    /// 스테이트 전환 및 행동조건 로직 실행 (Update)
    /// </summary>
    /// <param name="Send"></param>
    public void EnemyLogicUpdate(Enemy Send)
    {
        
    }

    /// <summary>
    /// 실제 행동 밸류 설정 (FixedUpdate)
    /// </summary>
    /// <param name="Send"></param>
    public void EnemyPhysicsUpdate(Enemy Send)
    {
        if (Send.SearchPlayer()&&!Send.Agent.pathPending)
        {
            
            if (Send.ChaseTarget != null )
            {

                Send.Agent.isStopped = false;
                Send.Agent.stoppingDistance = Send.EnemyData.MeleeAtkRange;
                Send.Agent.SetDestination(Send.ChaseTarget.position);
                //Debug.Log("이동중");

                if (Send.Targetdistance <= Send.EnemyData.MeleeAtkRange&& (Mathf.Abs(Send.Targetdistance - Send.EnemyData.MeleeAtkRange) > Send.DistanceThreshold))
                {

                    //Send.Agent.isStopped = true;
                    Send.Agent.velocity = Vector3.zero;
                    Send.EnemyChangeState(Enemy.EnemyState.Idle);
                    Debug.Log("EnemyMove : 접근->대기. Send.Targetdistance <= Send.EnemyData.MeleeAtkRange");
                }


                else if (Send.IsDragon == true && Send.Targetdistance>Send.EnemyData.MeleeAtkRange)
                {

                    if (Send.SAtkTimer > Send.SatkTerm)
                    {
                        Send.Agent.isStopped = true;
                        Send.Agent.velocity = Vector3.zero;
                        Send.EnemyChangeState(Enemy.EnemyState.SAttack);
                        Debug.LogWarning("SAttack");
                    }
                    
                }
                else
                {
                    return;
                }

            }


           

        }

        else
        {
            //Send.Agent.isStopped = true;
            Send.Agent.velocity = Vector3.zero;

            Send.EnemyChangeState(Enemy.EnemyState.Idle);
        }

    }

    /// <summary>
    /// LateUpdate 필요시 설정 (LateUpdate)
    /// </summary>
    /// <param name="Send"></param>
    public void EnemyOnLateUpdate(Enemy Send)
    {

    }



    /// <summary>
    /// 스테이트 종료 시 한번만 실행
    /// </summary>
    /// <param name="Send"></param>
    public void Exit(Enemy Send)
    {
        //Send.Agent.isStopped = true;
        
    }
}

////적 이동 구현

///// <summary>
///// 스테이트 진입 시 한번만 실행
///// </summary>
///// <param name="Send"></param>
//public void Enter(Enemy Send)
//{

//}

///// <summary>
///// 키입력 등의 로직 실행 (Update)
///// </summary>
///// <param name="Send"></param>
//public void EnemyMoveInput(Enemy Send)
//{


//}

///// <summary>
///// 스테이트 전환 및 행동조건 로직 실행 (Update)
///// </summary>
///// <param name="Send"></param>
//public void EnemyLogicUpdate(Enemy Send)
//{
//    if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("walk")) //walk를 태그로
//    {
//        if (Send.ChaseTarget == null)
//        {
//            Send.Agent.isStopped = true;
//            Send.Agent.velocity = Vector3.zero;

//            Send.EnemyChangeState(Enemy.EnemyState.Idle);
//            Debug.Log("EnemyMove : 접근->대기. 적 범위 밖");

//        }
//        else if (Send.ChaseTarget != null && Send.Targetdistance <= Send.EnemyData.CloseSightRange)
//        {

//            Send.Agent.isStopped = true;
//            Send.Agent.velocity = Vector3.zero;
//            Send.EnemyChangeState(Enemy.EnemyState.Idle);
//            Debug.Log("EnemyMove : 접근->대기. 적 감지");

//        }



//    }
//}

///// <summary>
///// 실제 행동 밸류 설정 (FixedUpdate)
///// </summary>
///// <param name="Send"></param>
//public void EnemyPhysicsUpdate(Enemy Send)
//{

//    if (Send.ChaseTarget != null && Send.Targetdistance > Send.EnemyData.MeleeAtkRange)
//    {

//        Send.Agent.isStopped = false;
//        Send.Agent.stoppingDistance = Send.EnemyData.MeleeAtkRange;
//        Send.Agent.SetDestination(Send.ChaseTarget.position);
//        Debug.Log("이동중");

//    }

//    else
//    {

//        Send.Agent.isStopped = true;
//        Send.Agent.velocity = Vector3.zero;
//        Send.EnemyChangeState(Enemy.EnemyState.Idle);
//        Debug.Log("EnemyMove : 접근->대기. physicsupdate");
//    }


//}

///// <summary>
///// LateUpdate 필요시 설정 (LateUpdate)
///// </summary>
///// <param name="Send"></param>
//public void EnemyOnLateUpdate(Enemy Send)
//{

//}



///// <summary>
///// 스테이트 종료 시 한번만 실행
///// </summary>
///// <param name="Send"></param>
//public void Exit(Enemy Send)
//{

//}