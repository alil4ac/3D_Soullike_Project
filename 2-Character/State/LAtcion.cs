using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAtcion : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        Send.CanAttackReady = false;

        Send.Anim.SetTrigger("LAction");

        Send.Col.sharedMaterial = Send.NormalFriction;

        Send.Data.Stamina -= 10f;

        UIManager.Instance.SetStamina();
    }

    public void Exit(PlayerController Send)
    {
        Send.PWeapon.EndAttack();
    }

    public void HandleInput(PlayerController Send)
    {
        if(Send.CanAttackReady &&
           Input.GetMouseButtonDown(0)&& Send.Data.Stamina>=10)
        {
            Send.ChangeState(PlayerController.EState.LAction);
        }
    }

    public void LogicUpdate(PlayerController Send)
    {
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("LAction") &&
            Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }
    }

    public void OnLateUpdate(PlayerController Send)
    {

    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
