using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DirectoryTextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator correspondingButtonAnim;



    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!this.GetComponent<Button>().interactable)
            return;

        else if (this.GetComponent<Button>().interactable)
            correspondingButtonAnim.SetTrigger("Highlighted");
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!this.GetComponent<Button>().interactable)
            return;
        correspondingButtonAnim.SetTrigger("Normal");
    }
}
