using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDeath : IEState<Enemy>
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
    }

    public void Enter(Enemy Send)
    {
        
        
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("hitted") && Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && Send.IsDead == true)
        {
            ////사망 실행 동작들
            Debug.Log("Dead");

            Send.OnDie();

        }
        

    }

    public void Exit(Enemy Send)
    {
        
    }


}
