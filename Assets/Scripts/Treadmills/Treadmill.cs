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
    public GameObject arrow;

    public GameObject rotation;

    public Animator anim;

    //purple
    Color westColor = new Color(1, 0.8156863f, 1f, 1f);
    //green
    Color northColor = new Color(0.8196079f, 1, 0.8196079f, 1);
    //orangey
    Color eastColor = new Color(1, 0.8823511f, 0.6179246f, 1);
    //reddish
    Color southColor = new Color(1, .7686275f, .7686275f, 1);

    public bool doNotDrop;

    GameObject gm;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        if (currentDirection == 0 || currentDirection == 180) anim.Play("Treadmill_Running_Vertical");
        if (currentDirection == 90 || currentDirection == 270) anim.Play("Treadmill_Running_Horizontal");
        anim.speed = 0;
        gm = GameObject.FindGameObjectWithTag("GM");
        gm.GetComponent<SpawnManager>().treadmills.Add(this.gameObject);
        spriteRend = this.GetComponent<SpriteRenderer>();
        //currentDirection = Mathf.RoundToInt(this.gameObject.transform.eulerAngles.z);
    }
    public void PlayAnimation()
    {
        anim.speed = 1.5f;
    }
    public void Rotate()
    {
        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);

        //StartCoroutine(RotateTreadmill());
        switch (currentDirection)
        {
            case 0:
                anim.Play("Treadmill_Running_Vertical");
                spriteRend.color = southColor;
                spriteRend.sprite = verticalSprite;
                arrow.transform.eulerAngles = new Vector3(arrow.transform.rotation.x, arrow.transform.rotation.y, 180);
                arrow.transform.localPosition = Vector3.zero;
                spriteRend.flipX = false;
                spriteRend.flipY = true;
                break;
            case 90:
                anim.Play("Treadmill_Running_Horizontal");
                spriteRend.color = eastColor;
                spriteRend.sprite = horizontalSprite;
                arrow.transform.eulerAngles = new Vector3(arrow.transform.rotation.x, arrow.transform.rotation.y, 270);
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, .08f, arrow.transform.localPosition.z);
                spriteRend.flipX = false;
                spriteRend.flipY = false;
                break;
            case 180:
                anim.Play("Treadmill_Running_Vertical");
                spriteRend.color = northColor;
                spriteRend.sprite = verticalSprite;
                arrow.transform.eulerAngles = new Vector3(arrow.transform.rotation.x, arrow.transform.rotation.y, 0);
                arrow.transform.localPosition = Vector3.zero;
                spriteRend.flipX = false;
                spriteRend.flipY = false;
                break;
            case 270:
                anim.Play("Treadmill_Running_Horizontal");
                spriteRend.color = westColor;
                spriteRend.sprite = horizontalSprite;
                arrow.transform.eulerAngles = new Vector3(arrow.transform.rotation.x, arrow.transform.rotation.y, 90);
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, .08f, arrow.transform.localPosition.z);
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
