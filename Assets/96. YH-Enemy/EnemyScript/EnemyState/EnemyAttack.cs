using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : IEState<Enemy>
{
    //적 공격 구현

    /// <summary>
    /// 스테이트 진입 시 한번만 실행
    /// </summary>
    /// <param name="Send">Enemy</param>
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
    /// 실제 행동 밸류 설정 (FixedUpdate)
    /// </summary>
    /// <param name="Send"></param>
    public void EnemyPhysicsUpdate(Enemy Send)
    {
        //if (Send.IsBoss)
        //{
        //GameObject mouth= GameObject.Find("mountain_dragon_ Head");
        //    if(mouth != null)
        //    {
        //        mouth.transform.position = Vector3.MoveTowards(mouth.transform.position, Send.ChaseTarget.position, 1 * Time.deltaTime);
        //    }

        //}




        if ( Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("meleeAtk") && Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            //Send.Agent.isStopped = true;
            Send.Agent.velocity = Vector3.zero;
            Send.EnemyChangeState(Enemy.EnemyState.Idle); //어택이 끝나면 대기 상태로
        }
        else if(Send.ChaseTarget == null)
        {
            //Send.Agent.isStopped = true;
            Send.Agent.velocity = Vector3.zero;
            Send.EnemyChangeState(Enemy.EnemyState.Idle); //어택이 끝나면 대기 상태로
        }



    }

    /// <summary>
    /// 스테이트 전환 및 행동조건 로직 실행 (Update)
    /// </summary>
    /// <param name="Send"></param>
    public void EnemyLogicUpdate(Enemy Send)
    {
        
        
       
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
        Send.Agent.isStopped = false;
        
    }
}
