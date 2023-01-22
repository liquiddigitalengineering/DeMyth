using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsList", menuName = "Levels/LevelsList")]
public class LevelsList : ScriptableObject
{
    public List<LevelInfo> Levels;
}

