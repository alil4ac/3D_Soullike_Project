using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        GameManager.Instance.IsRestart = true;
        Send.gameObject.layer = LayerMask.NameToLayer("Ignore");

        Send.Anim.SetTrigger("Dead");
        GameSoundManager.Instance.SetPlayerFx("you-died-sound-effect", Send.transform);
        UIManager.Instance.PlayerDie();
        Send.Col.sharedMaterial = Send.FullFirction;
    }

    public void Exit(PlayerController Send)
    {

    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {
        //테스트용 리스타트
        //if(Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Dead") && Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        Send.Data.Health = Send.Data.MaxHealth;
        //        Send.Anim.Play("Movement", 0);
        //    }
        //}
    }

    public void OnLateUpdate(PlayerController Send)
    {

    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
