using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "IdleAttack", menuName = "Attacks/Idle")]
public class Idle : EnemyBaseState
{
    public static Action<EnemyBaseState> OnStateChange;

    [SerializeField] private List<EnemyBaseState> InRangeAttacks;
    [SerializeField] private List<EnemyBaseState> NotInRangeAttacks;
    [Header("When player is in close range")]
    [SerializeField] private SpinAttack spinAttack;
    [SerializeField] private float timeBeforeNextSpinAttack;

    private EnemyBaseState state;
    private bool isNear, isUsingSpin;
    private float timeLeft;

    private void OnEnable()
    {
        EnemyStateManager.OutOfRangeEvent += InRange;
    }

    private void OnDisable()
    {
        EnemyStateManager.OutOfRangeEvent -= InRange;
    }

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        float timeInSeconds = time / 1000;
        ExecuteOperation(enemyStateManager);
        yield return new WaitForSeconds(timeInSeconds);
        ExitState(enemyStateManager);
    }

    public override void UpdateState(EnemyStateManager enemyStateManager) 
    { 
        timeLeft -= Time.deltaTime;

        if (timeLeft > 0) return;
        isUsingSpin = false;
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        if (isNear)
            state = InRangeAttacks[Random.Range(0, InRangeAttacks.Count)];
        else
            state = NotInRangeAttacks[Random.Range(0, NotInRangeAttacks.Count)];

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        if (isUsingSpin) return;
        enemyStateManager.SwitchStates(state);
    }

    private void InRange(bool inRange)
    {  
        isNear = inRange;

        if (!inRange || timeLeft > 0) return;

        timeLeft = timeBeforeNextSpinAttack;
        OnStateChange(spinAttack);
        isUsingSpin = true;
    }
}
