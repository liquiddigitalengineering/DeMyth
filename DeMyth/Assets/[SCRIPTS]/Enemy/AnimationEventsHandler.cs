using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    public static Action<EnemyStateManager> SpinAttackFinishedEvent;
    public static Action<EnemyStateManager> BasicSlamFinishedEvent;
    public static Action<EnemyStateManager> ChargeAttackFisnihedEvent;
    public static Action<EnemyStateManager> JumpAttackFisnihedEvent;

    [SerializeField] EnemyStateManager enemyStateManager;
    [SerializeField] private Animator anim;

    public void SpinAttackFinished() => SpinAttackFinishedEvent?.Invoke(enemyStateManager);
    public void BasicSlamFinished() => BasicSlamFinishedEvent?.Invoke(enemyStateManager);
    public void ChargeAttackFinished() => ChargeAttackFisnihedEvent?.Invoke(enemyStateManager);
    public void StopTheAnimation() => anim.speed = 0;

    public void StartNextAnim()
    {
        if (enemyStateManager.InRange) anim.SetTrigger("jumpSlam");
        else anim.SetTrigger("idle");
    }

    public void JumpAttackFinished() => JumpAttackFisnihedEvent?.Invoke(enemyStateManager);
}
