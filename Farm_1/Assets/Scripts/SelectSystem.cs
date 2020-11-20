using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSystem : MonoBehaviour
{
    private static SelectSystem _instance;
    public static SelectSystem Instance { get { return _instance; } } // a singleton

    private BuildSystem buildSystem;
    private PlantSystem plantSystem;
    private ManufactureSystem manufactureSystem;

    public LayerMask layerMask;
    public GameObject selectedObject;
    public GameObject selectedSystemObject;

    public Vector3 dragStartPosition;
    public Vector3 dragFinalPosition;
    public float dragDistance;

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

    void Start()
    {
        buildSystem = BuildSystem.Instance;
        plantSystem = PlantSystem.Instance;
        manufactureSystem = ManufactureSystem.Instance;
    }

    void Update()
    {
        selectedSystemObject = EventSystem.current.currentSelectedGameObject;
        // #if UNITY_ANDROID || UNITY_IOS
                // TouchHandler();
        // #elif UNITY_EDITOR
        ClickHandler();
        // #endif
    }

    void TouchHandler()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }
        RaycastHit hit = new RaycastHit();
        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
            {
                dragStartPosition = Input.GetTouch(0).position;

                // Construct a ray from the current touch coordinates.
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    selectedObject = hit.transform.gameObject;
                }
            }
            if (Input.GetTouch(0).phase.Equals(TouchPhase.Ended))
            {
                dragFinalPosition = Input.GetTouch(0).position;

                // calculate drag distance on screen point
                dragDistance = Mathf.Sqrt(((dragFinalPosition.x - dragStartPosition.x) * (dragFinalPosition.x - dragStartPosition.x)
                + (dragFinalPosition.y - dragStartPosition.y) * (dragFinalPosition.y - dragStartPosition.y)));

                if (dragDistance < 10 && selectedObject)
                {
                    SendOnClickResponse(selectedObject);
                }
                selectedObject = null;

            }
        }
    }

    void ClickHandler()
    {
        if (EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        RaycastHit hit = new RaycastHit();

        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = Input.mousePosition;

            // Construct a ray from the current touch coordinates.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                selectedObject = hit.transform.gameObject;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragFinalPosition = Input.mousePosition;

            // calculate drag distance on screen point
            dragDistance = Mathf.Sqrt(((dragFinalPosition.x - dragStartPosition.x) * (dragFinalPosition.x - dragStartPosition.x)
            + (dragFinalPosition.y - dragStartPosition.y) * (dragFinalPosition.y - dragStartPosition.y)));

            if (dragDistance < 10 && selectedObject)
            {
                SendOnClickResponse(selectedObject);
            }
            selectedObject = null;
        }
    }

    public void SendOnClickResponse(GameObject selectedGameObj)
    {
        switch (selectedGameObj.tag)
        {
            case "Grass":
                if (plantSystem.gameObject.activeSelf)
                {
                    plantSystem.SelectDeselectRemovableObj(selectedGameObj);
                }
                break;

            case "Forest Tree":
                if (plantSystem.gameObject.activeSelf)
                {
                    plantSystem.SelectDeselectRemovableObj(selectedGameObj);
                }
                break;

            case "Palm Oil Tree":
                if (!plantSystem.gameObject.activeSelf)
                {
                    selectedGameObj.GetComponent<Farming>().OnClickResponse();
                }
                break;

            case "Factory":
                if (!plantSystem.gameObject.activeSelf)
                {
                    manufactureSystem.selectedFactoryObject = selectedGameObj; // select first
                    
                    if (selectedGameObj.GetComponent<Earning>().isCollectable)
                    {
                        selectedGameObj.GetComponent<Earning>().OnClickResponse();
                    }
                    else if (!selectedGameObj.GetComponent<Earning>().isManufacturing
                    && selectedGameObj.GetComponent<Building>().isConstructed)
                    { // if it is not producing money and it is constructed
                        if (selectedGameObj.name.StartsWith("Lipstick"))
                        {
                            GameManager.Instance.lipstickFactoryPanel.SetActive(true);
                        }
                        else if (selectedGameObj.name.StartsWith("Chocolate"))
                        {
                            GameManager.Instance.chocolateFactoryPanel.SetActive(true);
                        }
                    }
                }
                break;

            default:
                return;
        }
    }

}
