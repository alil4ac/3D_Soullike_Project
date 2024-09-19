using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : IState<PlayerController>
{
    private bool IsRoll = false;
    public void Enter(PlayerController Send)
    {
        Send.Col.sharedMaterial = Send.NormalFriction;
        if (Send.IsTarget)
        {
            Vector3 dir = (Send.lowTarget.position - Send.transform.position).normalized;
            Vector3 crs = Vector3.Cross(Send.transform.up, dir);

            crs.Normalize();

            Vector3 newPos = Send.Anim.GetFloat("Horizontal") * crs;
            newPos += Send.Anim.GetFloat("Vertical") * dir;

            newPos.Normalize();

            Send.RB.AddForce(newPos * 5f, ForceMode.Impulse);
        }
        else
        {
            Send.RB.AddForce(Send.transform.forward * 5f, ForceMode.Impulse);
            Send.Anim.SetFloat("Horizontal", 0f);
            Send.Anim.SetFloat("Vertical", 1f);
        }

        IsRoll = true;
        GameSoundManager.Instance.SetPlayerFx("roll", Send.transform );
        Send.Data.Stamina -= 20f;
        UIManager.Instance.SetStamina();
    }

    public void Exit(PlayerController Send)
    {
        IsRoll = false;
    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") &&
        Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && IsRoll)
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
