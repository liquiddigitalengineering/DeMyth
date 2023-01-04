using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeAttack", menuName = "Attacks/ChargeAttack")]
public class Charge : EnemyBaseState
{
    [Tooltip("Time before the next attack in miliseconds")]
    [SerializeField] [Min(1000f)] private int timeBeforeAttack = 1000;

    public override void EnterState(EnemyStateManager enemyStateManager, int time)
    {
        ExecuteOperation(enemyStateManager);
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        Vector2 playerPosition = enemyStateManager.Player.transform.position;
        Vector2 enemyPosition = enemyStateManager.transform.position;

        if (playerPosition.x < enemyPosition.x)
            Debug.Log("Left baby");
        else
            Debug.Log("Right baby");

        if (playerPosition.y < enemyPosition.y)
            Debug.Log("Down  baby");
        else Debug.Log("Up baby");
        ExitState(enemyStateManager);
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.SwitchToIdle(timeBeforeAttack);
    }
}
