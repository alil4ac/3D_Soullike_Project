﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEState<T>
{

    void Enter(T Send);

    void EnemyMoveInput(T Send);

    void EnemyLogicUpdate(T Send);

    void EnemyPhysicsUpdate(T Send);

    void EnemyOnLateUpdate(T Send);

    void Exit(T Send);
}

/*
 * Enter         -> 스테이트 진입 시 한번만 실행
 * HandleInput   -> 키입력 등의 로직 실행 (Update)
 * LogicUpdate   -> 스테이트 전환 및 행동조건 로직 실행 (Update)
 * PhysicsUpdate -> 실제 행동 밸류 설정 (FixedUpdate)
 * OnLateUpdate  -> LateUpdate 필요시 설정 (LateUpdate)
 * Exit          -> 스테이트 종료 시 한번만 실행
 */
