using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate : MonoBehaviour
{
    public bool isClockwise;
    public Treadmill tm;
    public GameObject indicator;

    Vector3 startPos;
    Vector3 endPos;
    Camera cam;
    LineRenderer lr;

    Vector3 camOffset = new Vector3(0, 0, 20);

    bool isMouseOver = false;
    bool isDragging = false;

    // Time management
    private float downClickTime;
    private float clickDeltaTime = 0.2F;

    private void Awake()
    {
        tm = this.GetComponentInParent<Treadmill>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        cam = Camera.main;
    }

    public void Update()
    {
        if (Input.GetMouseButton(0) && isMouseOver)
        {
            if (Mathf.Abs(endPos.y - startPos.y) < .2f)
            {
                //moving right
                if (endPos.x > startPos.x)
                {
                    tm.currentDirection = 90;
                    tm.Rotate();
                    return;
                }
                //moving left
                if (endPos.x < startPos.x)
                {
                    tm.currentDirection = 270;
                    tm.Rotate();
                    return;
                }
            }
            else if (Mathf.Abs(endPos.x - startPos.x) < .2f)
            {
                //moving up
                if (endPos.y > startPos.y)
                {
                    tm.currentDirection = 180;
                    tm.Rotate();
                    return;
                }
                //moving down
                if (endPos.y < startPos.y)
                {
                    tm.currentDirection = 0;
                    tm.Rotate();
                    return;
                }
            }
            else return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (tm.GetComponent<SpriteRenderer>().material.GetFloat("_OutlineEnabled") == 1f)
            {
                tm.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 0);
            }
        }
    }
    public void OnMouseDown()
    {
        Debug.Log("hi");
        downClickTime = Time.time;
    }
    public void OnMouseDrag()
    {
        if (Time.time - downClickTime <= clickDeltaTime)
        {
            isDragging = false;
            return;
        }
        isDragging = true;
        if (!lr.enabled)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            /*if (lr == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
            }*/
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector2(0, 0));
            lr.SetPosition(1, new Vector2(0, 0));
            //startPos = cam.ScreenToWorldPoint(Input.mousePosition + camOffset);
            //startPos = cam.ScreenToWorldPoint(Input.mousePosition);
            startPos = this.GetComponentInParent<Transform>().localPosition;
            startPos = cam.ScreenToWorldPoint(Input.mousePosition + camOffset);
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
            tm.tmm.activeTreadmill = tm;
            tm.GetComponent<AudioSource>().PlayOneShot(tm.GetComponent<AudioSource>().clip);
            lr.enabled = true;
        }
        endPos = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;
        lr.SetPosition(1, endPos);
    }
    public void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        isMouseOver = true;
        if (Input.GetMouseButton(0))
        {
            tm.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 1);

            if (tm.tmm.activeTreadmill == null) return;
            if (tm.currentDirection != tm.tmm.activeTreadmill.currentDirection)
            {
                tm.currentDirection = tm.tmm.activeTreadmill.currentDirection;
                tm.Rotate();
                tm.GetComponent<AudioSource>().PlayOneShot(tm.GetComponent<AudioSource>().clip);
            }
        }
    }
    private void OnMouseUp()
    {
        if (Time.time - downClickTime <= clickDeltaTime && !isDragging)
        {
            switch (tm.currentDirection)
            {
                case 0:
                    tm.currentDirection = 90;
                    break;
                case 90:
                    tm.currentDirection = 180;
                    break;
                case 180:
                    tm.currentDirection = 270;
                    break;
                case 270:
                    tm.currentDirection = 0;
                    break;
            }

            tm.Rotate();
        }
        lr.enabled = false;
        lr.SetPosition(0, new Vector2(0, 0));
        lr.SetPosition(1, new Vector2(0, 0));
        isMouseOver = false;
        startPos = new Vector3(0, 0, 0);
        endPos = new Vector3(0, 0, 0);
        tm.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 0);
        isDragging = false;
    }
    /*public void OnMouseExit()
    {
        indicator.SetActive(false);
    }
    public void OnMouseOver()
    {
        indicator.SetActive(true);
    }
    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (isClockwise)
        {
            tm.currentDirection += 90;
            if (tm.currentDirection == 360)
            {
                tm.currentDirection = 0;
            }
            tm.Rotate();
            return;
        }
        else
        {
            tm.currentDirection -= 90;
            if (tm.currentDirection == -90)
            {
                tm.currentDirection = 270;
            }
            tm.Rotate();
            return;
        }
    }*/
}
