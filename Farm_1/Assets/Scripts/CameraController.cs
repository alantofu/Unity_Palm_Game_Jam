using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private bool panning = false; // a condition for touch panning

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

        // HandleMouseInput();

        #if UNITY_ANDROID
                HandleTouchInput();
        #endif

    }

    void HandleMouseInput()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            newZoom = cameraTransform.localPosition;
            dragStartPosition = Vector3.zero;
            dragCurrentPosition = Vector3.zero;
            newPosition = transform.position;
            return;
        }
        // unfocus or unfollow
        followTransform = null;

        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
            if (newZoom.y < 15 || newZoom.z > 35)
            {
                newZoom.y = 15;
                newZoom.z = 35;
            }
            else if (newZoom.y > 50 || newZoom.z < 0)
            {
                newZoom.y = 50;
                newZoom.z = 0;
            }
        }

        // Returns true during the frame the user pressed the given mouse button.
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = GetPlaneIntersectPos(Input.mousePosition);
            dragStartPosition.y = 0;
        }
        // Returns whether the given mouse button is held down.
        if (Input.GetMouseButton(0))
        {
            dragCurrentPosition = GetPlaneIntersectPos(Input.mousePosition);
            dragCurrentPosition.y = 0;
            newPosition = transform.position + dragStartPosition - dragCurrentPosition;
        }
        // need to google how Vector3.Lerp works
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                panning = true;
                dragStartPosition = GetPlaneIntersectPos(Input.GetTouch(0).position);
                dragStartPosition.y = 0;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && panning == true)
            {
                dragCurrentPosition = GetPlaneIntersectPos(Input.GetTouch(0).position);
                dragCurrentPosition.y = 0;
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch_0 = Input.GetTouch(0);
            Touch touch_1 = Input.GetTouch(1);

            Vector2 touchPrePos_0 = touch_0.position - touch_0.deltaPosition;
            Vector2 touchPrePos_1 = touch_1.position - touch_1.deltaPosition;

            float prevMagnitude = (touchPrePos_0 - touchPrePos_1).magnitude;
            float currentMagnitude = (touch_0.position - touch_1.position).magnitude;

            float diff = currentMagnitude - prevMagnitude;

            newZoom += diff * zoomAmount * 0.01f;
            if (newZoom.y < 15 || newZoom.z > 35)
            {
                newZoom.y = 15;
                newZoom.z = 35;
            }
            else if (newZoom.y > 50 || newZoom.z < 0)
            {
                newZoom.y = 50;
                newZoom.z = 0;
            }

            panning = false;
            dragStartPosition = transform.position;
            dragCurrentPosition = transform.position;
            newPosition = transform.position;
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    public static Vector3 GetPlaneIntersectPos(Vector3 inputPosition)
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        //Create a ray from the Mouse click position
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);

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
