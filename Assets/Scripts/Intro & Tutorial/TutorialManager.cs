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

    float moveAmt = 300;

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
        Vector3 newCardLocation = new Vector3(cardGroup.transform.localPosition.x - moveAmt, 0, 0);
        StartCoroutine(SwitchCards(newCardLocation));
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
        Vector3 newCardLocation = new Vector3(cardGroup.transform.localPosition.x + moveAmt, 0, 0);
        StartCoroutine(SwitchCards(newCardLocation));
    }

    IEnumerator SwitchCards(Vector3 newCardLocation)
    {
        float elapsedTime = 0;
        float waitTime = .2f;
        Vector3 startPos = cardGroup.transform.localPosition;
        Vector3 startCurrentCardScale = currentCard.transform.localScale;
        Vector3 startPrevCardScale = prevCard.transform.localScale;
        Vector3 newCurrentCardScale = new Vector3(1, 1, 1);
        Vector3 newPrevCardScale = new Vector3(.75f, .75f, .75f);

        while (elapsedTime < waitTime)
        {
            currentCard.transform.localScale = Vector3.Lerp(startCurrentCardScale, newCurrentCardScale, (elapsedTime / waitTime));
            prevCard.transform.localScale = Vector3.Lerp(startPrevCardScale, newPrevCardScale, (elapsedTime / waitTime));
            cardGroup.transform.localPosition = Vector3.Lerp(startPos, newCardLocation, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        cardGroup.transform.localPosition = newCardLocation;
        currentCard.transform.localScale = newCurrentCardScale;
        prevCard.transform.localScale = newPrevCardScale;
    }
}
