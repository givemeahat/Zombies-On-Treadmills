using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenAspectRatio : MonoBehaviour
{
    public Image background;
    public Sprite bg43;
    public Sprite bg169;
    // Start is called before the first frame update
    void Start()
    {
        if (Camera.main.aspect >= 1.7)
            background.sprite = bg169;
        else
            background.sprite = bg43;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
