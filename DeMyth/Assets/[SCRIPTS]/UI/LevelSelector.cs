using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private LevelsList levels;
    [SerializeField] private List<Button> levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        CheckLevels();
    }

    private void CheckLevels()
    {
        for (int i = 0; i < levels.Levels.Count; i++) {
            if (!levels.Levels[i].LevelCompleted) continue;

            levelButtons[i].interactable = true;
        }
    }
}
