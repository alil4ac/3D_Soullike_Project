using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : IEState<Enemy>
{
    

    /// <summary>
    /// 스테이트 진입 시 한번만 실행
    /// </summary>
    /// <param name="Send"></param>
    public void Enter(Enemy Send)
    {
        Send.Agent.isStopped = true;

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
        if (Send.SearchPlayer() && !Send.Agent.pathPending)
        {
            //Send.Agent.isStopped = true;

            if (Send.ChaseTarget != null)
            {
                //Send.transform.rotation = Quaternion.Slerp(Send.transform.rotation, Quaternion.LookRotation(Send.ChaseTarget.transform.position - Send.transform.position), Send.EnemyData.TurnSpeed);
                if ((Mathf.Abs(Send.Targetdistance - Send.EnemyData.CloseSightRange) > Send.DistanceThreshold) && Send.Targetdistance <= Send.EnemyData.CloseSightRange && Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                {
                    //Send.Agent.isStopped = false;

                    if (!Send.IsDragon)
                    {
                        Send.EnemyChangeState(Enemy.EnemyState.FallBack);
                        Debug.Log("EnemyIdle : 대기->후퇴");
                    }
                    else
                    {

                        if (Send.IsJumping == false)
                        {
                            if (Send.JumpBackTimer > Send.JumpBackTerm)
                            {
                                Send.IsJumping = true;
                                Send.ResetJumpBackTimer();
                                Send.EnemyChangeState(Enemy.EnemyState.BossBackJump);
                                Debug.Log("보스 백 점프");
                            }

                        }

                    }

                }

                else if (Send.Targetdistance < Send.EnemyData.MeleeAtkRange && (Mathf.Abs(Send.Targetdistance - Send.EnemyData.MeleeAtkRange) > Send.DistanceThreshold) &&  Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                {

                    Send.EnemyChangeState(Enemy.EnemyState.Attack);
                    Debug.Log("EnemyAttack으로 전환");


                }

                else if (Send.Targetdistance > Send.EnemyData.MeleeAtkRange && (Mathf.Abs(Send.Targetdistance - Send.EnemyData.MeleeAtkRange) > Send.DistanceThreshold) &&Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                {
                    Send.Agent.isStopped = false;
                    Send.Agent.stoppingDistance = Send.EnemyData.MeleeAtkRange;
                    Send.transform.rotation = Quaternion.Slerp(Send.transform.rotation, Quaternion.LookRotation(Send.ChaseTarget.transform.position - Send.transform.position), Send.EnemyData.TurnSpeed);

                    
                    Send.EnemyChangeState(Enemy.EnemyState.Move);

                    if (Send.IsDragon == true &&Send.IsSatk==false)
                    {
                        if (Send.SAtkTimer > Send.SatkTerm)
                        {
                            Send.IsSatk = true;
                            Send.Agent.isStopped = true;
                            Send.Agent.velocity = Vector3.zero;
                            Send.EnemyChangeState(Enemy.EnemyState.SAttack);
                            Debug.LogWarning("SAttack");
                        }
                        else
                        {
                            //Debug.Log("EnemyIdle : 대기->접근");
                        }
                    }



                }

            }

        }


        if (Send.ChaseTarget != null && Send.Targetdistance <= Send.EnemyData.MeleeAtkRange)
        {
            Send.transform.rotation = Quaternion.Slerp(Send.transform.rotation, Quaternion.LookRotation(Send.ChaseTarget.transform.position - Send.transform.position), Send.EnemyData.TurnSpeed * 1.3f);

            if (Send.IsBattle == true)
            {
                Send.transform.rotation = Quaternion.Slerp(Send.transform.rotation, Quaternion.LookRotation(Send.ChaseTarget.transform.position - Send.transform.position), Send.EnemyData.TurnSpeed * 2f);
            }
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

    }

}
