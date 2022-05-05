using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    float duration = .2f;
    bool isRotating;
    public int currentDirection;
    public Transform waypoint;

    public SpriteRenderer spriteRend;
    public Sprite verticalSprite;
    public Sprite horizontalSprite;

    public bool doNotDrop;

    GameObject gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        gm.GetComponent<SpawnManager>().treadmills.Add(this.gameObject);
        spriteRend = this.GetComponent<SpriteRenderer>();
        //currentDirection = Mathf.RoundToInt(this.gameObject.transform.eulerAngles.z);
    }

    public void OnMouseDown()
    {
        Debug.Log("H???");
        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
        currentDirection += 90;
        if (currentDirection == 360)
        {
            currentDirection = 0;
        }
        //StartCoroutine(RotateTreadmill());
        switch (currentDirection)
        {
            case 0:
                spriteRend.sprite = verticalSprite;
                spriteRend.flipX = false;
                spriteRend.flipY = true;
                break;
            case 90:
                spriteRend.sprite = horizontalSprite;
                spriteRend.flipX = false;
                spriteRend.flipY = false;
                break;
            case 180:
                spriteRend.sprite = verticalSprite;
                spriteRend.flipX = false;
                spriteRend.flipY = false;
                break;
            case 270:
                spriteRend.sprite = horizontalSprite;
                spriteRend.flipX = true;
                spriteRend.flipY = false;
                break;
        }
    }

    /*IEnumerator RotateTreadmill()
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
    }*/
}
