using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    LevelSelectScreen lvlSelect;
    public Vector2 anchorPoint;
    public bool isActive;

    public int levelNumber;

    private void Start()
    {
        lvlSelect = GetComponentInParent<LevelSelectScreen>();
    }

    public void SetLevelButtonActive()
    {
        GetComponent<Button>().interactable = true;
    }
    public void TriggerZoomIn()
    {
        lvlSelect.ZoomOnPoint(anchorPoint, this.GetComponent<RectTransform>().anchoredPosition, levelNumber);
    }

}
