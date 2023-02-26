using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelButtons;

    // Start is called before the first frame update
    void Awake()
    {
        CheckLevels();
    }

    private void CheckLevels()
    {
        for (int i = 0; i < MainMenu.LastPlayedLevelID; i++) {
            levelButtons[i].SetActive(true);
        }
    }
}
