using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private List<LevelInfo> levels;
    [SerializeField] private List<Button> levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        CheckLevels();
    }

    private void CheckLevels()
    {
        for (int i = 0; i < levels.Count; i++) {
            if (!levels[i].LevelCompleted) continue;

            levelButtons[i].interactable = true;
        }
    }
}
