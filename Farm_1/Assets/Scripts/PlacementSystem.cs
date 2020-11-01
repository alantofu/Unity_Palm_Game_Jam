using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    // https://www.youtube.com/watch?v=0yHBDZHLRbQ

    private static PlacementSystem _instance;
    public static PlacementSystem Instance { get { return _instance; } } // a singleton

    [SerializeField]
    public GameObject selectedObject;

    private Vector3 tempPosition;
    private Vector3 newPosition;

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

    private void LateUpdate()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.position = newPosition;
        }
        // if (!Input.GetMouseButton(0))
        // {
        //     selectedObject = null;
        //     transform.position = new Vector3(2, 0, 2);
        // }
    }

    private void OnMouseUp()
    {
        selectedObject = null;
    }

    private void Update()
    {
        tempPosition = CameraController.GetPlaneIntersectPos();
        newPosition.x = Mathf.RoundToInt(tempPosition.x / GridSystem.Instance.gridSize) * GridSystem.Instance.gridSize;
        newPosition.y = 0;
        newPosition.z = Mathf.RoundToInt(tempPosition.z / GridSystem.Instance.gridSize) * GridSystem.Instance.gridSize;
    }

}
