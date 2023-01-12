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


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        //doesnt allow the player to move while he chats with a NPC
        if(DialogueManager.instance.dialogueIsPlaying) { rb.velocity = Vector2.zero; return; }

        Inputs();
        
        Move();
        
    }

  

    private void Move()
    {
        Vector2 moveVector2D;
        moveVector2D = (transform.up * verticalInput + transform.right * horizontalInput);

        rb.velocity = moveVector2D * playerSpeed * 1000 * Time.deltaTime;



        /* Movement with MovePostion*/

        //Vector2 playerPosition2D;
        //playerPosition2D = new Vector2(transform.position.x, transform.position.y);
        //rb.MovePosition(playerPosition2D + moveVector2D * playerSpeed * Time.deltaTime);
    }

    private void Inputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }
}
