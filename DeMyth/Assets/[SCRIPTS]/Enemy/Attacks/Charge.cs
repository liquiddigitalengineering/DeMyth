using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Transform = UnityEngine.Transform;

public enum DirectionEnum { Left, Right, Down, Up, NoDirection}

[CreateAssetMenu(fileName = "ChargeAttack", menuName = "Attacks/ChargeAttack")]
public class Charge : EnemyBaseState
{
    [Tooltip("Time before the next attack in miliseconds")]
    [SerializeField] [Min(1000f)] private int timeBeforeAttack = 1000;

    private Vector2 playerPosNormalized;

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

        enemyStateManager.gameObject.transform.position = Vector3.MoveTowards(enemyStateManager.gameObject.transform.position, enemyStateManager.Player.transform.position, 10 * Time.deltaTime);
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
