using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] Transform attackPoint;

    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackDamage = 2f;
    [SerializeField] float attackReload = 1f;
    private float currentAttackRange;
    private float currentAttackDamage;
    private float currentAttackReload;

    private float lastAttackTime=0;

    private Animator playerAnimator;
    private void Start()
    {
        lastAttackTime = 0;
        currentAttackRange = attackRange;
        currentAttackDamage = attackDamage;
        currentAttackReload = attackReload;

        playerAnimator = GetComponent<Animator>();


        DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.choiceEvent += DialogueManager_choiceEvent;
    }

    private void DialogueManager_choiceEvent(string obj)
    {    
        if (obj == "Weapon")
        {
            currentAttackRange += 0.2f;
            currentAttackDamage += 1;
            currentAttackReload += 0.5f;
        }    
    }

    private void Update()
    {
        playerAnimator.SetBool("Attack", false);
        if (!CanAttack()) { return; }
        if(!Input.GetKeyDown(KeyCode.LeftShift)) { return; }

        Attack();
        playerAnimator.SetBool("Attack", true);
        lastAttackTime = Time.time + currentAttackReload;
    }


    private void Attack()
    {
        Collider[] objectsHit = Physics.OverlapSphere(attackPoint.position, currentAttackRange); // all the objects that are in the punch radius
        
        foreach (Collider collider in objectsHit)
        {
            //hit an enemy 
            if(collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.OnDamaged(currentAttackDamage);
            }
            //hit something else for now will do nothing 
            else
            {
                return;
            }
        }
  
    }

    private bool CanAttack()
    {      
        if(Time.time > lastAttackTime)
            return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        //a visual of the attack while in the inspector
        Gizmos.DrawWireSphere(attackPoint.position, currentAttackRange);   
    }
}
