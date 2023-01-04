using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="BasicSlamAttack", menuName = "Attacks/BasicSlam")]
public class BasicSlam : EnemyBaseState
{
    [Min(0)][SerializeField] private float playerFollowTime;
    [Tooltip("In milliseconds")]
    [SerializeField] private int timeBeforeNextAttack = 1000;
    [SerializeField] private GameObject graveStone;

    private float timeLeft;

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        timeLeft = playerFollowTime;
        yield return null;
    }

    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        if (timeLeft < 0) ExitState(enemyStateManager);

        ExecuteOperation(enemyStateManager);
        timeLeft -= Time.deltaTime;
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyStateManager enemyStateManager) => enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
}
