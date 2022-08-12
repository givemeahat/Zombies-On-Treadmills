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
        Vector2 newPos = (currentPos / this.GetComponent<RectTransform>().sizeDelta);
        transform.pivot = newPos;
        StartCoroutine(ZoomOnPoint());
    }

    IEnumerator ZoomOnPoint()
    {
        Vector3 startScale = this.transform.localScale;
        Vector3 endScale = new Vector3(2, 2, 2);
        float time = 0;
        float waitTime = 1f;

        while (time < waitTime)
        {
            time += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(startScale, endScale, time / waitTime);
            this.transform.localScale = scale;
            yield return null;
        }
    }
}
