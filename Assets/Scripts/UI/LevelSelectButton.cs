using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public LevelSelectScreen lvlSelect;
    public Vector2 anchorPoint;
    public bool isActive;

    public int levelNumber;

    private void Start()
    {
        if (!isActive) GetComponent<Button>().interactable = false;
    }

    public void SetLevelButtonActive()
    {
        GetComponent<Button>().interactable = true;
    }
    public void TriggerZoomIn()
    {
        if (lvlSelect.GetComponent<RectTransform>().pivot == anchorPoint) return;
        lvlSelect.ZoomOnPoint(anchorPoint, this.GetComponent<RectTransform>().anchoredPosition, levelNumber);
    }

}
