using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHit : IEState<Enemy>
{
    

    public void EnemyLogicUpdate(Enemy Send)
    {
        if ( Send.IsDead == true && Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("hitted") && Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) //체력이 0이하&&사탕 체크 true 일때
        {
            Send.EnemyChangeState(Enemy.EnemyState.Die); //몬스터의 state를 사망 state로 전환
          
        }
        else if (Send.IsDead == false&&Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("hitted") && Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Send.Agent.ResetPath();
            Send.EnemyChangeState(Enemy.EnemyState.Idle);
          
        }

        else
        {

        }

    }

    public void EnemyMoveInput(Enemy Send)
    {
    }

    public void EnemyOnLateUpdate(Enemy Send)
    {

    }

    public void EnemyPhysicsUpdate(Enemy Send)
    {


    }

    public void Enter(Enemy Send)
    {
        Send.Agent.isStopped = true;
        Send.Anim.SetTrigger("isHit");
        if (Send.IsDragon)
        {
            Send.DragonStopBreath();
        }
        
    }

    public void Exit(Enemy Send)
    {
        Send.Agent.isStopped = false;
    }
}
