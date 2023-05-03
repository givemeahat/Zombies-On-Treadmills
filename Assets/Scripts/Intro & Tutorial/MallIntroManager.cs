using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class MallIntroManager : MonoBehaviour
{
    public string[] lines;
    public TextMeshProUGUI tutorialText;
    TextMeshProEffect tutorialTextEffect;
    public int lineTracker = 0;
    bool hasFinishedLine = true;

    public GameObject faderPanel;

    public GameObject continueSymbol;

    public GameObject[] tutImages;
    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = lines[lineTracker];
        tutorialTextEffect = tutorialText.gameObject.GetComponent<TextMeshProEffect>();
        tutorialTextEffect.Play();
        continueSymbol.GetComponent<Animator>().Play("Return Symbol_Active");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!hasFinishedLine)
            {
                //tutorialText.text = lines[lineTracker];
                //tutorialTextEffect.Finish();
                //hasFinishedLine = true;
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
            tutImages[1].GetComponent<Animator>().Play("Intro1_FadeOut");
        }
        if (lineTracker == 3)
        {
            tutImages[2].SetActive(true);
            tutImages[1].SetActive(false);
        }
        if (lineTracker == 4)
        {
            tutImages[3].SetActive(true);
            tutImages[2].SetActive(false);
        }
        if (lineTracker == 5)
        {
            tutImages[4].SetActive(true);
            tutImages[3].SetActive(false);
        }
        if (lineTracker == 4)
        {
            tutImages[5].SetActive(true);
            tutImages[4].SetActive(false);
        }
        int currentImage = 0;
        if (tutImages[0].activeInHierarchy) currentImage = 0;
        if (tutImages[1].activeInHierarchy) currentImage = 1;
        if (tutImages[2].activeInHierarchy) currentImage = 2;
        if (tutImages[3].activeInHierarchy) currentImage = 3;
        if (tutImages[4].activeInHierarchy) currentImage = 4;
        if (tutImages[5].activeInHierarchy) currentImage = 5;

        tutorialText.text = lines[lineTracker];
        tutorialTextEffect.Play();
        StartCoroutine(WaitToPlayActiveAnimation(currentImage));
    }

    IEnumerator WaitToPlayActiveAnimation(int currImage)
    {
        //float tutorialTimer = lines[lineTracker].Length / 50 + .5f;
        float waitTime = 0f;
        if (tutImages[currImage].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length == 0 || tutImages[currImage].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            waitTime = lines[lineTracker].Length / 20 + .2f;

        yield return new WaitForSeconds(waitTime);
        continueSymbol.GetComponent<Animator>().Play("Return Symbol_Active");
        hasFinishedLine = true;
    }
}
