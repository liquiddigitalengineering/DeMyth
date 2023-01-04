using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public GameObject Player { get => player; }
    public int Health { get => health; }

    [Header("Health things")]
    [SerializeField] private int health;

    [SerializeField] private GameObject player;
    [SerializeField] private EnemyBaseState initialState;


    private EnemyBaseState currentScene;

    private void Awake()
    {
        currentScene = initialState;

        StartCoroutine(currentScene.EnterState(this, 0));
    }

    private void Update()
    {
        currentScene.UpdateState(this);
    }

    public void SwitchStates(EnemyBaseState state)
    {
        currentScene = state;
        StartCoroutine(state.EnterState(this, 0));
    }

    public void SwitchToIdle(int time)
    {
        currentScene = initialState;
        StartCoroutine(currentScene.EnterState(this, time));
    }
}
