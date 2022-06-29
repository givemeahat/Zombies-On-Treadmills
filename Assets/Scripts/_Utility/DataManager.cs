using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public Vector2[] scores;
    public GameObject levelSelectScreen;
    public int currentLevel;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void SaveScore(int levelNum, Vector2 ratio)
    {
        scores[levelNum] = ratio;
    }
    public void OpenLevelSelect()
    {
        //Open level select
        levelSelectScreen.SetActive(true);
    }
    public void CloseLevelSelect()
    {
        //Close level select
        levelSelectScreen.SetActive(false);
    }
    public void GoToScene(int sceneNumber)
    {
        SavePlayer();
        SceneManager.LoadScene(sceneNumber);
        //Save game here
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Saving Player");
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        currentLevel = data.currentLevel;
        scores = data.scores;
        Debug.Log("has loaded player");
    }
    public void WipeData()
    {
        SaveSystem.WipePlayer();
    }
}
