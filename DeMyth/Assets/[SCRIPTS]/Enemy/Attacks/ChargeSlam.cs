using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeSlamAttack", menuName = "Attacks/ChargeSlam")]
public class ChargeSlam : EnemyBaseState
{
    [SerializeField] private float attackTime;
    [SerializeField] private int timeBeforeNextAttack = 1;
    [SerializeField] private float exitState = 5;
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager,int time)
    {
        enemyStateManager.GetAnimator.SetTrigger("chargeSlam");
        enemyStateManager.GetGraveStone.SetActive(true);
        yield return new WaitForSeconds(exitState);

        ExitState(enemyStateManager);
    }

    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        float y = Mathf.Cos(Time.time * frequency) * amplitude;
        float x = Mathf.Sin(Time.time * frequency) * amplitude;

        enemyStateManager.GetGraveStone.transform.position = new Vector2(x + enemyStateManager.gameObject.transform.position.x, y + enemyStateManager.gameObject.transform.position.y);
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
       
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.GetGraveStone.SetActive(false);
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }
}
