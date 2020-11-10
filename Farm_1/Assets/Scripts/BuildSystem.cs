using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    private static BuildSystem _instance;
    public static BuildSystem Instance { get { return _instance; } } // a singleton
    private GridSystem gridSystem;
    public GameObject[] factoryPrefab;
    public GameObject factoryParent;
    public GameObject freshObject;
    public GameObject selectedForestObject;

    public Vector3 dragStartPosition;
    public Vector3 dragFinalPosition;

    private void Awake()
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
        gridSystem = GridSystem.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (freshObject)
            {
                dragStartPosition = Input.mousePosition;

                // if mouse clicked object is not a forest object or empty
                Vector2Int clickedPoint = gridSystem.getGridPointByPosition(CameraController.GetPlaneIntersectPos());
                if (gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y] == null
                    || !gridSystem.objectOnGrid[clickedPoint.x, clickedPoint.y].CompareTag("Forest Tree"))
                {
                    selectedForestObject = null;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (freshObject)
            {
                dragFinalPosition = Input.mousePosition;
                // calculate drag distance on screen point
                float dragDistance = Mathf.Sqrt(((dragFinalPosition.x - dragStartPosition.x) * (dragFinalPosition.x - dragStartPosition.x)
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
                        newWorldMousePos = gridSystem.getRoundedPosition(CameraController.GetPlaneIntersectPos());
                    }
                    Vector2Int newFreshPoint = gridSystem.getGridPointByPosition(newWorldMousePos);
                    Vector2Int prevFreshPoint = gridSystem.getGridPointByPosition(freshObject.transform.position);
                    if (CheckFactoryBuildPoint(newFreshPoint))
                    {
                        ActivateForestMeshObj(prevFreshPoint.x, prevFreshPoint.y);
                        ActivateForestMeshObj(prevFreshPoint.x + 1, prevFreshPoint.y);
                        ActivateForestMeshObj(prevFreshPoint.x, prevFreshPoint.y + 1);
                        ActivateForestMeshObj(prevFreshPoint.x + 1, prevFreshPoint.y + 1);

                        freshObject.transform.position = gridSystem.getRoundedPosition(newWorldMousePos);

                        DeactivateForestMeshObj(newFreshPoint.x, newFreshPoint.y);
                        DeactivateForestMeshObj(newFreshPoint.x + 1, newFreshPoint.y);
                        DeactivateForestMeshObj(newFreshPoint.x, newFreshPoint.y + 1);
                        DeactivateForestMeshObj(newFreshPoint.x + 1, newFreshPoint.y + 1);
                    }
                }
                else
                {
                    selectedForestObject = null;
                }
            }
        }
    }

    public void InstantiateFactoryObj(int index)
    {
        freshObject = Instantiate(factoryPrefab[index],
                                    Vector3.zero,
                                    Quaternion.identity,
                                    factoryParent.transform);
        freshObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void DeactivateForestMeshObj(int x, int z)
    {
        GameObject tempObj = gridSystem.objectOnGrid[x, z];
        if (tempObj != null && tempObj.CompareTag("Forest Tree"))
        {
            tempObj.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void ActivateForestMeshObj(int x, int z)
    {
        GameObject tempObj = gridSystem.objectOnGrid[x, z];
        if (tempObj != null && tempObj.CompareTag("Forest Tree"))
        {
            tempObj.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void PlaceFactoryObj()
    {
        Vector2Int newFreshPoint = gridSystem.getGridPointByPosition(freshObject.transform.position);
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
        freshObject = null;
        selectedForestObject = null;
    }

    private bool CheckFactoryBuildPoint(Vector2Int gridPoint)
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                // if it is not empty or forest object
                if (!(gridSystem.objectOnGrid[gridPoint.x + i, gridPoint.y + j] == null
                || gridSystem.objectOnGrid[gridPoint.x + i, gridPoint.y + j].CompareTag("Forest Tree")))
                {
                    return false;
                }
            }
        }
        return true;
    }


}
