using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public string[] lines;
    public TextMeshProUGUI tutorialText;
    TextMeshProEffect tutorialTextEffect;
    public int lineTracker = 0;

    public void Start()
    {
        tutorialText.text = lines[lineTracker];
        tutorialTextEffect = tutorialText.gameObject.GetComponent<TextMeshProEffect>();
        tutorialTextEffect.Play();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) ProgressIntro();
    }
    public void ProgressIntro()
    {
        lineTracker++;
        if (lineTracker > lines.Length - 1)
        {
            //end intro here
            return;
        }
        tutorialText.text = lines[lineTracker];
        tutorialTextEffect.Play();
    }
}
