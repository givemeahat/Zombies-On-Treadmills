using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DirectoryTextButton : MonoBehaviour//, //IPointerEnterHandler, IPointerExitHandler
{
    public Animator correspondingButtonAnim;

    public int baseLevelNumber;
    public MallLVLSelLoader loader;

    public void SendLevelInfoToLoader()
    {
        loader.baseLevelNumber = baseLevelNumber;
        loader.LoadData();
    }

    /*public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!this.GetComponent<Button>().interactable)
            return;
        if (this.GetComponent<Canvas>()) { this.GetComponent<Canvas>().sortingOrder = 1021; }
        else if (this.GetComponent<Button>().interactable)
        {
            correspondingButtonAnim.SetTrigger("Highlighted");
            correspondingButtonAnim.gameObject.GetComponent<Canvas>().sortingOrder = 1021;
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!this.GetComponent<Button>().interactable)
            return;
        if (this.GetComponent<Canvas>()) { this.GetComponent<Canvas>().sortingOrder = 1020; }
        correspondingButtonAnim.SetTrigger("Normal");
        if (correspondingButtonAnim.gameObject.GetComponent<Canvas>())
            correspondingButtonAnim.gameObject.GetComponent<Canvas>().sortingOrder = 1020;
    }*/
}
