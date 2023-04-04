using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrenzyLEDIndicator : MonoBehaviour
{
    public Image ledImage;
    public Sprite onSprite;
    public Sprite offSprite;

    private void Awake()
    {
        SpawnManager spawnManager = GetComponentInParent<SpawnManager>();
        if (spawnManager.isFrenzy)
        {
            if (GetComponent<Animator>())
                this.GetComponent<Animator>().Play("LED_On");
            else this.GetComponent<Image>().sprite = onSprite;
        }
        else
        {
            if (GetComponent<Animator>())
                this.GetComponent<Animator>().Play("LED_Off");
            else this.GetComponent<Image>().sprite = offSprite;
        }
    }
}
