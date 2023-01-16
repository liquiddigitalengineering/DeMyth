using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpSlamAttack", menuName = "Attacks/JumpSlam")]
public class JumpSlam : EnemyBaseState
{
    [Tooltip("In milliseconds")]
    [SerializeField] private int timeBeforeNextAttack = 500;
    [Header("How fast should enemy move")]
    [SerializeField] private float speed = 3;
    [Header("Attack damage")]
    [SerializeField] private float attackDamage;

    private Vector3 playerTransform;
    private bool canBeFollowed = false;

    #region OnEnable & OnDisable
    private void OnEnable()
    {
        AnimationEventsHandler.JumpAttackFisnihedEvent += ExitState;
    }

    private void OnDisable()
    {
        AnimationEventsHandler.JumpAttackFisnihedEvent -= ExitState;
    }
    #endregion

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        enemyStateManager.Col.enabled = false;
        LookDirection(enemyStateManager);
        enemyStateManager.Anim.SetTrigger("jump");
        canBeFollowed = false;
        ExecuteOperation(enemyStateManager);
        yield return new WaitForSeconds(0.5f);
       
        canBeFollowed = true; 
    }

    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        if (canBeFollowed == false) return;

        enemyStateManager.gameObject.transform.position = Vector3.MoveTowards(enemyStateManager.gameObject.transform.position, playerTransform, speed * Time.deltaTime);

        if(enemyStateManager.transform.position == playerTransform) {
            enemyStateManager.Anim.speed = 1;
        }
           
    }

   

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        playerTransform = enemyStateManager.Player.transform.position;
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.GetDamageManager.DamagePlayer(attackDamage);
        enemyStateManager.Col.enabled = true;
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }


    #region other methods
    private void LookDirection(EnemyStateManager enemyStateManager)
    {
        Vector2 playerPosNormalized = (enemyStateManager.Player.transform.position - enemyStateManager.transform.position).normalized;
        if (playerPosNormalized.x >= 0)
            enemyStateManager.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        else if (playerPosNormalized.x < 0)
            enemyStateManager.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    #endregion
}
