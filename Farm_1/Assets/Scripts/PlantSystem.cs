using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantSystem : MonoBehaviour
{
    private static PlantSystem _instance;
    public static PlantSystem Instance { get { return _instance; } } // a singleton

    private GridSystem gridSystem;

    public GameObject palmPrefab;
    public Transform palmParent;

    public GameObject selectedForestObject;
    public GameObject selectedChildObj;

    public GameObject buySeedPanel;

    public Vector3 dragStartPosition;
    public Vector3 dragFinalPosition;

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

    private void Start()
    {
        gridSystem = GridSystem.Instance;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            dragStartPosition = Input.mousePosition;
            dragFinalPosition = Input.mousePosition;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedForestObject)
            {
                dragStartPosition = Input.mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedForestObject)
            {
                dragFinalPosition = Input.mousePosition;
                // calculate drag distance on screen point
                float dragDistance = Mathf.Sqrt(((dragFinalPosition.x - dragStartPosition.x) * (dragFinalPosition.x - dragStartPosition.x)
                + (dragFinalPosition.y - dragStartPosition.y) * (dragFinalPosition.y - dragStartPosition.y)));

                if (dragDistance < 10)
                {
                    // HighlightForestObject();
                    if (!buySeedPanel.activeSelf)
                    {
                        buySeedPanel.SetActive(true);
                    }
                }
                else
                {
                    // UnhighlightForestObject();
                    selectedForestObject = null;
                }
            }
        }
    }

    public void HighlightForestObject()
    {
        if (selectedForestObject && !BuildSystem.Instance.gameObject.activeSelf)
        {
            selectedChildObj = selectedForestObject.transform.GetChild(1).gameObject;
            selectedChildObj.SetActive(true);
            selectedForestObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void UnhighlightForestObject()
    {
        if (selectedForestObject && !BuildSystem.Instance.gameObject.activeSelf)
        {
            selectedChildObj = selectedForestObject.transform.GetChild(1).gameObject;
            selectedChildObj.SetActive(false);
            selectedForestObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void StopAllHighlight()
    { // fix bug where need to unhighlight but without other condition
        if (selectedForestObject)
        {
            selectedChildObj = selectedForestObject.transform.GetChild(1).gameObject;
            selectedChildObj.SetActive(false);
            selectedForestObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void PlacePalmObj()
    {
        UnhighlightForestObject();

        Vector2Int selectedPoint = gridSystem.getGridPointByPosition(selectedForestObject.transform.position);

        if (gridSystem.objectOnGrid[selectedPoint.x, selectedPoint.y].CompareTag("Forest Tree"))
        {
            GameObject newObj = Instantiate(palmPrefab,
                                        gridSystem.getPositionByGridPoint(selectedPoint.x, selectedPoint.y),
                                        Quaternion.identity,
                                        palmParent);
            newObj.name = "Palm Tree (" + selectedPoint.x.ToString() + ", " + selectedPoint.y.ToString() + ")";
            gridSystem.objectOnGrid[selectedPoint.x, selectedPoint.y] = newObj;

            Destroy(selectedForestObject);
        }

        selectedForestObject = null;
        selectedChildObj = null;
    }

    private void OnDisable()
    {
        UnhighlightForestObject();
        selectedForestObject = null;
        selectedChildObj = null;
    }
}
