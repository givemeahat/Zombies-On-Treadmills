 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Treadmill : MonoBehaviour
{
    float duration = .2f;
    bool isRotating;
    public int currentDirection;
    public Transform waypoint;

    public SpriteRenderer spriteRend;
    public Sprite verticalSprite;
    public Sprite horizontalSprite;

    //purple
    Color westColor = new Color(1, 0.8156863f, 1f, 1f);
    //green
    Color northColor = new Color(0.8196079f, 1, 0.8196079f, 1);
    //orangey
    Color eastColor = new Color(1, 1, 0.8156863f, 1);
    //reddish
    Color southColor = new Color(1, .7686275f, .7686275f, 1);

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
        if (EventSystem.current.IsPointerOverGameObject()) return;
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
                spriteRend.color = southColor;
                spriteRend.sprite = verticalSprite;
                spriteRend.flipX = false;
                spriteRend.flipY = true;
                break;
            case 90:
                spriteRend.color = eastColor;
                spriteRend.sprite = horizontalSprite;
                spriteRend.flipX = false;
                spriteRend.flipY = false;
                break;
            case 180:
                spriteRend.color = northColor;
                spriteRend.sprite = verticalSprite;
                spriteRend.flipX = false;
                spriteRend.flipY = false;
                break;
            case 270:
                spriteRend.color = westColor;
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
