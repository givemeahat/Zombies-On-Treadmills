using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentLevel;
    public Vector2[] scores;
    public bool hasUpdatedGuidebook;

    public PlayerData(DataManager lm)
    {
        currentLevel = lm.currentLevel;
        scores = lm.scores;
        hasUpdatedGuidebook = lm.hasUpdatedGuidebook;
    }

}
