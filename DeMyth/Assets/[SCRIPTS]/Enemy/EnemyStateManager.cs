using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public static Action<bool> OutOfRangeEvent;

    public GameObject Player { get => player; }
    public GameObject GraveStone { get => graveStone; }
    public bool InRange { get => playerInRange; }

    [SerializeField] private GameObject player;
    [SerializeField] private EnemyBaseState initialState;
    [SerializeField] private GameObject graveStone;

    private EnemyBaseState currentScene;
    private bool playerInRange = false;

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

    #region triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            OutOfRangeEvent?.Invoke(true);
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //        playerInRange = true;
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) {
            playerInRange = false;

            OutOfRangeEvent?.Invoke(false);
        }
            
    }
    #endregion
}
