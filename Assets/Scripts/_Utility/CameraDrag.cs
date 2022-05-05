using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour
{
    public float minFov;
    public float maxFov;
    public float sensitivity;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;
    public Vector3 resetPos;
    public Quaternion resetRot;
    private Vector3 origin;
    private Vector3 difference;
    public bool drag = false;

    public Texture2D dragTex;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    Quaternion finalRotation;
    public float rotDuration;
    public float rotSpeed = .5f;

    public bool isTopDown;
    Ray2D ray;
    RaycastHit2D hit;

    private void Awake()
    {
        resetPos = this.transform.position;
        resetRot = this.transform.rotation;
        if (isTopDown == true)
        {
            this.gameObject.SetActive(false);
        }
    }
    
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            difference = (GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition)) - transform.localPosition;
            if (drag == false)
            {
                drag = true;
                origin = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else if (drag)
        {
            drag = false;
        }
        if (drag == true)
        {
            //Cursor.SetCursor(dragTex, hotSpot, cursorMode);
            this.transform.position = origin - difference;
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), Mathf.Clamp(transform.position.z, minZ, maxZ));
        }
        float fov = GetComponent<Camera>().orthographicSize;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        GetComponent<Camera>().orthographicSize = fov;
    }
}