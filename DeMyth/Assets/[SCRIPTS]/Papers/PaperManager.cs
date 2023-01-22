using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    [SerializeField] private LevelWithPaper levelInfo;
    [SerializeField] private GameObject paperObject;
    [SerializeField] private PaperQuest paperQuest;

    private void OnEnable()
    {
        CollectPaper.OnPaperCollected += PaperCollected;
    }

    private void OnDisable()
    {
        CollectPaper.OnPaperCollected -= PaperCollected;
    }

    private void Awake()
    {
        if (!paperQuest.QuestActivated || levelInfo.PaperCollected || paperObject == null) return;

        paperObject.SetActive(true);
    }

    private void PaperCollected()
    {
        levelInfo.PaperCollected = true;
        paperQuest.PaperCollected();

        paperObject.SetActive(false);
    }
}
