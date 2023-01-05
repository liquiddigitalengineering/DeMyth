using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="BasicSlamAttack", menuName = "Attacks/BasicSlam")]
public class BasicSlam : EnemyBaseState
{
    [Min(0)][SerializeField] private float playerFollowTime;
    [Tooltip("In milliseconds")]
    [SerializeField] private int timeBeforeNextAttack = 1000;
    [Header("Grave stone settings")]

    [SerializeField] [Min(0)] private float graveStoneSpeed;
    [SerializeField][Min(0)] private float graveStoneDamage;

    private float timeLeft;
    private Transform playerTransform;

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {      
        playerTransform = enemyStateManager.Player.transform;
        enemyStateManager.GraveStone.transform.position = playerTransform.position;
        enemyStateManager.GraveStone.SetActive(true);

        timeLeft = playerFollowTime;
        yield return null;
    }

    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        if (timeLeft <= 0) ExitState(enemyStateManager);

        ExecuteOperation(enemyStateManager);
        timeLeft -= Time.deltaTime;
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        playerTransform = enemyStateManager.Player.transform;

        enemyStateManager.GraveStone.transform.position = Vector3.MoveTowards(enemyStateManager.GraveStone.transform.position, playerTransform.position, graveStoneSpeed * Time.deltaTime);
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.GraveStone.SetActive(false);
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }
}
