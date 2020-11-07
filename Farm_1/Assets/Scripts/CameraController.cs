using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class CameraController : MonoBehaviour
{
    // https://www.youtube.com/watch?v=rnqF6S7PfFA&t=734s

    public static CameraController instance;
    public Transform followTransform;
    public Transform cameraTransform;
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed; // change camera movement speed
    public float movementTime; // change camera smoothness movement
    public Vector3 newPosition;
    public Vector3 zoomAmount;
    public Vector3 newZoom;
    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTransform != null)
        {
            transform.position = new Vector3(Mathf.RoundToInt(followTransform.position.x - 35),
                                                0,
                                                Mathf.RoundToInt(followTransform.position.z - 35));
        }
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        // unfocus or unfollow
        followTransform = null;

        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        // Returns true during the frame the user pressed the given mouse button.
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = GetPlaneIntersectPos();
            dragStartPosition.y = 0;
        }
        // Returns whether the given mouse button is held down.
        if (Input.GetMouseButton(0))
        {
            dragCurrentPosition = GetPlaneIntersectPos();
            dragCurrentPosition.y = 0;
            newPosition = transform.position + dragStartPosition - dragCurrentPosition;
        }
        // need to google how Vector3.Lerp works
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }


    }

    public static Vector3 GetPlaneIntersectPos()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        //Create a ray from the Mouse click position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;

        Vector3 hitPoint = Vector3.zero;

        if (plane.Raycast(ray, out enter))
        {
            //Get the point that is clicked
            hitPoint = ray.GetPoint(enter);
        }

        return hitPoint;
    }

}
