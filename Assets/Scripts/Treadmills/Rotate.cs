using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate : MonoBehaviour
{
    public bool isClockwise;
    public Treadmill tm;
    public GameObject indicator;

    private void Awake()
    {
        tm = this.GetComponentInParent<Treadmill>();
    }
    public void OnMouseExit()
    {
        indicator.SetActive(false);
    }
    public void OnMouseOver()
    {
        indicator.SetActive(true);
    }
    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (isClockwise)
        {
            tm.currentDirection += 90;
            if (tm.currentDirection == 360)
            {
                tm.currentDirection = 0;
            }
            tm.Rotate();
            return;
        }
        else
        {
            tm.currentDirection -= 90;
            if (tm.currentDirection == -90)
            {
                tm.currentDirection = 270;
            }
            tm.Rotate();
            return;
        }
    }
}
