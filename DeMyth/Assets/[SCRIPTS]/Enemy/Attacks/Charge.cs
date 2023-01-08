using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionEnum { Left, Right, Down, Up, NoDirection}

[CreateAssetMenu(fileName = "ChargeAttack", menuName = "Attacks/ChargeAttack")]
public class Charge : EnemyBaseState
{
    [Tooltip("Time before the next attack in miliseconds")]
    [SerializeField] [Min(1000f)] private int timeBeforeAttack = 1000;

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
        ExecuteOperation(enemyStateManager);

        yield return null;
    }

    public override void UpdateState(EnemyStateManager enemyStateManager) { }


    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        switch (ChoosedDirection(enemyStateManager)) {
            case DirectionEnum.Left:
                enemyStateManager.Anim.SetTrigger("sideCharge");
                break;
            case DirectionEnum.Right:
                enemyStateManager.gameObject.transform.localScale = new Vector3(-1, 1, 1);
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
        Vector2 playerPosNormalized = (enemyStateManager.Player.transform.position - enemyStateManager.transform.position).normalized;

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
        enemyStateManager.gameObject.transform.localScale = new Vector3(1, 1,1);
        enemyStateManager.SwitchToIdle(timeBeforeAttack);
    }
}
