using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BasicSlamAttack", menuName = "Attacks/BasicSlam")]
public class BasicSlam : EnemyBaseState
{
    public float GetGraveStoneDamage { get => graveStoneDamage; }

    [Min(0)][SerializeField] private float playerFollowTime;
    [SerializeField] private int timeBeforeNextAttack = 1;
    [Header("Grave stone settings")]

    [SerializeField] [Min(9)] private float maxGraveStoneSpeed;
    [SerializeField][Min(0)] private float graveStoneDamage;

    private float timeLeft;
    private Transform playerTransform;
    private bool canBeFollowed = false;


    private void OnEnable()
    {
        AnimationEventsHandler.BasicSlamFinishedEvent += EventMethod;
    }

    private void OnDisable()
    {
        AnimationEventsHandler.BasicSlamFinishedEvent -= EventMethod;
    }

    public override IEnumerator EnterState(EnemyStateManager enemyStateManager, int time)
    {
        enemyStateManager.Anim.SetTrigger("basicSlam");
        timeLeft = playerFollowTime;
        yield return new WaitForSeconds(0.1f);
    }



    public override void UpdateState(EnemyStateManager enemyStateManager)
    {
        if (!canBeFollowed) return;
        if (timeLeft <= 0) ExitState(enemyStateManager);

        ExecuteOperation(enemyStateManager);
        timeLeft -= Time.deltaTime;
    }

    protected override void ExecuteOperation(EnemyStateManager enemyStateManager)
    {
        playerTransform = enemyStateManager.Player.transform;

        enemyStateManager.GraveStone.transform.position = Vector3.MoveTowards(enemyStateManager.GraveStone.transform.position, playerTransform.position, GraveStoneSpeed() * Time.deltaTime);
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.GraveStone.GetComponent<Animator>().SetTrigger("despawn");
        enemyStateManager.GraveStone.SetActive(false);
        enemyStateManager.SwitchToIdle(timeBeforeNextAttack);
    }

    private float GraveStoneSpeed() => Random.Range(8, maxGraveStoneSpeed);

    private void EventMethod(EnemyStateManager enemyStateManager)
    {
        playerTransform = enemyStateManager.Player.transform;
        enemyStateManager.GraveStone.transform.position = playerTransform.position;
        enemyStateManager.GraveStone.SetActive(true);


        canBeFollowed = true;
    }
}
