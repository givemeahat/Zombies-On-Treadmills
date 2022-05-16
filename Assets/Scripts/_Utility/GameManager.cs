using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { PLANNING, RUNNING, END };
    public GameState currentGameState = GameState.PLANNING;

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

    private void Start()
    {
        zombieNoncomplianceRate.text = "Zombie Noncompliance: " + randomness * 100 + "%";
        numberOfZombiesTotal = this.GetComponent<SpawnManager>().numberOfSpawns * this.GetComponent<SpawnManager>().zombiesToSpawn;
    }

    public void RestartLevel()
    {
        int levelNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelNum);
    }

    public void UpdateZombiesKilled()
    {
        zombiesKilled++;
        //zombiesKilledText.text = ""+zombiesKilled;
        ratio = peopleKilled / numberOfZombiesTotal;
        ratio = ratio / 1;
        humanDeathsImage.fillAmount = ratio;
    }
    public void UpdatePeopleKilled()
    {
        peopleKilled++;
        //peopleKilledText.text = ""+peopleKilled;
        ratio = peopleKilled / numberOfZombiesTotal;
        ratio = ratio / 1;
        humanDeathsImage.fillAmount = ratio;
    }
    public void EndGame()
    {
        currentGameState = GameState.END;
        levelEndScreen.SetActive(true);
    }
}
