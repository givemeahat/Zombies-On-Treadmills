using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public Vector2[] scores;
    public string[] levelNames;
    public int[] levelBuildIndex;
    public Sprite[] levelScreenshot;

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
        levelSelectScreen.GetComponent<Animator>().Play("LevelSelect_Open");
    }
    public void CloseLevelSelect()
    {
        //Close level select
        //levelSelectScreen.SetActive(false);
        StartCoroutine(CloseLvlSelect());
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

    IEnumerator CloseLvlSelect()
    {
        float time = 0;
        float waitTime = .3f;
        levelSelectScreen.GetComponent<Animator>().Play("LevelSelect_Close");
        while (time < waitTime)
        {
            yield return null;
        }
        levelSelectScreen.gameObject.SetActive(false);
    }
}
