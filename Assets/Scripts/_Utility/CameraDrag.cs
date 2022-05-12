using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour
{
    public float minFov;
    public float maxFov;
    public float sensitivity;

    public float speed = 5f;    
    public float speedModifier = 1f;

    Rigidbody rb;
    float horizontal;
    float vertical;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float yMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        rb.velocity = new Vector3(xMove, yMove, rb.velocity.z) * speed; // Creates velocity in direction of value equal to keypress (WASD). rb.velocity.y deals with falling + jumping by setting velocity to y. 

        float fov = GetComponent<Camera>().orthographicSize;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        GetComponent<Camera>().orthographicSize = fov;

        /*Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float left = Screen.width * 0.2f;
        float right = Screen.width - (Screen.width * 0.2f);

        if (mousePosition.x < left || mousePosition.y < left)
        {
            cameraDragging = true;
        }
        else if (mousePosition.x > right || mousePosition.y > right)
        {
            cameraDragging = true;
        }

        if (cameraDragging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

            if (move.x > 0f || move.y > 0f)
            {
                if (this.transform.position.x < outerRight || this.transform.position.y < outerRight)
                {
                    transform.Translate(move, Space.World);
                }
            }
            else
            {
                if (this.transform.position.x > outerLeft || this.transform.position.y < outerLeft)
                {
                    transform.Translate(move, Space.World);
                }
            }
        }*/

    }

    /*public float minFov;
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
    }*/
}