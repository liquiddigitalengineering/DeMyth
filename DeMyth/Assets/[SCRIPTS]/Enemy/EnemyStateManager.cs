using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public GameObject Player { get => player; }
    [SerializeField] private GameObject player;

    [SerializeField] private Idle idle;

    private EnemyBaseState currentScene;

    private void Awake()
    {
        currentScene = idle;

        currentScene.EnterState(this, 0);
    }


    public void SwitchStates(EnemyBaseState state)
    {
        currentScene = state;
        state.EnterState(this, 0);
    }

    public void SwitchToIdle(int time)
    {
        currentScene = idle;
        currentScene.EnterState(this, time);
    }
}
