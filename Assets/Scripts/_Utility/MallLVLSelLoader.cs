using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MallLVLSelLoader : MonoBehaviour
{
    public DataManager data;

    public int baseLevelNumber;
    public int buildIndexToLoad;

    public void LoadData()
    {
        buildIndexToLoad = data.levelBuildIndex[baseLevelNumber];
    }
    public void LoadLevel()
    {
        data.LoadLevel(buildIndexToLoad);
    }
}
