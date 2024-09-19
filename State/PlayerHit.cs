using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        Send.gameObject.layer = LayerMask.NameToLayer("Ignore");

        Send.PWeapon.EndAttack();

        Send.Anim.SetBool("IsSmash", Send.IsSmash);

        Send.Anim.SetFloat("HitVertical", Send.HitPos.z * 2);

        Send.Anim.SetFloat("HitHorizontal", Send.HitPos.x);

        Send.Anim.SetTrigger("Hit");

        Send.Col.sharedMaterial = Send.FullFirction;

        CameraManager.Instance.ShakeDoCam(DG.Tweening.Ease.InOutBounce);
    }

    public void Exit(PlayerController Send)
    {
        Send.gameObject.layer = LayerMask.NameToLayer("PlayerHit");
    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {
        if(Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("HitNormal") &&
           Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }

        else if(Send.Anim.GetCurrentAnimatorStateInfo(0).IsTag("StandUp") &&
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
