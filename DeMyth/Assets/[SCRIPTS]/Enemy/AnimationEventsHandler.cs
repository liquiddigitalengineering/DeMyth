using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    public static Action<EnemyStateManager> SpinAttackFinishedEvent;
    public static Action<EnemyStateManager> BasicSlamFinishedEvent;
    public static Action<EnemyStateManager> ChargeAttackFisnihedEvent;

    [SerializeField] EnemyStateManager enemyStateManager;
   

    public void SpinAttackFinished() => SpinAttackFinishedEvent?.Invoke(enemyStateManager);
    public void BasicSlamFinished() => BasicSlamFinishedEvent?.Invoke(enemyStateManager);
    public void ChargeAttackFinished() => ChargeAttackFisnihedEvent?.Invoke(enemyStateManager);
}
