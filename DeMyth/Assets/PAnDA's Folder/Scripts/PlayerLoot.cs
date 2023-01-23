using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoot : MonoBehaviour
{
    private int currentHealthPotions;
    private bool lantern = false;
    private bool weapon = false;
    void Start()
    {
        lantern = false;
        weapon = false;
        DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.choiceEvent += DialogueManager_choiceEvent;
    }

    private void DialogueManager_choiceEvent(string obj)
    {   
        if(obj == "HealthPotion")
        {
            currentHealthPotions +=1;
            return;
        }
        if(obj == "Weapon")
        {
            weapon = true;
            return;
        }
        if(obj == "Lantern")
        {
            lantern |= true;    
            return;
        }
    }

}
