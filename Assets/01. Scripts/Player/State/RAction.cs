﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAction : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        Send.Col.sharedMaterial = Send.NormalFriction;

        Send.Data.Stamina -= 50f;

        UIManager.Instance.SetStamina();
    }

    public void Exit(PlayerController Send)
    {
        Send.PWeapon.EndAttack();
    }


    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {
        #region SetMoveValue

        //if (Send.IsTarget)
        //{
        //    Vector3 LookPos = new Vector3(Send.lowTarget.position.x, 0f, Send.lowTarget.position.z);

        //    Send.transform.LookAt(LookPos);
        //}

        #endregion


        #region SetChangeState

        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("RAction") &&
           Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
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
