using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentLevel;
    public float[] humanDeaths;
    public float[] zombieDeaths;
    public float volumeLevel = 1;
    public bool hasFinishedTutorial;
    public bool hasUpdatedGuidebook;
    public bool musicIsMuted;
    public bool isFullScreen = true;

    public PlayerData(DataManager lm)
    {
        humanDeaths = new float[10];
        zombieDeaths = new float[10];
        currentLevel = lm.currentLevel;
        isFullScreen = lm.isFullScreen;
        hasFinishedTutorial = lm.hasFinishedTutorial;
        hasUpdatedGuidebook = lm.hasUpdatedGuidebook;
        volumeLevel = lm.volumeLevel;
        musicIsMuted = lm.musicIsMuted;
        for (int i = 0; i < lm.scores.Length; i++)
        {
            humanDeaths[i] = lm.scores[i].x;
            zombieDeaths[i] = lm.scores[i].y;
        }
    }

}
