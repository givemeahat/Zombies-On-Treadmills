using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] cards;
    public int tracker = 0;
    public GameObject cardGroup;
    public GameObject currentCard;
    GameObject prevCard;

    float moveAmt;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D)) MoveForwards();
        if (Input.GetKeyUp(KeyCode.A)) MoveBackwards();
    }

    public void MoveForwards()
    {
        //if (tracker++ > cards.Length - 1) return;
        /*if (tracker++ == cards.Length - 1)
        {
            //end tutorial code here
        }*/
        prevCard = currentCard;
        tracker++;
        currentCard = cards[tracker];
        moveAmt = 350;
        StartCoroutine(SwitchCards());
    }
    
    public void MoveBackwards()
    {
        /*if (tracker-- < 0) return;
        if (tracker-- == cards.Length - 1)
        {
            tracker = 0;
            currentCard = cards[tracker];
            return;
        }*/
        prevCard = currentCard;
        tracker--;
        currentCard = cards[tracker];
        moveAmt = -350;
        StartCoroutine(SwitchCards());
    }

    IEnumerator SwitchCards()
    {
        float elapsedTime = 0;
        float waitTime = .5f;
        Vector3 newCardLocation = new Vector3(cardGroup.transform.localPosition.x +- moveAmt, 0, 0);
        Vector3 newCurrentCardScale = new Vector3(1, 1, 1);
        Vector3 newPrevCardScale = new Vector3(.75f, .75f, .75f);

        while (elapsedTime < waitTime)
        {
            currentCard.transform.localScale = Vector3.Slerp(currentCard.transform.localScale, newCurrentCardScale, (elapsedTime / waitTime));

            prevCard.transform.localScale = Vector3.Slerp(prevCard.transform.localScale, newPrevCardScale, (elapsedTime / waitTime));

            cardGroup.transform.localPosition = Vector3.Slerp(cardGroup.transform.localPosition, newCardLocation, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        currentCard.transform.localScale = newCurrentCardScale;
        prevCard.transform.localScale = newPrevCardScale;
        cardGroup.transform.localPosition = newCardLocation;
    }
}
