using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleAttack", menuName = "Attacks/Idle")]
public class Idle : EnemyBaseState
{
    [SerializeField] private List<EnemyBaseState> InRangeAttacks;
    [SerializeField] private List<EnemyBaseState> NotInRangeAttacks;
    [Min(1)]
    [SerializeField] private float enemyRange = 1;

    private EnemyBaseState state;

    public override void EnterState(EnemyStateManager enemyStateManager, int time) => Operation(enemyStateManager, time);


    private async void Operation(EnemyStateManager enemyStateManager, int time)
    {
        ExecuteOperation(enemyStateManager);
        await Task.Delay(time);
        ExitState(enemyStateManager);
    }
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
