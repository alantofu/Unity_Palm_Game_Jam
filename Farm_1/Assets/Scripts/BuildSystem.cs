using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    private static BuildSystem _instance;
    public static BuildSystem Instance { get { return _instance; } } // a singleton
    private GridSystem gridSystem;
    public GameObject factoryPrefab;
    public GameObject freshObject;

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
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (freshObject)
            {
                dragFinalPosition = Input.mousePosition;
                float dragDistance = Mathf.Sqrt(((dragFinalPosition.x - dragStartPosition.x) * (dragFinalPosition.x - dragStartPosition.x)
                + (dragFinalPosition.y - dragStartPosition.y) * (dragFinalPosition.y - dragStartPosition.y)));

                if (dragDistance < 10)
                {                   
                    Vector3 tempPosition = gridSystem.getRoundedPosition(CameraController.GetPlaneIntersectPos());
                    Vector2Int tempVector2 = gridSystem.getGridPointByPosition(tempPosition);
                    var obj1 = gridSystem.objectOnGrid[tempVector2.x, tempVector2.y];
                    var obj2 = gridSystem.objectOnGrid[tempVector2.x + 1, tempVector2.y];
                    var obj3 = gridSystem.objectOnGrid[tempVector2.x, tempVector2.y+1];
                    var obj4 = gridSystem.objectOnGrid[tempVector2.x+1, tempVector2.y+1];
                    gridSystem.objectOnGrid[tempVector2.x, tempVector2.y].SetActive(false);
                    gridSystem.objectOnGrid[tempVector2.x + 1, tempVector2.y].SetActive(false);
                    gridSystem.objectOnGrid[tempVector2.x, tempVector2.y + 1].SetActive(false);
                    gridSystem.objectOnGrid[tempVector2.x + 1, tempVector2.y + 1].SetActive(false);
                    freshObject.transform.position = gridSystem.getRoundedPosition(CameraController.GetPlaneIntersectPos());
                }
            }
        }
    }

    public void CreateFactory()
    {
        freshObject = Instantiate(factoryPrefab,
                                    Vector3.zero,
                                    Quaternion.identity);
    }
}
