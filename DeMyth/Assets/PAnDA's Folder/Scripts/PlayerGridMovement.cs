using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGridMovement : MonoBehaviour
{
    private GameManager gameManager;
    Grid currentGrid;
    private float cellSize;

    [SerializeField] Vector2 startingPoint;

    [SerializeField] float playerSpeed=1;
    [SerializeField] float movementCooldown=0.1f;
    private bool moving;

    private bool canMove;
    [SerializeField] LayerMask rayCanHitLayer;
    [SerializeField] float raycastRadius = 0.2f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentGrid = gameManager.grid;
        cellSize = gameManager.cellSize;

        if (startingPoint.x > currentGrid.GetWidth() || startingPoint.y > currentGrid.GetHeight()) { Debug.LogError("your starting point does fit the grid"); return; }

        transform.position = startingPoint;
    }

    private void Update()
    {
        if(moving) { return; }
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 direction;

        if(Input.GetKeyDown(KeyCode.D) && transform.position.x < currentGrid.GetWidth())
        {
            direction = new Vector3(cellSize * playerSpeed, 0, 0);
            if (!CanMove(direction)) { return; }
            transform.position += direction;
            moving = true;
            StartCoroutine(MovementCooldown());

        }
           
        else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > 0)
        {
            direction = new Vector3(-cellSize * playerSpeed, 0, 0);
            if (!CanMove(direction)) { return; }
            transform.position += direction;
            moving = true;
            StartCoroutine(MovementCooldown());

        }
           
        else if (Input.GetKeyDown(KeyCode.W) && transform.position.y < currentGrid.GetHeight())
        {
            direction = new Vector3(0, cellSize * playerSpeed, 0);
            if (!CanMove(direction)) { return; }
            transform.position += direction;
            moving = true;
            StartCoroutine(MovementCooldown());

        }
            
        else if (Input.GetKeyDown(KeyCode.S) && transform.position.y > 0)
        {
            direction = new Vector3(0, -cellSize * playerSpeed, 0);
            if (!CanMove(direction)) { return; }
            transform.position += direction;
            moving = true;
            StartCoroutine(MovementCooldown());

        }
        

    }

    bool CanMove(Vector3 direction)
    {

        RaycastHit2D hit = Physics2D.CircleCast(transform.position + new Vector3 (0,0.5f,0), raycastRadius, direction, direction.magnitude, rayCanHitLayer);
        if (hit.collider !=null)
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.tag == "UnpushableObject")
            {
                 return false;
            }
            else if (hit.transform.tag == "PushableObject")
            {
                Debug.Log("i am in the pushable object i pushed it to :" + direction);
                hit.transform.position += direction;
                return true;
            }
            
        }
        return true;
    }


    IEnumerator MovementCooldown()
    {
        yield return new WaitForSeconds(movementCooldown);
        moving = false;
    }

}

