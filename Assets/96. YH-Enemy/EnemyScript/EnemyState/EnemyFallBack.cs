using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFallBack : IEState<Enemy>
{

    public void EnemyLogicUpdate(Enemy Send)
    {

    }

    public void EnemyMoveInput(Enemy Send)
    {
    }

    public void EnemyOnLateUpdate(Enemy Send)
    {

    }

    public void EnemyPhysicsUpdate(Enemy Send)
    {

        if (Send.ChaseTarget != null && !Send.Agent.pathPending)
        {

            Vector3 direction = Send.transform.position - Send.ChaseTarget.position;
            Vector3 fallBackPosition = Send.ChaseTarget.position + direction.normalized * Send.EnemyData.MeleeAtkRange;

            Send.transform.rotation = Quaternion.Slerp(Send.transform.rotation, Quaternion.LookRotation(Send.ChaseTarget.transform.position - Send.transform.position), Send.EnemyData.TurnSpeed * 1.3f);

            Send.Agent.isStopped = false;

            Send.Agent.SetDestination(fallBackPosition);



            if (Send.Targetdistance >= Send.EnemyData.CloseSightRange && (Mathf.Abs(Send.Targetdistance - Send.EnemyData.MeleeAtkRange) > Send.DistanceThreshold))     //타켓 거리가 인접범위보다 크면
            {
                Send.Agent.isStopped = true;
                Send.Agent.velocity = Vector3.zero;
                Send.EnemyChangeState(Enemy.EnemyState.Idle);
                Debug.LogWarning("성공적으로 탈출");

            }

            else
            {
                return;
            }


        }
        else if (Send.ChaseTarget == null && !Send.Agent.pathPending)//Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("fallBack"))
        {
            if (Send.Targetdistance >= Send.EnemyData.CloseSightRange && (Mathf.Abs(Send.Targetdistance - Send.EnemyData.MeleeAtkRange) > Send.DistanceThreshold))
            {

                //Send.Agent.isStopped = true;
                Send.Agent.velocity = Vector3.zero;
                Send.EnemyChangeState(Enemy.EnemyState.Idle);
                Debug.Log("EnemyFallBack : 후퇴->대기, 타켓x or 거리>=근접범위");

            }

        }





    }

    public void Enter(Enemy Send)
    {
        Send.Agent.isStopped = false;
        
        Send.Agent.stoppingDistance = 0.0f;
        //isJumping = false;
        //jumpBackTimer = 0f;

    }

    public void Exit(Enemy Send)
    {
        
        Send.Agent.stoppingDistance = Send.EnemyData.MeleeAtkRange;
        Send.Agent.speed = 3.5f;
        Send.Agent.acceleration = 5f;

    }



}
