using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{

    public void TriggerZoomIn()
    {
        Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        GetComponentInParent<LevelSelectScreen>().ZoomOnPoint(pos);
    }
}
