using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public string[] lines;
    public TextMeshProUGUI tutorialText;
    TextMeshProEffect tutorialTextEffect;
    public int lineTracker = 0;
    bool hasFinishedLine = true;

    public GameObject continueSymbol;

    public void Start()
    {
        tutorialText.text = lines[lineTracker];
        tutorialTextEffect = tutorialText.gameObject.GetComponent<TextMeshProEffect>();
        tutorialTextEffect.Play();
        continueSymbol.GetComponent<Animator>().Play("Return Symbol_Active");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!hasFinishedLine)
            {
                tutorialText.text = lines[lineTracker];
                tutorialTextEffect.Finish();
                hasFinishedLine = true;
                return;
            }
            else
            {
                hasFinishedLine = false;
                continueSymbol.GetComponent<Animator>().Play("Return Symbol_Inactive");
                ProgressIntro();
            }
        }
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
        StartCoroutine(WaitToPlayActiveAnimation());
    }

    IEnumerator WaitToPlayActiveAnimation()
    {
        float tutorialTimer = lines[lineTracker].Length / 50 + .5f;
        yield return new WaitForSeconds(tutorialTimer);
        continueSymbol.GetComponent<Animator>().Play("Return Symbol_Active");
        hasFinishedLine = true;
    }
}
