using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeSlamAttack", menuName = "Attacks/ChargeSlam")]
public class ChargeSlam : EnemyBaseState
{
    [SerializeField] private float attackTime;
    [Tooltip("In milliseconds")]
    [SerializeField] private int timeBeforeNextAttack = 1000;
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager,int time)
    {
        enemyStateManager.GraveStone.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }

    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        float y = Mathf.Cos(Time.time * frequency) * amplitude;
        float x = Mathf.Sin(Time.time * frequency) * amplitude;

        enemyStateManager.GraveStone.transform.position = new Vector2(x,y);
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.GraveStone.SetActive(false);
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }
}
