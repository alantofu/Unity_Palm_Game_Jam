using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 
public class CameraController : MonoBehaviour
{
    // https://www.youtube.com/watch?v=rnqF6S7PfFA&t=734s

    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } } // a singleton

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

    public bool isPanToDefault = false;
    public bool isZoomToDefault = false;
    public Vector3 defaultPosition = new Vector3(-34, 0, -34);
    public Vector3 defaultZoom = new Vector3(0, 40, 10);

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        newPosition = defaultPosition;
        newZoom = defaultZoom;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPanToDefault && isZoomToDefault && (defaultPosition != newPosition || newZoom != defaultZoom))
        {
            PanningToDefault();
            ZoomingToDefault();
            return;
        }
        else
        {
            isZoomToDefault = false;
            isPanToDefault = false;
        }


#if UNITY_EDITOR
    
#elif UNITY_ANDROID
        HandleTouchInput();
#endif

    }

    void HandleMouseInput()
{
    {
        {
        {
            dragStartPosition = Vector3.zero;
            dragCurrentPosition = Vector3.zer
            newPosition = transform.position;
            return;
            return;
        }

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

            newPosition.x = Mathf.Clamp(newPosition.x, -70f, -5f);
            newPosition.z = Mathf.Clamp(newPosition.z, -70f, -5f);
        }
        // need to google how Vector3.Lerp works
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    {
    {
        {
        {
            {
            {
                dragStartPositi
                dragStartPosition.y = 0;
            }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && panning == true)
            {
                dragCurrentPosition = GetPlaneIntersectPos(Input.GetTouch(0).position);
                dragCurrentPosition.y = 0;
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                newPosition.x = Mathf.Clamp(newPosition.x, -70f, -5f);
                newPosition.z = Mathf.Clamp(newPosition.z, -70f, -5f);
            }
        }
        {
        {
            Touch touch_1 = Input.GetTouch(1);
    

            Vector2 touchPrePos_1 = touch_1.position - touch_1.deltaPosition;
    

            float currentMagnitude = (touch_0.position - touch_1.position).m
    

    

            if (newZoom.y < 15 || newZoom.z > 35)
            {
            {
                newZoom.z = 35;
            }
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
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTim
    }
    }

    {
    {
        newPosition = Vector3.
        transform.position = newPosition;
    }
    }

    {
    {
        newZoom = Vector3.Lerp(
        cameraTransform.localPosition = newZoom;
    }
    }

    {
    {
    

        //Create a ray from the Mouse click position
    

        //Initialise the enter variable
    

    

        {
        {
            //Get the point that is clicked
        }
        }
        return hitPoint;
    
}
