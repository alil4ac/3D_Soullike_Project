using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStateMachine<T>
{
    public IEState<T> eCurrentState { get; private set; }

    private T e_Send;

    public EStateMachine(T send, IEState<T> eState)
    {
        e_Send = send;
        Initialze(eState);
    }

    private void Initialze(IEState<T> startState)
    {
        eCurrentState= startState;

        startState.Enter(e_Send);

    }

    public void EnemyChangeState(IEState<T> newState)
    {
        eCurrentState.Exit(e_Send);

        eCurrentState = newState;

        eCurrentState.Enter(e_Send);
    }

    public void EnemyMoveInput()
    {
        eCurrentState.EnemyMoveInput(e_Send);
    }

    public void EnemyLogicInput()
    {
        eCurrentState.EnemyLogicUpdate(e_Send);
    }

    public void EnemyPhysicsUpdate()
    {
        eCurrentState.EnemyPhysicsUpdate(e_Send);
    }

    public void EnemyOnLateupdate()
    {
        eCurrentState.EnemyOnLateUpdate(e_Send);
    }
}
