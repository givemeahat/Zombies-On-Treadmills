using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { PLANNING, RUNNING, END };
    public GameState currentGameState = GameState.PLANNING;

    public int zombiesKilled = 0;
    public int peopleKilled = 0;

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
    }

    public void RestartLevel()
    {
        int levelNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelNum);
    }

    public void UpdateZombiesKilled()
    {
        zombiesKilled++;
        zombiesKilledText.text = "Zombies Killed: " + zombiesKilled;
        float ratio = peopleKilled / zombiesKilled;
        humanDeathsImage.fillAmount = ratio;
    }
    public void UpdatePeopleKilled()
    {
        peopleKilled++;
        peopleKilledText.text = "People Killed: " + peopleKilled;
        float ratio = peopleKilled / zombiesKilled;
        humanDeathsImage.fillAmount = ratio;
    }
    public void EndGame()
    {
        currentGameState = GameState.END;
        levelEndScreen.SetActive(true);
    }
}
