using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "DarknessAttack", menuName = "Attacks/DarknessAttack")]
public class Darkness : EnemyBaseState
{
    [Min(0)] [SerializeField] private float attackDuration;
    [SerializeField] private int timeBeforeNextAttack;

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        enemyStateManager.LightFade(false, 1f);

        yield return new WaitForSeconds(attackDuration);
        ExitState(enemyStateManager);
    }


    public override void UpdateState(EnemyStateManager enemyStateManager) { return; }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager) { }


    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
        enemyStateManager.LightFade(true, 1f);
    }
}
