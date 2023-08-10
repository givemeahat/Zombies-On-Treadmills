using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { PLANNING, RUNNING, END };
    public GameState currentGameState = GameState.PLANNING;

    public string levelName;
    public int level;
    public GameObject levelTitle;
    public Text levelTitleText;
    public GameObject mallLevelTitleAccent;
    public Text frenzyTitleText;
    public DataManager dataManager;
    public GameObject lvlMgrPrefab;
    public float zombiesKilled = 0;
    public float peopleKilled = 0;

    public Text ratioZombiesKilledText;
    public Text ratioPeopleKilledText;

    public int numberOfZombiesTotal;
    public float ratio;

    public Text zombiesKilledText;
    public Text peopleKilledText;
    public Image humanDeathsImage;

    public float randomness;
    public Text zombieNoncomplianceRate;

    public GameObject menu;
    public GameObject levelEndScreen;

    public List<GameObject> zombiesInScene;
    float currentTimeScale = 1;

    public bool isTutorial;

    public GameObject guideBook;
    public GameObject[] guideBookPages;
    public Sprite[] missingPages;

    public GameObject slider;
    public Toggle muteMusicToggle;
    public Toggle isFullScreenMode;

    public Sprite neighborhoodLevelTitleImg;
    public Sprite mallLevelTitleImg;


    private void Awake()
    {
        Time.timeScale = 1;
        if (GameObject.FindGameObjectWithTag("LevelManager"))
        {
            dataManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DataManager>();
        }
        else
        {
            Instantiate(lvlMgrPrefab);
            dataManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DataManager>();
        }
        //WipeGame();
        //slider.GetComponent<Slider>().value = dataManager.volumeLevel;
        //AudioListener.volume = slider.GetComponent<Slider>().value;
        dataManager.currentLevel = level;
        dataManager.UnlockLevels();
        levelTitleText.text = levelName;
        isFullScreenMode.isOn = Screen.fullScreen;
        //isFullScreenMode.isOn = dataManager.isFullScreen;
        if (this.gameObject.GetComponent<SpawnManager>().isFrenzy)
        {
            frenzyTitleText.gameObject.SetActive(true);
        }
        if (!levelTitle.activeInHierarchy && !isTutorial) levelTitle.SetActive(true);
        if (level < 9)
        {
            //if (mallLevelTitleAccent.activeInHierarchy) mallLevelTitleAccent.SetActive(false);
            levelTitle.GetComponent<Image>().sprite = neighborhoodLevelTitleImg;
            levelTitleText.color = Color.white;
        }
        if (level >= 9)
        {
            //if (!mallLevelTitleAccent.activeInHierarchy) mallLevelTitleAccent.SetActive(true);
            levelTitle.GetComponent<Image>().sprite = mallLevelTitleImg;
            if (level == 9 || level == 10 || level == 11) levelTitleText.color = new Color (0.4196079f, 0.8274511f, 1, 1);
            if (level == 12 || level == 13) levelTitleText.color = new Color(1, 0.4196079f, 0.8002242f, 1);
            if (level == 14 || level == 15) levelTitleText.color = new Color(1, 0.8765869f, 0.4196079f, 1);
            if (level == 16 || level == 17) levelTitleText.color = new Color(0.5961097f, 0.9245283f, 0.3968494f, 1);

        }
        if (!isTutorial) StartCoroutine(CloseLevelTitle());
    }
    private void Start()
    {
        //zombieNoncomplianceRate.text = "Zombie Deviance: " + randomness * 100 + "%";
        if (this.GetComponent<SpawnManager>().isFrenzy)
        {
            numberOfZombiesTotal = this.GetComponent<SpawnManager>().numberOfSpawns * this.GetComponent<SpawnManager>().zombiesToSpawn;
        }
        else
        {
            numberOfZombiesTotal = this.GetComponent<SpawnManager>().zombieSpawners.Count * this.GetComponent<SpawnManager>().zombiesToSpawn;
        }

        if (dataManager.musicIsMuted) muteMusicToggle.isOn = true;
        if (dataManager.isFullScreen) isFullScreenMode.isOn = true;
        //slider.GetComponent<Slider>().value = dataManager.volumeLevel;
    }
    private void Update()
    {
        if (dataManager == null)
        {
            Instantiate(lvlMgrPrefab);
            dataManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DataManager>();
            dataManager.currentLevel = level;
            dataManager.UnlockLevels();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            if (!menu.activeInHierarchy)
            {
                menu.SetActive(true);
            }
        }
        if (slider.GetComponent<Slider>().value != dataManager.volumeLevel)
        {
            slider.GetComponent<Slider>().value = dataManager.volumeLevel;
        }
        if (Input.GetKeyDown(KeyCode.X)) WipeGame();
    }
    public void MuteMusic()
    {
        dataManager.MuteMusic(muteMusicToggle.isOn);
    }
    public void ToggleFS()
    {
        if (isFullScreenMode.isOn) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        else Screen.fullScreenMode = FullScreenMode.Windowed;
        dataManager.isFullScreen = isFullScreenMode.isOn;
        dataManager.SavePlayer();
    }
    public void RemoveLevelTitle()
    {
        StartCoroutine(CloseLevelTitle());
    }
    public IEnumerator CloseLevelTitle()
    {
        yield return new WaitForSeconds(2.5f);
        levelTitleText.gameObject.SetActive(false);
    }
    public void CheckZombies()
    {
        if (zombiesInScene.Count > 0)
        {
            return;
        }
        if (zombiesInScene.Count == 0)
        {
            EndGame();
            return;
        }
    }

    public void RestartLevel()
    {
        int levelNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelNum);
    }
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(dataManager.currentLevel != 0)
            {
                dataManager.LoadLevel(dataManager.levelBuildIndex[dataManager.currentLevel]);
                return;
            }
        }
        int levelNum = SceneManager.GetActiveScene().buildIndex + 1;
        dataManager.LoadLevel(levelNum);
    }
    public void OpenLevelSelect()
    {
        dataManager.OpenLevelSelect();
    }
    public void GoToIntro()
    {
        dataManager.LoadLevel(1);
    }
    public void EndGame()
    {
        currentGameState = GameState.END;
        dataManager.UnlockLevel();
        levelEndScreen.SetActive(true);
        Vector2 finalRatio = new Vector2(peopleKilled, zombiesKilled);
        dataManager.SaveScore(level, finalRatio);
    }

    public void UpdateTornPage()
    {
        guideBookPages[5].GetComponent<Image>().sprite = missingPages[1];
        guideBookPages[6].GetComponent<Image>().sprite = missingPages[0];
        dataManager.hasUpdatedGuidebook = true;
    }

    #region SAVE SYSTEM
    public void SaveGame()
    {
        dataManager.SavePlayer();
    }
    public void LoadPlayer()
    {
        dataManager.LoadPlayer();
    }
    public void WipeGame()
    {
        dataManager.WipeData();
    }
    public void QuitGame()
    {
        dataManager.SavePlayer();
        Application.Quit();
    }
    #endregion

    #region UPDATING UI STUFF
    public void UpdateZombiesKilled()
    {
        zombiesKilled++;
        zombiesKilledText.text = "Zombies Destroyed: " + zombiesKilled;
        ratioZombiesKilledText.text = "" + zombiesKilled;
        ratio = peopleKilled / numberOfZombiesTotal;
        ratio = ratio / 1;
        humanDeathsImage.fillAmount = ratio;
    }
    public void UpdatePeopleKilled()
    {
        peopleKilled++;
        peopleKilledText.text = "People Killed: " + peopleKilled;
        ratioPeopleKilledText.text = ""+peopleKilled;
        ratio = peopleKilled / numberOfZombiesTotal;
        ratio = ratio / 1;
        humanDeathsImage.fillAmount = ratio;
    }
    public void UpdateVolume()
    {
        dataManager.volumeLevel = slider.GetComponent<Slider>().value;
        AudioListener.volume = slider.GetComponent<Slider>().value;
        dataManager.SavePlayer();
    }
    #endregion

    #region TIME STUFF
    public void ResumeGame()
    {
        Time.timeScale = currentTimeScale;
    }
    public void PauseGame()
    {
        //Time.timeScale = 0;
    }
    public void NormalTimeMode()
    {
        Time.timeScale = 1;
        currentTimeScale = 1;
    }
    public void DoubleTimeMode()
    {
        Time.timeScale = 2;
        currentTimeScale = 2;
    }
    public void QuadTimeMode()
    {
        Time.timeScale = 4;
        currentTimeScale = 4;
    }
    #endregion
}
