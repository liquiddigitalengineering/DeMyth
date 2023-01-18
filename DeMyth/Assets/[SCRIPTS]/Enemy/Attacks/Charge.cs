using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Transform = UnityEngine.Transform;

public enum DirectionEnum { Left, Right, Down, Up, NoDirection}

[CreateAssetMenu(fileName = "ChargeAttack", menuName = "Attacks/ChargeAttack")]
public class Charge : EnemyBaseState
{
    [SerializeField] [Min(1000f)] private int timeBeforeAttack = 1;

    [SerializeField] private float speed = 10f;

    private Vector2 playerPosNormalized;
    private Vector2 playerTransform;

    private void OnEnable()
    {
        AnimationEventsHandler.ChargeAttackFisnihedEvent += ExitState;   
    }

    private void OnDisable()
    {
        AnimationEventsHandler.ChargeAttackFisnihedEvent -= ExitState;
    }

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        playerPosNormalized  = NormalizedPlayerPosition(enemyStateManager.Player.transform, enemyStateManager.transform);
        ExecuteOperation(enemyStateManager);

        yield return null;
    }

    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.gameObject.transform.position = Vector3.MoveTowards(enemyStateManager.gameObject.transform.position, playerTransform, speed * Time.deltaTime);
    }


    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        switch (ChoosedDirection(enemyStateManager)) {
            case DirectionEnum.Left:
                enemyStateManager.Anim.SetTrigger("sideCharge");
                break;
            case DirectionEnum.Right:
                enemyStateManager.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                enemyStateManager.Anim.SetTrigger("sideCharge");
                break;
            case DirectionEnum.Down:
                enemyStateManager.Anim.SetTrigger("downCharge");
                break;
            case DirectionEnum.Up:
                enemyStateManager.Anim.SetTrigger("upCharge");
                break;
        }

        playerTransform = enemyStateManager.Player.transform.position;

    }

    private DirectionEnum ChoosedDirection(EnemyStateManager enemyStateManager)
    {
        if (playerPosNormalized.y <= -0.5)
            return DirectionEnum.Down;

        else if (playerPosNormalized.y >= 0.5)
            return DirectionEnum.Up;


        if (playerPosNormalized.x >= 0.5)
            return DirectionEnum.Right;
        else if (playerPosNormalized.x <= -0.5)
            return DirectionEnum.Left;

        return DirectionEnum.NoDirection;
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        enemyStateManager.SwitchToIdle(timeBeforeAttack);
    }
     

    private Vector2 NormalizedPlayerPosition(Transform playerTransform, Transform enemyTransform) => (playerTransform.position -enemyTransform.position).normalized;
}
