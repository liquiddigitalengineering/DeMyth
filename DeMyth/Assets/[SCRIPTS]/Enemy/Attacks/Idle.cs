using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "IdleAttack", menuName = "Attacks/Idle")]
public class Idle : EnemyBaseState
{
    /// <summary>
    /// Event is called when the player is near the enemy, so enemy can be switched to spin attack
    /// </summary>
    public static Action<EnemyBaseState> OnStateChanged;

    [SerializeField] private List<EnemyBaseState> InRangeAttacks;
    [SerializeField] private List<EnemyBaseState> NotInRangeAttacks;
    [Space(10)][Header("When player is in close range")]
    [SerializeField] private SpinAttack spinAttack;
    [SerializeField] private JumpSlam jumpAttack;
    [SerializeField] private float timeBeforeNextSpinAttack;

    private EnemyBaseState state;
    private bool isNear, isUsingSpin;
    private float timeLeftBeforeNextSpin;

    private void OnEnable()
    {
        EnemyStateManager.OnRangeChanged += InRange;
        timeLeftBeforeNextSpin = timeBeforeNextSpinAttack;
    }

    private void OnDisable()
    {
        EnemyStateManager.OnRangeChanged -= InRange;
    }

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        isNear = enemyStateManager.InRange;

        isUsingSpin = false;
      
        yield return new WaitForSeconds(time);

        ExitState(enemyStateManager);
    }

    public override void UpdateState(EnemyStateManager enemyStateManager) 
    { 
        timeLeftBeforeNextSpin -= Time.deltaTime;

        if (timeLeftBeforeNextSpin > 0) return;
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

        ExecuteOperation(enemyStateManager);
        enemyStateManager.SwitchStates(state);
    }

    private void InRange(bool inRange, EnemyBaseState currentState)
    {  
        isNear = inRange;

        if (!inRange || timeLeftBeforeNextSpin > 0 || currentState == jumpAttack) return;

        SpinAttack();
    }

    #region Player is near
    /// <summary>
    /// Triggered when the player is near the enemy, so the player can be knockbacked
    /// </summary>
    private void SpinAttack()
    {
        timeLeftBeforeNextSpin = timeBeforeNextSpinAttack;
        OnStateChanged(spinAttack);
        isUsingSpin = true;
    }
    #endregion
}
