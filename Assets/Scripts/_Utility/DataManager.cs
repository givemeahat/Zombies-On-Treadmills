using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public Vector2[] scores;
    public string[] levelNames;
    public int[] levelBuildIndex;
    public Sprite[] levelScreenshot;
    public Button[] levelButtons;

    public GameObject levelSelectScreen;
    public int currentLevel;

    public GameObject loadingScreen;
    public Button closeButton;
    public bool isTemp;
    public bool hasFinishedTutorial;
    public bool hasUpdatedGuidebook;
    bool destroy;
    public float volumeLevel;

    void Start()
    {
        DontDestroyOnLoad(this);
        LoadPlayer();
    }
    public void SaveScore(int levelNum, Vector2 ratio)
    {
        scores[levelNum] = ratio;
    }
    public void OpenLevelSelect()
    {
        //Open level select
        levelSelectScreen.SetActive(true);
        levelSelectScreen.GetComponentInChildren<LevelSelectScreen>().ResetScreen();
        if (levelSelectScreen.GetComponentInChildren<LevelSelectScreen>().detailsPanel.gameObject.activeInHierarchy)
            levelSelectScreen.GetComponentInChildren<LevelSelectScreen>().detailsPanel.gameObject.SetActive(false);
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
    public void UnlockLevel()
    {
        if (!levelButtons[currentLevel + 1].interactable)
        {
            levelButtons[currentLevel + 1].interactable = true;
        }
    }
    public void UnlockLevels()
    {
        for (var x = 0; x < currentLevel + 1; x++)
        {
            levelButtons[x].interactable = true;
        }
    }
    public void LoadLevel(int index)
    {
        if (isTemp) destroy = true;
        else destroy = false;
        if (levelSelectScreen.activeInHierarchy) StartCoroutine(CloseLvlSelect());
        StartCoroutine(TransitionToScene(index));
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Saving Player");
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            Debug.Log("beepbop");
            SavePlayer();
            return;
        }
        currentLevel = data.currentLevel;
        hasFinishedTutorial = data.hasFinishedTutorial;
        volumeLevel = data.volumeLevel;
        GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().slider.GetComponent<Slider>().value = volumeLevel;
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].x = data.humanDeaths[i];
            scores[i].y = data.zombieDeaths[i];
        }
        //scores = data.scores;
        Debug.Log("has loaded player");
    }
    public void WipeData()
    {
        SaveSystem.WipePlayer();
        Debug.Log("bye bye");
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
    IEnumerator TransitionToScene(int index)
    {
        
        SavePlayer();
        Time.timeScale = 1f;
        loadingScreen.GetComponent<Image>().material.SetVector("_HalftonePosition", new Vector4(0, .5f, 0, 0));
        yield return new WaitForSeconds(.2f);
        loadingScreen.SetActive(true);
        float startValue = 2f;
        float endValue = 0f;
        float time = 0;
        float waitTime = 1f;
        while (time < waitTime)
        {
            time += Time.deltaTime;
            float fade = Mathf.Lerp(startValue, endValue, time/waitTime);
            loadingScreen.GetComponent<Image>().material.SetFloat("_HalftoneFade", fade);
            yield return null;
        }
        loadingScreen.GetComponent<Image>().material.SetVector("_HalftonePosition", new Vector4 (1, .5f, 0, 0));
        SceneManager.LoadScene(index);
        startValue = 0f;
        endValue = 2f;
        time = 0f;
        waitTime = 1f;
        while (time < waitTime)
        {
            time += Time.deltaTime;
            float fade = Mathf.Lerp(startValue, endValue, time / waitTime);
            loadingScreen.GetComponent<Image>().material.SetFloat("_HalftoneFade", fade);
            yield return null;
        }
        loadingScreen.SetActive(false);
        if (destroy)
            Destroy(this.gameObject);
    }
}
