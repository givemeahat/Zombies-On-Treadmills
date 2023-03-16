using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    public Text floorText;
    public Color blue;
    public Color pink;
    public Color yellow;
    public Color green; 

    public void SendFloorInfo(int floorNum)
    {
        if (floorNum == 1 || floorNum == 2) floorText.color = blue;
        if (floorNum == 3 || floorNum == 4) floorText.color = pink;
        if (floorNum == 5 || floorNum == 6) floorText.color = yellow;
        if (floorNum == 7 || floorNum == 8) floorText.color = green;
        floorText.text = "FLOOR " + floorNum;
        Debug.Log("Hi");
    }
}
