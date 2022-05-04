using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int zombiesKilled = 0;
    public int peopleKilled = 0;

    public Text zombiesKilledText;
    public Text peopleKilledText;

    public float randomness;

    public void RestartLevel()
    {
        int levelNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelNum);
    }

    public void UpdateZombiesKilled()
    {
        zombiesKilled++;
        zombiesKilledText.text = "Zombies Killed: " + zombiesKilled;
    }
    public void UpdatePeopleKilled()
    {
        peopleKilled++;
        peopleKilledText.text = "People Killed: " + peopleKilled;
    }
    public void WinGame()
    {

    }

    public void LoseGame()
    {

    }
}
