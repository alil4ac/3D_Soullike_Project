using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : IState<PlayerController>
{
    protected float horizontal;
    protected float vertical;
    protected Vector3 m_move;
    protected float m_forward;
    protected float m_turn;
    protected Vector3 m_Camforward;
    protected float StandTurnSpeed = 270f;
    protected float MovingTurnSpeed = 450f;
    protected Vector3 m_Pos;

    private bool IsMove = false;

    public void Enter(PlayerController Send)
    {
        IsMove = true;
    }

    public void Exit(PlayerController Send)
    {
        IsMove = false;
        GameSoundManager.Instance.PlayerStop();
    }

    public void HandleInput(PlayerController Send)
    {
        if (GameManager.Instance.IsPaused == false)
        {
            #region GetInputValue

            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");

            if (vertical != 0 || horizontal != 0)
            {
                Send.Col.sharedMaterial = Send.NormalFriction;
                GameSoundManager.Instance.PlayerMove();
            }
            else if (vertical == 0 && horizontal == 0)
            {
                Send.Col.sharedMaterial = Send.FullFirction;
                GameSoundManager.Instance.PlayerStop();
            }
            else { return; }

            #endregion

            #region SetChangeState

            if (IsMove)
            {
                if (Input.GetMouseButtonDown(0) && Send.Data.Stamina >= 10f) 
                { 
                    Send.ChangeState(PlayerController.EState.LAction); 
                }

                else if (Input.GetMouseButtonDown(1) && Send.Data.Stamina >= 50f) 
                { 
                    Send.ChangeState(PlayerController.EState.RAction); 
                }

                else if (Input.GetKeyDown(KeyCode.Q)) { Send.ChangeState(PlayerController.EState.Drink); }

                else if (Input.GetKeyDown(KeyCode.Space) && Send.Data.Stamina > 20f)
                {

                    if (Send.Index != PlayerController.EState.Roll)
                    {
                        IsMove = false;
                        Send.ChangeState(PlayerController.EState.Roll);

                    }
                }

                else if (Input.GetKey(KeyCode.LeftArrow)) { Send.ChangeState(PlayerController.EState.Buff); }

                else { }

            }

            #endregion

        }
        else if (GameManager.Instance.IsPaused == true)
        {
            vertical = 0;
            horizontal = 0;
            GameSoundManager.Instance.PlayerStop();
        }
        
        if (Send.Data.Stamina < Send.Data.MaxStamina)
        {
            Send.Data.Stamina += 0.33f;
        }
        else if(Send.Data.Stamina > Send.Data.MaxStamina)
        {
            Send.Data.Stamina = Send.Data.MaxStamina;
        }
    }

    public void LogicUpdate(PlayerController Send)
    {
        #region SetMoveValue

        if (Send.IsTarget && Send.lowTarget != null)
        {
            Vector3 LookPos = new Vector3(Send.lowTarget.position.x, Send.transform.position.y, Send.lowTarget.position.z);

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

        Send.Anim.SetFloat("Value", m_forward, 0.01f, Time.deltaTime);

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

            Send.RB.MovePosition(Send.transform.position + newCrs * Time.fixedDeltaTime * 4f);

            Debug.DrawLine(Send.transform.position, Send.transform.position + Send.RB.velocity);
        }
        else if (!Send.IsTarget)
        {

            if (m_Pos.x != 0 || m_Pos.z != 0)
            {
                Vector3 crs = Vector3.Cross(Send.transform.right, Send.SlopeNormalPerp);
                Send.RB.velocity = crs * 5f;
            }
            Debug.DrawLine(Send.transform.position, Send.transform.position + Send.RB.velocity);
        }

        #endregion



    }
}
