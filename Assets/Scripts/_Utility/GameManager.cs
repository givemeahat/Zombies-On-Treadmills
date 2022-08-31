using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { PLANNING, RUNNING, END };
    public GameState currentGameState = GameState.PLANNING;

    public int level;
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
        dataManager.currentLevel = level;
    }

    private void Start()
    {
        zombieNoncomplianceRate.text = "Zombie Deviance: " + randomness * 100 + "%";
        if (this.GetComponent<SpawnManager>().isFrenzy)
        {
            numberOfZombiesTotal = this.GetComponent<SpawnManager>().numberOfSpawns * this.GetComponent<SpawnManager>().zombiesToSpawn;
        }
        else
        {
            numberOfZombiesTotal = this.GetComponent<SpawnManager>().zombieSpawners.Count * this.GetComponent<SpawnManager>().zombiesToSpawn;
        }
    }

    private void Update()
    {
        if (dataManager == null)
        {
            Instantiate(lvlMgrPrefab);
            dataManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DataManager>();
            dataManager.currentLevel = level;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            if (!menu.activeInHierarchy)
            {
                menu.SetActive(true);
            }
        }
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
        int levelNum = SceneManager.GetActiveScene().buildIndex + 1;
        dataManager.LoadLevel(levelNum);
    }
    public void OpenLevelSelect()
    {
        dataManager.OpenLevelSelect();
    }

    public void EndGame()
    {
        currentGameState = GameState.END;
        dataManager.UnlockLevel();
        levelEndScreen.SetActive(true);
        Vector2 finalRatio = new Vector2(peopleKilled, zombiesKilled);
        dataManager.SaveScore(level, finalRatio);
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

    #endregion

    #region UPDATING UI STUFF
    public void UpdateZombiesKilled()
    {
        zombiesKilled++;
        zombiesKilledText.text = "Zombies Destroyed: " + zombiesKilled;
        ratioZombiesKilledText.text = "Destroyed Zombies: " + zombiesKilled;
        ratio = peopleKilled / numberOfZombiesTotal;
        ratio = ratio / 1;
        humanDeathsImage.fillAmount = ratio;
    }
    public void UpdatePeopleKilled()
    {
        peopleKilled++;
        peopleKilledText.text = "People Killed: " + peopleKilled;
        ratioPeopleKilledText.text = "People Killed: " + peopleKilled;
        ratio = peopleKilled / numberOfZombiesTotal;
        ratio = ratio / 1;
        humanDeathsImage.fillAmount = ratio;
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
