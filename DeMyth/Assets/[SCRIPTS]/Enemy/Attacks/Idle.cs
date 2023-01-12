using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleAttack", menuName = "Attacks/Idle")]
public class Idle : EnemyBaseState
{
    [Min(1)][SerializeField] private float enemyRange = 1;
    [SerializeField] private List<EnemyBaseState> InRangeAttacks;
    [SerializeField] private List<EnemyBaseState> NotInRangeAttacks;
   
    private EnemyBaseState state;

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        float timeInSeconds = time / 1000;
        ExecuteOperation(enemyStateManager);
        yield return new WaitForSeconds(timeInSeconds);
        ExitState(enemyStateManager);
    }

    public override void UpdateState(EnemyStateManager enemyStateManager) { }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        float distance = Vector2.Distance(enemyStateManager.gameObject.transform.position, enemyStateManager.Player.transform.position);
        bool isNear = distance <= enemyRange;

        if (isNear)
            state = InRangeAttacks[Random.Range(0, InRangeAttacks.Count)];
        else
            state = NotInRangeAttacks[Random.Range(0, NotInRangeAttacks.Count)];

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.SwitchStates(state);
    }
}
