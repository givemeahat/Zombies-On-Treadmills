using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{
    RectTransform transform;
    Vector2 offset;
    public LevelSelectDetailsPanel detailsPanel;

    public bool isZoomedIn;

    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<RectTransform>();
    }

    public void ZoomOnPoint(Vector2 currentPos, Vector2 buttonPos, int levelNumber)
    {
        if (!detailsPanel.isActiveAndEnabled) detailsPanel.gameObject.SetActive(true);
        if (isZoomedIn)
        {
            detailsPanel.baseLevelNumber = levelNumber;
            detailsPanel.LoadData();

            Time.timeScale = 1f;
            StartCoroutine(GoToOtherPoint(currentPos));
            return;
        }
        //this.GetComponent<Animator>().StopPlayback();
        detailsPanel.baseLevelNumber = levelNumber;
        detailsPanel.LoadData();

        Time.timeScale = 1f;
        transform.pivot = currentPos;

        offset = new Vector2(-buttonPos.x - 150, buttonPos.x + 150);
        if (!isZoomedIn) StartCoroutine(ZoomOnPoint());
    }
    public void ResetScreen()
    {
        GetComponent<RectTransform>().SetLeft(0);
        GetComponent<RectTransform>().SetRight(0);
        //transform.localScale = new Vector3(1, 1, 1);
        //transform.pivot = new Vector2(.5f, .5f);
    }
    public void ZoomOut()
    {
        Time.timeScale = 1f;
        StartCoroutine(ZoomOutOfPoint());
    }
    IEnumerator ZoomOutOfPoint()
    {
        Vector3 startScale = this.transform.localScale;
        Vector3 endScale = new Vector3(1, 1, 1);
        Vector3 startPos = offset;
        Vector2 endPos = Vector2.zero;
        float time = 0;
        float waitTime = .3f;

        while (time < waitTime)
        {
            time += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(startScale, endScale, time / waitTime);
            Vector2 position = Vector2.Lerp(startPos, endPos, time / waitTime);
            this.transform.localScale = scale;
            //this.GetComponent<RectTransform>().SetLeft(position.x);
            //this.GetComponent<RectTransform>().SetRight(position.y);
            this.transform.localScale = scale;
            yield return null;
        }
        transform.pivot = new Vector2 (.5f, .5f);
        //Time.timeScale = 0f;
        isZoomedIn = false;
    }
    IEnumerator ZoomOnPoint()
    {
        Vector3 startScale = this.transform.localScale;
        Vector3 endScale = new Vector3(2, 2, 2);
        Vector3 startPos = Vector2.zero;
        Vector2 endPos = offset;
        float time = 0;
        float waitTime = .3f;

        while (time < waitTime)
        {
            time += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(startScale, endScale, time / waitTime);
            Vector2 position = Vector2.Lerp(startPos, endPos, time / waitTime);
            this.transform.localScale = scale;
            //this.GetComponent<RectTransform>().SetLeft(position.x);
            //this.GetComponent<RectTransform>().SetRight(position.y);
            yield return null;
        }
        //Time.timeScale = 0f;
        isZoomedIn = true;
    }
    IEnumerator GoToOtherPoint(Vector2 currentPos)
    {
        Vector3 startScale = this.transform.localScale;
        Vector3 endScale = new Vector3(1, 1, 1);
        Vector3 startPos = offset;
        Vector2 endPos = Vector2.zero;
        float time = 0;
        float waitTime = .3f;

        while (time < waitTime)
        {
            time += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(startScale, endScale, time / waitTime);
            Vector2 position = Vector2.Lerp(startPos, endPos, time / waitTime);
            this.transform.localScale = scale;
            //this.GetComponent<RectTransform>().SetLeft(position.x);
            //this.GetComponent<RectTransform>().SetRight(position.y);
            this.transform.localScale = scale;
            yield return null;
        }
        transform.pivot = currentPos;
        startScale = this.transform.localScale;
        endScale = new Vector3(2, 2, 2);
        startPos = Vector2.zero;
        endPos = offset;
        time = 0;
        waitTime = .3f;

        while (time < waitTime)
        {
            time += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(startScale, endScale, time / waitTime);
            Vector2 position = Vector2.Lerp(startPos, endPos, time / waitTime);
            this.transform.localScale = scale;
            //this.GetComponent<RectTransform>().SetLeft(position.x);
            //this.GetComponent<RectTransform>().SetRight(position.y);
            yield return null;
        }
        //Time.timeScale = 0f;
        isZoomedIn = true;
    }
}
