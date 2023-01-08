using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpSlamAttack", menuName = "Attacks/JumpSlam")]
public class JumpSlam : EnemyBaseState
{
    [Tooltip("In milliseconds")]
    [SerializeField] private int timeBeforeNextAttack = 500;
    [Header("How high should enemy jump")]
    [SerializeField] private float height = 2;
    [Header("How fast should enemy move")]
    [SerializeField] private float speed = 2;
    [Header("Attack damage")]
    [SerializeField] private float attackDamage;

    private Vector2 playerTransform;
    private bool canBeFollowed = false;
    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        enemyStateManager.Anim.SetTrigger("jumpSlam");
        canBeFollowed = false;
        yield return new WaitForSeconds(1f);
        ExecuteOperation(enemyStateManager);
        canBeFollowed = true;
    }

    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        if (!canBeFollowed) return;

        enemyStateManager.gameObject.transform.position = Vector3.MoveTowards(enemyStateManager.gameObject.transform.position, playerTransform, speed * Time.deltaTime);
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        playerTransform = enemyStateManager.Player.transform.position;
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }
}
