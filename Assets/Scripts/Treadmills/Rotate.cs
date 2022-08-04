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

    void Update()
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
    }
    public void OnMouseDrag()
    {
        endPos = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;
        lr.SetPosition(1, endPos);
    }
    private void OnMouseUp()
    {
        lr.enabled = false;
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
