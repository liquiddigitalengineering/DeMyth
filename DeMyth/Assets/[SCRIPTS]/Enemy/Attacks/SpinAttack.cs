using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpinAttack", menuName ="Attacks/Spin")]
public class SpinAttack : EnemyBaseState
{
    [SerializeField] private int timeBeforeNextAttack = 1000;
    [SerializeField] private float maxPlayerTime;

    private float time = 0;
    private bool inRange;

    #region OnEnable & OnDisable
    private void OnEnable()
    {
        EnemyStateManager.OutOfRangeEvent += StopCounting;
    }

    private void OnDisable()
    {
        EnemyStateManager.OutOfRangeEvent -= StopCounting;
    }
    #endregion

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        inRange = false;
        this.time = 0;
        yield return new WaitForSeconds(0.1f);
    }



    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        if (!inRange) return;
        
        time += Time.deltaTime;

        if (time < maxPlayerTime) return;
    }
    
    private void StopCounting(bool inRange)
    {
        this.inRange = inRange;

        if(!inRange) time = 0;

    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager) { }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }
}
