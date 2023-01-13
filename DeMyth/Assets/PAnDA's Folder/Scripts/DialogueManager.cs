using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    


    [Header("Dialogue UI")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText; // the text is the a child of the panel

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }


    [Header("Choices UI")]
    [SerializeField] GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    private void Awake()
    {
        if(instance != null) { Debug.LogWarning("found more than one Dialogue Manager in the scene"); }
        instance = this;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //get allof the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int i = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[i] = choice.GetComponentInChildren<TextMeshProUGUI>();
            i++;
        }

    }

    private void Update()
    {
        // return right away if dialogue is not playing 
        if(!dialogueIsPlaying) { return; }

        if(Input.GetKeyDown(KeyCode.Space)) { ContinueDialogue(); }
        
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueDialogue();
    }

    private void ContinueDialogue()
    {
        if (currentStory.canContinue) 
        {
            //set the text for the current dialogue line 
            dialogueText.text = currentStory.Continue();  // the continue works like a stuck 
            // display choices,if any ,for this dialogue
            DisplayChoices();
        }
        else { ExitDialogue(); }
    }

    private void ExitDialogue()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // check that we dont have less UI than our choices 
        if (currentChoices.Count > choices.Length) { Debug.LogWarning("you have less ui choices than the story choices." + "story choices:" + currentChoices.Count + "ui choices:" + choices.Length); }

        int index = 0;
        //enable and initialize the choices up to the amount of choices for this line of dialogue 
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;    
        }
        //go through the remaining choices the ui supports and make sure they are hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoices());

    }
    private IEnumerator SelectFirstChoices()
    {
        //event system needs to be cleared first then wait
        //for at least one frame before we set the current selected object 

        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }


}
