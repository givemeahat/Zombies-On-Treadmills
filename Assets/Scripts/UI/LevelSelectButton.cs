using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    public Vector2 anchorPoint;
    public void TriggerZoomIn()
    {
        GetComponentInParent<LevelSelectScreen>().ZoomOnPoint(anchorPoint, this.GetComponent<RectTransform>().anchoredPosition);
    }
}
