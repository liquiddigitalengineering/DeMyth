using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private OtherLevelData otherData;
    [SerializeField] private GameObject latern;

    private void Awake()
    {
        Time.timeScale = 1;

        levelText.text = levelInfo.LevelIndex.ToString();

        if (!otherData.HasLatern) return;

        latern.SetActive(true);
    }

    public void LevelCompleted()
    {
        levelInfo.LevelCompleted = true;
    }
}