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
    public LevelManager levelManager;
    public float zombiesKilled = 0;
    public float peopleKilled = 0;

    int numberOfZombiesTotal;
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
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Start()
    {
        zombieNoncomplianceRate.text = "Zombie Noncompliance: " + randomness * 100 + "%";
        numberOfZombiesTotal = this.GetComponent<SpawnManager>().numberOfSpawns * this.GetComponent<SpawnManager>().zombiesToSpawn;
    }

    private void Update()
    {
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
            Debug.Log("ha he");
            EndGame();
            return;
        }
    }

    public void RestartLevel()
    {
        int levelNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelNum);
    }

    public void EndGame()
    {
        currentGameState = GameState.END;
        levelEndScreen.SetActive(true);
        Vector2 finalRatio = new Vector2(peopleKilled, zombiesKilled);
        levelManager.SaveScore(level, finalRatio);
    }

    #region UPDATING UI STUFF
    public void UpdateZombiesKilled()
    {
        zombiesKilled++;
        zombiesKilledText.text = "Zombies Destroyed: " + zombiesKilled;
        ratio = peopleKilled / numberOfZombiesTotal;
        ratio = ratio / 1;
        humanDeathsImage.fillAmount = ratio;
    }
    public void UpdatePeopleKilled()
    {
        peopleKilled++;
        peopleKilledText.text = "People Killed: " + peopleKilled;
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
        Time.timeScale = 0;
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
