using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : ScriptableObject
{
    public abstract IEnumerator EnterState(EnemyStateManager enemyStateManager, int time);
    public abstract void UpdateState(EnemyStateManager enemyStateManager);
    protected abstract void ExecuteOperation(EnemyStateManager enemyStateManager);
    public abstract void ExitState(EnemyStateManager enemyStateManager);
}
