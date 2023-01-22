using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="quest", menuName ="Quests")]
public class PaperQuest : ScriptableObject
{
    public bool QuestActivated;
    public int PapersCollected;
    public bool QuestCompleted;


    public void PaperCollected()
    {
        PapersCollected++;

        if (PapersCollected == 5)
            QuestCompleted = true;
    }
}
