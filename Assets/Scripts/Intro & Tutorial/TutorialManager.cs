using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    //configure card switching here
    public float timeBetweenCards = .2f;
    public Vector3 activeCardScale;
    public Vector3 inactiveCardScale;

    //card switching utility
    float moveAmt = 300;
    bool hasFinishedTransition = true;

    //card variables & card group
    public GameObject[] cards;
    public int tracker = 0;
    public GameObject cardGroup;

    //tracking active/inactive cards
    GameObject currentCard;
    GameObject prevCard;
    Vector3 newCardLocation;

    //UI
    public Button forwardButton;
    public Button backwardButton;

    DataManager dm;

    private void Start()
    {
        dm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DataManager>();
        if (dm.hasFinishedTutorial) this.gameObject.SetActive(false);
        foreach (GameObject card in cards)
        {
            card.GetComponent<Animator>().playbackTime = 0;
        }
        currentCard = cards[0];
        cards[0].GetComponent<Animator>().playbackTime = 1;
    }

    private void Update()
    {
        if (dm == null)
        {
            dm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DataManager>();
        }   
    }
    public void FinishTutorial()
    {
        dm.hasFinishedTutorial = true;
        this.gameObject.SetActive(false);
    }
    public void MoveForwards()
    {
        if (!hasFinishedTransition)
        {
            StopCoroutine(SwitchCards(newCardLocation));
            cardGroup.transform.localPosition = newCardLocation;
            currentCard.transform.localScale = activeCardScale;
            prevCard.transform.localScale = inactiveCardScale;
            hasFinishedTransition = true;
        }
        if (!backwardButton.interactable) backwardButton.interactable = true;

        tracker++;
        if (tracker == cards.Length - 1)
        {
            forwardButton.interactable = false;
        }
        prevCard = currentCard;
        currentCard = cards[tracker];
        newCardLocation = new Vector3(cardGroup.transform.localPosition.x - moveAmt, 0, 0);
        StartCoroutine(SwitchCards(newCardLocation));
    }
    
    public void MoveBackwards()
    {
        if (!hasFinishedTransition)
        {
            StopCoroutine(SwitchCards(newCardLocation));
            cardGroup.transform.localPosition = newCardLocation;
            currentCard.transform.localScale = activeCardScale;
            prevCard.transform.localScale = inactiveCardScale;
            hasFinishedTransition = true;
        }
        if (!forwardButton.interactable) forwardButton.interactable = true;

        tracker--;
        if (tracker == 0)
        {
            backwardButton.interactable = false;
        }
        prevCard = currentCard;
        currentCard = cards[tracker];
        newCardLocation = new Vector3(cardGroup.transform.localPosition.x + moveAmt, 0, 0);
        StartCoroutine(SwitchCards(newCardLocation));
    }

    IEnumerator SwitchCards(Vector3 newCardLocation)
    {
        float elapsedTime = 0;
        Vector3 startPos = cardGroup.transform.localPosition;
        Vector3 startCurrentCardScale = currentCard.transform.localScale;
        Vector3 startPrevCardScale = prevCard.transform.localScale;

        while (elapsedTime < timeBetweenCards)
        {
            hasFinishedTransition = false;
            currentCard.transform.localScale = Vector3.Lerp(startCurrentCardScale, activeCardScale, (elapsedTime / timeBetweenCards));
            prevCard.transform.localScale = Vector3.Lerp(startPrevCardScale, inactiveCardScale, (elapsedTime / timeBetweenCards));
            cardGroup.transform.localPosition = Vector3.Lerp(startPos, newCardLocation, (elapsedTime / timeBetweenCards));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        currentCard.GetComponent<Animator>().playbackTime = 1;
        prevCard.GetComponent<Animator>().playbackTime = 0;
        cardGroup.transform.localPosition = newCardLocation;
        currentCard.transform.localScale = activeCardScale;
        prevCard.transform.localScale = inactiveCardScale;
        hasFinishedTransition = true;
    }
}
