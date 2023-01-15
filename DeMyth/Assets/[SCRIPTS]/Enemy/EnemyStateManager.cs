using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public static Action<bool> OutOfRangeEvent;

    public SkeletonMecanim Skeleton { get => skeletonMecanim; }
    public Animator Anim { get => anim; }
    public GameObject Player { get => player; }
    public GameObject GraveStone { get => graveStone; }
    public Rigidbody2D Rb { get => rb; }
    public Collider2D Col { get => col; }
    public bool InRange { get; private set; }

    [SerializeField] private GameObject player;
    [SerializeField] private EnemyBaseState initialState;
    [SerializeField] private GameObject graveStone;
    [SerializeField] private Animator anim;
    [SerializeField] private SkeletonMecanim skeletonMecanim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private EnemyBaseState currentScene;

    private void OnEnable()
    {
        Idle.OnStateChange += SwitchStates;
    }

    private void OnDisable()
    {
        Idle.OnStateChange -= SwitchStates;
    }

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
        graveStone.SetActive(false);
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
        if (collision.CompareTag("Player")) {
            InRange = true;
            OutOfRangeEvent?.Invoke(true);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) {
            OutOfRangeEvent?.Invoke(false);
            InRange = false;
        }
           
            
    }
    #endregion
}
