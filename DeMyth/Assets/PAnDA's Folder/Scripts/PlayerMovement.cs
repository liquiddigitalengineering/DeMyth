using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float verticalInput;
    private float horizontalInput;

    [Range(0,3)]
    [SerializeField] float playerSpeed = 1f;
    [Header("Knockback info")]
    [SerializeField] float knockbackForce = 1f;
    [SerializeField] float knockbackTime = 1f;

    private bool isKnockbacked = false;
    private Vector2 knockbackPosition;

    private Animator playerAnimator;
    private void OnEnable()
    {
        SpinAttack.PlayerKnockedEvent += KnockBack;
    }
    private void OnDisable()
    {
        SpinAttack.PlayerKnockedEvent -= KnockBack;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (DialogueManager.instance.dialogueIsPlaying) { rb.velocity = Vector2.zero; return; }
        Inputs();
    }

    void FixedUpdate()
    {
        //doesnt allow the player to move while he chats with a NPC
        //if(DialogueManager.instance.dialogueIsPlaying) { rb.velocity = Vector2.zero; return; }

        Move();     
    }

    private void Move()
    {
        Vector2 moveVector2D = (transform.up * verticalInput + transform.right * horizontalInput);
        if (!isKnockbacked) {           
            rb.velocity = moveVector2D * playerSpeed * 1000 * Time.deltaTime;
        }
        else {
            rb.velocity = knockbackPosition.normalized * knockbackForce;
        }
        playerAnimator.SetFloat("Move", moveVector2D.magnitude);
    }

    private void Inputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    #region Knockback
    private void KnockBack(Transform enemyTransform)
    {
        Debug.Log("knockbacked");
        isKnockbacked = true;
        knockbackPosition = transform.position - enemyTransform.position;

        StartCoroutine(KnockbackingCoroutine());
    }

    private IEnumerator KnockbackingCoroutine()
    {      
        yield return new WaitForSeconds(knockbackTime);
        isKnockbacked = false;
    }
    #endregion
}
