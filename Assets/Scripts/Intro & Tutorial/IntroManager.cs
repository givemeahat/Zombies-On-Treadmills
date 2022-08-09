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

    public GameObject[] tutImages;

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
        if (lineTracker == 1)
        {
            tutImages[1].SetActive(true);
            tutImages[0].SetActive(false);
        }
        if (lineTracker == 2)
        {
            tutImages[2].SetActive(true);
            tutImages[1].SetActive(false);
        }
        if (lineTracker == 3)
        {
            tutImages[2].GetComponent<Animator>().Play("Intro3_Slide");
        }
        if (lineTracker == 4)
        {
            tutImages[2].GetComponent<Animator>().Play("Intro3_Zoom Out");
        }
        if (lineTracker == 5)
        {
            tutImages[2].GetComponent<Animator>().Play("Intro3_Zombies Fall");
        }
        if (lineTracker == 6)
        {
            tutImages[2].GetComponent<Animator>().Play("Intro3_Fade Out");
        }
        if (lineTracker > lines.Length - 1)
        {
            //end intro here
            return;
        }
        int currentImage = 0;
        if (tutImages[0].activeInHierarchy) currentImage = 0;
        if (tutImages[1].activeInHierarchy) currentImage = 1;
        if (tutImages[2].activeInHierarchy) currentImage = 2;

        tutorialText.text = lines[lineTracker];
        tutorialTextEffect.Play();
        StartCoroutine(WaitToPlayActiveAnimation(currentImage));
    }

    IEnumerator WaitToPlayActiveAnimation(int currImage)
    {
        //float tutorialTimer = lines[lineTracker].Length / 50 + .5f;
        float waitTime = 0f;
        if (tutImages[currImage].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length == 0 || tutImages[currImage].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            waitTime = lines[lineTracker].Length / 25 + .2f;
        else waitTime = tutImages[currImage].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        if (currImage == 2 && lineTracker == 2) waitTime = 2f;
        if (currImage == 2 && lineTracker == 3) waitTime = 9f;

        yield return new WaitForSeconds(waitTime);
        continueSymbol.GetComponent<Animator>().Play("Return Symbol_Active");
        hasFinishedLine = true;
    }
}
