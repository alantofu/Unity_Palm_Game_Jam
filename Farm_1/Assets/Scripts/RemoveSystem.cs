using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSystem : MonoBehaviour
{

    private GridSystem gridSystem;
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = GridSystem.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Vector2Int tempPoint = gridSystem.getGridPointByPosition(CameraController.GetPlaneIntersectPos());
            // Destroy(gridSystem.objectOnGrid[tempPoint.x, tempPoint.y]);
        }
    }
}
