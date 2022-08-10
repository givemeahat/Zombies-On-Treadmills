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

    Vector3 camOffset = new Vector3(0, 0, 10);

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
                    Debug.Log("moving right");
                    tm.currentDirection = 90;
                    tm.Rotate();
                    return;
                }
                //moving left
                if (endPos.x < startPos.x)
                {
                    Debug.Log("moving left");
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
                    Debug.Log("moving up");
                    tm.currentDirection = 180;
                    tm.Rotate();
                    return;
                }
                //moving down
                if (endPos.y < startPos.y)
                {
                    Debug.Log("moving down");
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
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
        lr.enabled = true;
        lr.positionCount = 2;
        startPos = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;
        lr.SetPosition(0, startPos);
        lr.useWorldSpace = true;
        tm.tmm.activeTreadmill = tm;
        tm.GetComponent<AudioSource>().PlayOneShot(tm.GetComponent<AudioSource>().clip);
    }
    public void OnMouseDrag()
    {
        endPos = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;
        lr.SetPosition(1, endPos);
    }
    public void OnMouseOver()
    {
        isMouseOver = true;
        if (Input.GetMouseButton(0))
        {
            tm.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 1);

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
