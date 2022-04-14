using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    float duration = .2f;
    bool isRotating;
    public int currentDirection;
    public Transform waypoint;

    private void Start()
    {
        currentDirection = Mathf.RoundToInt(this.gameObject.transform.eulerAngles.z);
    }

    public void OnMouseDown()
    {
        if (isRotating) return;
        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
        StartCoroutine(RotateTreadmill());
    }

    IEnumerator RotateTreadmill()
    {
        isRotating = true;
        float startRotation = this.gameObject.transform.eulerAngles.z;
        float endRotation = startRotation - 90f;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, time / duration);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, zRotation);
            yield return null;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, endRotation);
        currentDirection = Mathf.RoundToInt(endRotation);
        if (currentDirection == 360)
        {
            currentDirection = 0;
        }
        isRotating = false;
    }
}
