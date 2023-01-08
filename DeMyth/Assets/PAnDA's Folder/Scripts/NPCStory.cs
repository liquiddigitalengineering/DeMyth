using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class NPCStory : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;

    private Queue<string> sentences;
    private bool insideTrigger= false;

    private void Start()
    {
        sentences = new Queue<string>(); 
        
        if(TryGetComponent<NPC>(out NPC npc))
        {
            dialogue.npcName = npc.npcName;
        }
    }

    private void Update()
    {
        if(!insideTrigger) { return; }
            if (!Input.GetKeyDown(KeyCode.E)) { return; }
                if (FindObjectOfType<DialogueManager>().GetInsideDialogue()) {  return; }
                    StartDialogue();
    }

    // if the player doesnt move it doesnt consider him inside the collider
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag != "Player") { return; }
        insideTrigger = true;
    }

    private void StartDialogue()
    {
       
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        insideTrigger = false;
    }


}
