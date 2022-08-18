using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectDetailsPanel : MonoBehaviour
{
    public DataManager data;

    public Image levelImage;
    public TMP_Text levelTitle;
    public TMP_Text peopleKilledNumber;
    public TMP_Text zombiesKilledNumber;

    public int baseLevelNumber;
    public int buildIndexToLoad;

    public void LoadData()
    {
        levelTitle.text = data.levelNames[baseLevelNumber];
        peopleKilledNumber.text = data.scores[baseLevelNumber].x.ToString();
        zombiesKilledNumber.text = data.scores[baseLevelNumber].y.ToString();
        levelImage.sprite = data.levelScreenshot[baseLevelNumber];
        buildIndexToLoad = data.levelBuildIndex[baseLevelNumber];
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(buildIndexToLoad);
    }
}
