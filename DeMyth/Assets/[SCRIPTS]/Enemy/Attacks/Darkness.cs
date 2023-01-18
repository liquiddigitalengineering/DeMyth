using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "DarknessAttack", menuName = "Attacks/DarknessAttack")]
public class Darkness : EnemyBaseState
{
    [Min(0)] [SerializeField] private float attackDuration;
    [SerializeField] private int timeBeforeNextAttack;

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        Debug.Log("Hello from darkness");
        Light2D light = enemyStateManager.GetLight2D;


        yield return new WaitForSeconds(attackDuration);
        ExitState(enemyStateManager);
    }


    public override void UpdateState(EnemyStateManager enemyStateManager) { return; }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager) { }


    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        Light2D light = enemyStateManager.GetLight2D;

        EnableLight(light);
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }


    private IEnumerator EnableLight(Light2D light2D)
    {
        while (light2D.intensity < 1 ) {
            yield return new WaitForSeconds(0.5f);
            light2D.intensity += 0.1f + Time.deltaTime;
            Debug.Log("Hello?");
        }      
    }

    private IEnumerator DisableLight(Light2D light2D)
    {
        Debug.Log("Hello?");
        while (light2D.intensity > 0) {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Hello?");
            light2D.intensity -= 0.1f + Time.deltaTime;
        }
    }
}
