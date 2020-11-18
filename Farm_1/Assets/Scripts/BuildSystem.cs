﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSystem : MonoBehaviour
{
    private static BuildSystem _instance;
    public static BuildSystem Instance { get { return _instance; } } // a singleton
    private GridSystem gridSystem;
    private SelectSystem selectSystem;

    public List<GameObject> factoryPrefabList;
    public GameObject factoryParent;
    public GameObject freshObject;
    public GameObject selectedForestObject;

    public Vector3 dragStartPosition;
    public Vector3 dragFinalPosition;
    public float dragDistance;

    public bool canBuild = false;

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
        factoryPrefabList = new List<GameObject>();
        gridSystem = GridSystem.Instance;
        selectSystem = SelectSystem.Instance;
        if (factoryPrefabList.Count < 1)
        {
            factoryPrefabList.Clear();
            factoryPrefabList.Add(Resources.Load("Prefabs/Lipstick Factory", typeof(GameObject)) as GameObject);
            factoryPrefabList.Add(Resources.Load("Prefabs/Chocolate Factory", typeof(GameObject)) as GameObject);
        }
    }

    void Update()
    {
        // #if UNITY_EDITOR
        // clickHandler();
        // #elif UNITY_ANDROID || UNITY_IOS
        TouchHandler();
        // #endif

        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (freshObject)
        //     {
        //         dragStartPosition = Input.mousePosition;

        //         // if mouse clicked object is not a forest object or empty
        //         Vector2Int clickedPoint = gridSystem.getGridPointByPosition(CameraController.GetPlaneIntersectPos(Input.mousePosition));
        //         if (gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y] == null
        //             || !gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y].CompareTag("Forest Tree"))
        //         {
        //             selectedForestObject = null;
        //         }
        //     }
        // }
        // if (Input.GetMouseButtonUp(0))
        // {
        //     if (freshObject)
        //     {
        //         dragFinalPosition = Input.mousePosition;
        //         // calculate drag distance on screen point
        //         float dragDistance = Mathf.Sqrt(((dragFinalPosition.x - dragStartPosition.x) * (dragFinalPosition.x - dragStartPosition.x)
        //         + (dragFinalPosition.y - dragStartPosition.y) * (dragFinalPosition.y - dragStartPosition.y)));

        //         if (dragDistance < 10)
        //         {
        //             Vector3 newWorldMousePos;
        //             if (selectedForestObject)
        //             {
        //                 newWorldMousePos = selectedForestObject.transform.position;
        //             }
        //             else
        //             {
        //                 newWorldMousePos = gridSystem.getRoundedPosition(CameraController.GetPlaneIntersectPos(Input.mousePosition));
        //             }
        //             Vector2Int newFreshPoint = gridSystem.getGridPointByPosition(newWorldMousePos);
        //             Vector2Int prevFreshPoint = gridSystem.getGridPointByPosition(freshObject.transform.position);
        //             if (CheckFactoryBuildPoint(newFreshPoint))
        //             {
        //                 ActivateForestMeshObj(prevFreshPoint.x, prevFreshPoint.y);
        //                 ActivateForestMeshObj(prevFreshPoint.x + 1, prevFreshPoint.y);
        //                 ActivateForestMeshObj(prevFreshPoint.x, prevFreshPoint.y + 1);
        //                 ActivateForestMeshObj(prevFreshPoint.x + 1, prevFreshPoint.y + 1);

        //                 freshObject.transform.position = gridSystem.getRoundedPosition(newWorldMousePos);

        //                 DeactivateForestMeshObj(newFreshPoint.x, newFreshPoint.y);
        //                 DeactivateForestMeshObj(newFreshPoint.x + 1, newFreshPoint.y);
        //                 DeactivateForestMeshObj(newFreshPoint.x, newFreshPoint.y + 1);
        //                 DeactivateForestMeshObj(newFreshPoint.x + 1, newFreshPoint.y + 1);
        //             }
        //         }
        //         else
        //         {
        //             selectedForestObject = null;
        //         }
        //     }
        // }
    }

    void TouchHandler()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }

        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
            {
                dragStartPosition = Input.GetTouch(0).position;

                if (freshObject)
                {
                    Vector2Int clickedPoint = gridSystem.GetGridPointByPosition(CameraController.GetPlaneIntersectPos(dragStartPosition));
                    // if mouse clicked object is a forest object or not empty
                    if (gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y] == null)
                    {
                        selectedForestObject = null;
                    }
                    else if (gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y].CompareTag("Forest Tree"))
                    {
                        selectedForestObject = gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y];
                    }
                }
            }
            if (Input.GetTouch(0).phase.Equals(TouchPhase.Ended))
            {
                dragFinalPosition = Input.GetTouch(0).position;

                // calculate drag distance on screen point
                dragDistance = Mathf.Sqrt(((dragFinalPosition.x - dragStartPosition.x) * (dragFinalPosition.x - dragStartPosition.x)
                + (dragFinalPosition.y - dragStartPosition.y) * (dragFinalPosition.y - dragStartPosition.y)));

                if (dragDistance < 10)
                {
                    Vector3 newWorldMousePos;
                    if (selectedForestObject)
                    {
                        newWorldMousePos = selectedForestObject.transform.position;
                    }
                    else
                    {
                        newWorldMousePos = gridSystem.GetRoundedPosition(CameraController.GetPlaneIntersectPos(dragFinalPosition));
                    }
                    Vector2Int newFreshPoint = gridSystem.GetGridPointByPosition(newWorldMousePos);
                    Vector2Int prevFreshPoint = gridSystem.GetGridPointByPosition(freshObject.transform.position);
                    MovePlanningFactory(prevFreshPoint, newFreshPoint, freshObject);
                }
                else
                {
                    selectedForestObject = null;
                }
            }
        }
    }

    void ClickHandler()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = Input.mousePosition;
            if (freshObject)
            {
                Vector2Int clickedPoint = gridSystem.GetGridPointByPosition(CameraController.GetPlaneIntersectPos(dragStartPosition));
                // if mouse clicked object is a forest object or not empty
                if (gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y] == null)
                {
                    selectedForestObject = null;
                }
                else if (gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y].CompareTag("Forest Tree"))
                {
                    selectedForestObject = gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y];
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragFinalPosition = Input.mousePosition;

            // calculate drag distance on screen point
            dragDistance = Mathf.Sqrt(((dragFinalPosition.x - dragStartPosition.x) * (dragFinalPosition.x - dragStartPosition.x)
            + (dragFinalPosition.y - dragStartPosition.y) * (dragFinalPosition.y - dragStartPosition.y)));

            if (dragDistance < 10)
            {
                Vector3 newWorldMousePos;
                if (selectedForestObject)
                {
                    newWorldMousePos = selectedForestObject.transform.position;
                }
                else
                {
                    newWorldMousePos = gridSystem.GetRoundedPosition(CameraController.GetPlaneIntersectPos(dragFinalPosition));
                }
                Vector2Int newFreshPoint = gridSystem.GetGridPointByPosition(newWorldMousePos);
                Vector2Int prevFreshPoint = gridSystem.GetGridPointByPosition(freshObject.transform.position);
                MovePlanningFactory(prevFreshPoint, newFreshPoint, freshObject);
            }
            else
            {
                selectedForestObject = null;
            }
        }


    }

    public void InstantiateFactoryObj(int index)
    {
        freshObject = Instantiate(factoryPrefabList[index],
                                    gridSystem.GetPositionByGridPoint(45, 49),
                                    Quaternion.identity,
                                    factoryParent.transform);
        freshObject.layer = LayerMask.NameToLayer("Ignore Raycast");

    }

    public void PlaceFactoryObj()
    {
        Vector2Int newFreshPoint = gridSystem.GetGridPointByPosition(freshObject.transform.position);
        freshObject.layer = LayerMask.NameToLayer("Default");
        freshObject.name = "Factory (" + newFreshPoint.x.ToString() + ", " + newFreshPoint.y.ToString() + ")";
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Destroy(gridSystem.objectOnGrid[newFreshPoint.x + i, newFreshPoint.y + j]);
                gridSystem.objectOnGrid[newFreshPoint.x + i, newFreshPoint.y + j] = freshObject;
            }
        }
        freshObject.GetComponent<Earning>().enabled = true;
        freshObject = null;
        selectedForestObject = null;
    }


    public void CancelFactoryPlanning()
    {
        if (freshObject != null)
        {
            Vector2Int freshObjPoint = gridSystem.GetGridPointByPosition(freshObject.transform.position);
            int size = Mathf.RoundToInt(freshObject.GetComponent<BoxCollider>().size.x);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    ActivateDeactivateForestMesh(freshObjPoint.x + i, freshObjPoint.y + j, true);
                }
            }
            Destroy(freshObject);
            freshObject = null;
            selectedForestObject = null;
        }
    }

    private void MovePlanningFactory(Vector2Int prevPoint, Vector2Int newPoint, GameObject freshObject)
    {
        int size = Mathf.RoundToInt(freshObject.GetComponent<BoxCollider>().size.x);
        if (CheckFactoryBuildPoint(newPoint, size))
        { // check are the new points all forest obj
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    ActivateDeactivateForestMesh(prevPoint.x + i, prevPoint.y + j, true);
                    ActivateDeactivateForestMesh(newPoint.x + i, newPoint.y + j, false);
                }
            }
            Debug.Log("New Point: " + gridSystem.GetPositionByGridPoint(newPoint.x, newPoint.y));
            freshObject.transform.position = gridSystem.GetPositionByGridPoint(newPoint.x, newPoint.y);
        }
    }

    private void ActivateDeactivateForestMesh(int x, int z, bool toggle)
    {
        GameObject tempObj = gridSystem.objectOnGrid[x, z];
        if (tempObj != null && tempObj.CompareTag("Forest Tree"))
        {
            tempObj.transform.GetChild(0).gameObject.SetActive(toggle);
        }
    }

    private bool CheckFactoryBuildPoint(Vector2Int gridPoint, int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // if it is not (empty or forest object)
                if (gridSystem.objectOnGrid[gridPoint.x + i, gridPoint.y + j] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }


}
