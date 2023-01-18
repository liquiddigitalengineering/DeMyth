using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyStateManager : MonoBehaviour
{
    /// <summary>
    /// Is triggered whenever the player leaves or enters enemy's range zone
    /// </summary>
    public static Action<bool> OnRangeChanged;

    public SkeletonMecanim Skeleton { get => skeletonMecanim; }
    public Animator Anim { get => anim; }
    public GameObject Player { get => player; }
    public GameObject GraveStone { get => graveStone; }
    public Rigidbody2D Rb { get => rb; }
    public Collider2D Col { get => col; }
    public bool InRange { get; private set; }
    public DamageManager GetDamageManager { get => damageManager; }
    public Light2D GetLight2D { get => light2D; }

    [SerializeField] private GameObject player;
    [SerializeField] private EnemyBaseState initialState;
    [SerializeField] private GameObject graveStone;
    [SerializeField] private Animator anim;
    [SerializeField] private SkeletonMecanim skeletonMecanim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private DamageManager damageManager;
    [SerializeField] private Light2D light2D;

    private EnemyBaseState currentState;

    private void OnEnable()
    {
        Idle.OnStateChanged += SwitchStates;
    }

    private void OnDisable()
    {
        Idle.OnStateChanged -= SwitchStates;
    }

    private void Start()
    {
        currentState = initialState;
        StartCoroutine(currentState.EnterState(this, 1));
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchStates(EnemyBaseState state)
    {
        graveStone.SetActive(false);
        currentState = state;
        StartCoroutine(state.EnterState(this, 1));
    }

    public void SwitchToIdle(int time)
    {
        currentState = initialState;
        StartCoroutine(currentState.EnterState(this, time));
    }

    #region triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            InRange = true;
            OnRangeChanged?.Invoke(true);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) {
            OnRangeChanged?.Invoke(false);
            InRange = false;
        }
           
            
    }
    #endregion
}
