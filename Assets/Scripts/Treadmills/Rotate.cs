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

    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lr == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
            }
            lr.enabled = true;
            lr.positionCount = 2;
            startPos = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
        }
        if (Input.GetMouseButton(0))
        {
            endPos = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(1, endPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
        }
    }*/

    public void Update()
    {
        if (Input.GetMouseButton(0) && isMouseOver)
        {
            if (Mathf.Abs(endPos.y - startPos.y) < .1f)
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
            else if (Mathf.Abs(endPos.x - startPos.x) < .1f)
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
        /*if (EventSystem.current.IsPointerOverGameObject()) return;
        /*if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
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
        lr.enabled = true;*/
    }
    public void OnMouseDrag()
    {
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
        lr.enabled = false;
        lr.SetPosition(0, new Vector2(0, 0));
        lr.SetPosition(1, new Vector2(0, 0));
        isMouseOver = false;
        startPos = new Vector3(0, 0, 0);
        endPos = new Vector3(0, 0, 0);
        tm.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 0);
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
