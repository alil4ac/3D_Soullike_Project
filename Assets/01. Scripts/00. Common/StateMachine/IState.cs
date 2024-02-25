using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    void Enter(T Send);

    void HandleInput(T Send);

    void LogicUpdate(T Send);

    void PhysicsUpdate(T Send);

    void OnLateUpdate(T Send);

    void Exit(T Send);
}

/*
 * Enter         -> ������Ʈ ���� �� �ѹ��� ����
 * HandleInput   -> Ű�Է� ���� ���� ���� (Update)
 * LogicUpdate   -> ������Ʈ ��ȯ �� �ൿ���� ���� ���� (Update)
 * PhysicsUpdate -> ���� �ൿ ��� ���� (FixedUpdate)
 * OnLateUpdate  -> LateUpdate �ʿ�� ���� (LateUpdate)
 * Exit          -> ������Ʈ ���� �� �ѹ��� ����
 */
