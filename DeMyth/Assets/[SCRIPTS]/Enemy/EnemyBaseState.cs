using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : ScriptableObject
{
  
    public abstract void EnterState(EnemyStateManager enemyStateManager, int time);
    protected abstract void ExecuteOperation(EnemyStateManager enemyStateManager);
    public abstract void ExitState(EnemyStateManager enemyStateManager);
}
