using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{
    RectTransform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<RectTransform>();
    }

    public void ZoomOnPoint(Vector2 currentPos)
    {
        transform.pivot = currentPos;
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
        float time = 0;
        float waitTime = .3f;

        while (time < waitTime)
        {
            time += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(startScale, endScale, time / waitTime);
            this.transform.localScale = scale;
            yield return null;
        }
        transform.pivot = Vector2.zero;
    }
    IEnumerator ZoomOnPoint()
    {
        Vector3 startScale = this.transform.localScale;
        Vector3 endScale = new Vector3(2, 2, 2);
        float time = 0;
        float waitTime = .3f;

        while (time < waitTime)
        {
            time += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(startScale, endScale, time / waitTime);
            this.transform.localScale = scale;
            yield return null;
        }
    }
}
