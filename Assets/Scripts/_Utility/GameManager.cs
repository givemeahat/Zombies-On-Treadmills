using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int zombiesKilled = 0;
    public int peopleKilled = 0;

    public Text zombiesKilledText;
    public Text peopleKilledText;

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
