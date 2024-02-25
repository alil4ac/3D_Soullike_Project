using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBossBackJump : IEState<Enemy>
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

            Vector3 direction = Send.transform.position - Send.ChaseTarget.position; // Send 기준으로 ChaseTarget 객체까지의 방향과 거리 벡터
            Vector3 jumpBackPosition = Send.ChaseTarget.position + direction.normalized * Send.EnemyData.MeleeAtkRange * 1.5f; //공격하면서 뒤로 물러날 방향과 거리
            //direction = Vector3.ClampMagnitude(direction,15f);
            //Vector3 fallBackPosition= Send.transform.position+direction;


            Send.Agent.isStopped = false;
            //Send.Agent.stoppingDistance = 0.0f;

            //Send.Agent.SetDestination(fallBackPosition);

            Send.Agent.SetDestination(jumpBackPosition);
            if (Send.Targetdistance>Send.EnemyData.CloseSightRange && Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("jumpBack")&& Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
            {
                
                Send.Agent.isStopped = true;
                Send.Agent.velocity = Vector3.zero;
                Send.EnemyChangeState(Enemy.EnemyState.Idle);
            }


        }
        else if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("jumpBack"))
        {
            if (Send.ChaseTarget == null || Send.Targetdistance >= (Send.EnemyData.CloseSightRange + Send.EnemyData.MeleeAtkRange) * 0.5f)
            {

                Send.Agent.isStopped = true;
                Send.Agent.velocity = Vector3.zero;
                Send.EnemyChangeState(Enemy.EnemyState.Idle);
                Debug.Log("EnemyBossJumpBack : 후퇴->대기, 타켓x or 거리>=근접범위");

            }

        }

        if (Send.ChaseTarget != null)
        {
            Send.transform.rotation = Quaternion.Slerp(Send.transform.rotation, Quaternion.LookRotation(Send.ChaseTarget.transform.position - Send.transform.position), Send.EnemyData.TurnSpeed * 1.3f);

        }
    }

    public void Enter(Enemy Send)
    {
        Send.Agent.stoppingDistance = 0.0f;
        Send.Agent.speed = 10f;
        Send.Agent.acceleration = 15f;

    }

    public void Exit(Enemy Send)
    {
        Send.Agent.stoppingDistance = Send.EnemyData.MeleeAtkRange;
        Send.Agent.speed = 3.5f;
        Send.Agent.acceleration = 5f;
        Send.IsJumping = false;
        
    }



}
