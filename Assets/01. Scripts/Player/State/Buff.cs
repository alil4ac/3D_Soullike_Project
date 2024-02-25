using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {

    }

    public void Exit(PlayerController Send)
    {

    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {
        #region SetChangeState

        if (Send.Anim.GetCurrentAnimatorStateInfo(1).IsTag("Buff") &&
            Send.Anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1f)
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }

        #endregion
    }

    public void OnLateUpdate(PlayerController Send)
    {

    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
