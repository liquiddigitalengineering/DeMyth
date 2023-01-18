using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpinAttack", menuName ="Attacks/Spin")]
public class SpinAttack : EnemyBaseState
{
    public static Action<Transform> PlayerKnockedEvent;

    [SerializeField] private int timeBeforeNextAttack = 1;
    [SerializeField] private float maxPlayerTime = 1;

    private float time = 0;
    private bool playerInRange = false;

    #region OnEnable & OnDisable
    private void OnEnable()
    {
        EnemyStateManager.OnRangeChanged += StopCounting;
        AnimationEventsHandler.SpinAttackFinishedEvent += ExitState;
    }

    private void OnDisable()
    {
        EnemyStateManager.OnRangeChanged -= StopCounting;
        AnimationEventsHandler.SpinAttackFinishedEvent -= ExitState;
    }
    #endregion

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        playerInRange = true;
        enemyStateManager.Anim.SetTrigger("spinAttack");
        this.time = 0;
        yield return new WaitForSeconds(0.1f);
    }



    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        if (!playerInRange) return;
        
        time += Time.deltaTime;
        if (time < maxPlayerTime) return;

        PlayerKnockedEvent?.Invoke(enemyStateManager.transform);
    }
    
    private void StopCounting(bool inRange)
    {     
        playerInRange = inRange;
        if (!inRange) time = 0;
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager) { }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }
}
