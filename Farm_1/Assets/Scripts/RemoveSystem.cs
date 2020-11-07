using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSystem : MonoBehaviour
{

    private GridSystem gridSystem;

    void Start()
    {
        gridSystem = GridSystem.Instance;
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Vector2Int tempPoint = gridSystem.getGridPointByPosition(CameraController.GetPlaneIntersectPos());
            // Destroy(gridSystem.objectOnGrid[tempPoint.x, tempPoint.y]);
        }
    }
}
