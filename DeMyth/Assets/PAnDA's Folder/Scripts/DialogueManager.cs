using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI npcName;


    [SerializeField] Animator animator;

    private bool insideDialogue=false; 
    public bool GetInsideDialogue() { return insideDialogue; }
    private void Start()
    {
        sentences = new Queue<string>();    
    }

    public void StartDialogue(Dialogue dialogue)
    {
        insideDialogue = true;

        sentences.Clear();

        animator.SetBool("IsOpen",true);

        npcName.text = dialogue.npcName;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }

    public void EndDialogue()
    {
        insideDialogue = false;
        animator.SetBool("IsOpen", false);
    }

}
