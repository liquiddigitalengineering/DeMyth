using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level_", menuName ="Levels/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public int LevelIndex;
    public bool LevelCompleted;
}



