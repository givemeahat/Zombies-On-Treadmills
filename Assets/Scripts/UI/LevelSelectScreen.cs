using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{
    RectTransform transform;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<RectTransform>();
    }

    public void ZoomOnPoint(Vector2 currentPos, Vector2 buttonPos)
    {
        transform.pivot = currentPos;

        offset = new Vector2(-buttonPos.x - 150, buttonPos.x + 150);
        StartCoroutine(ZoomOnPoint());
    }
    public void ZoomOut()
    {
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
            this.GetComponent<RectTransform>().SetLeft(position.x);
            this.GetComponent<RectTransform>().SetRight(position.y);
            this.transform.localScale = scale;
            yield return null;
        }
        transform.pivot = new Vector2 (.5f, .5f);
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
            this.GetComponent<RectTransform>().SetLeft(position.x);
            this.GetComponent<RectTransform>().SetRight(position.y);
            yield return null;
        }
    }
}
