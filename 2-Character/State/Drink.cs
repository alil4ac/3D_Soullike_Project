using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : IState<PlayerController>
{
    float horizontal;
    float vertical;
    Vector3 m_move;
    float m_forward;
    float m_turn;
    Vector3 m_Camforward;
    float StandTurnSpeed = 90f;
    float MovingTurnSpeed = 180f;
    Vector3 m_Pos;

    
    public void Enter(PlayerController Send)
    {
        Send.PWeapon.CurrentWeapon.SetActive(false);

        Send.Col.sharedMaterial = Send.NormalFriction;
    }

    public void Exit(PlayerController Send)
    {
        Send.PWeapon.CurrentWeapon.SetActive(true);
    }

    public void HandleInput(PlayerController Send)
    {
        #region GetInputValue

        vertical = Input.GetAxis("Vertical")/2;

        horizontal = Input.GetAxis("Horizontal")/2;

        #endregion
    }

    public void LogicUpdate(PlayerController Send)
    {
        #region SetMoveValue

        Send.Anim.SetBool("IsTarget", Send.IsTarget);

        if (Send.IsTarget)
        {
            Vector3 LookPos = new Vector3(Send.lowTarget.position.x, 0f, Send.lowTarget.position.z);

            Send.transform.LookAt(LookPos);
        }
        else
        {
            m_Camforward = Vector3.Scale(Send.m_Cam.transform.forward, new Vector3(1, 0, 1)).normalized;

            m_move = vertical * m_Camforward + horizontal * Send.m_Cam.transform.right;

            if (m_move.magnitude > 1f) { m_move.Normalize(); }

            m_move = Send.transform.InverseTransformDirection(m_move);

            m_turn = Mathf.Atan2(m_move.x, m_move.z);

            m_forward = m_move.z;

            float turnSpeed = Mathf.Lerp(StandTurnSpeed, MovingTurnSpeed, m_forward);

            Send.transform.Rotate(0, m_turn * turnSpeed * Time.deltaTime, 0);

            m_Pos = new Vector3(m_turn, 0f, m_forward).normalized;
        }

        #endregion

        #region SetAnimationValue

        Send.Anim.SetFloat("Value", m_forward, 0.05f, Time.deltaTime);

        #endregion

        #region SetChangeState

        if(Send.Anim.GetCurrentAnimatorStateInfo(1).IsName("Drinking") &&
           Send.Anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1f)
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }

        #endregion

        Send.CheckSlope();
    }

    public void OnLateUpdate(PlayerController Send)
    {

    }

    public void PhysicsUpdate(PlayerController Send)
    {
        #region SetPhysicsValue

        if (Send.IsTarget && Send.lowTarget != null)
        {
            Vector3 dir = (Send.lowTarget.position - Send.transform.position).normalized;
            Vector3 crs = Vector3.Cross(Send.transform.up, dir);

            crs.Normalize();

            Vector3 newPos = horizontal * crs;
            newPos += vertical * dir;
            newPos.y = 0f;
            newPos.Normalize();

            Vector3 right = Vector3.Cross(newPos, -Send.transform.up);

            Vector3 newCrs = Vector3.Cross(right, Send.SlopeNormalPerp);

            Send.Anim.SetFloat("Horizontal", horizontal, 0.01f, Time.fixedDeltaTime);
            Send.Anim.SetFloat("Vertical", vertical, 0.01f, Time.fixedDeltaTime);

            Send.RB.MovePosition(Send.transform.position + newCrs * Time.fixedDeltaTime * 2f);

            Debug.DrawLine(Send.transform.position, Send.transform.position + Send.RB.velocity);
        }
        else if (!Send.IsTarget)
        {

            if (m_Pos.x != 0 || m_Pos.z != 0)
            {
                Vector3 crs = Vector3.Cross(Send.transform.right, Send.SlopeNormalPerp);
                Send.RB.velocity = crs * 2.5f;
            }
            Debug.DrawLine(Send.transform.position, Send.transform.position + Send.RB.velocity);
        }
        #endregion

    }
}
