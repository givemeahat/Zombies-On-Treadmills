using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LavaDrag : MonoBehaviour
{
    Vector3 origin;
    Vector3 difference;

    bool isDragging;
    public bool isOverLava;
    Camera mainCam;
    Rigidbody rb;

    public void Start()
    {
        mainCam = Camera.main;
        rb = mainCam.gameObject.GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0) && isOverLava)
        {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);
            if (!isDragging)
            {
                isDragging = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else isDragging = false;
        if (isDragging && isOverLava)
        {
            Camera.main.transform.position = origin - difference;
        }
    }

    public void MouseOverLava()
    {
        //isOverLava = true;
    }
    public void MouseNotOverLava()
    {
        //isOverLava = false;
    }
}
