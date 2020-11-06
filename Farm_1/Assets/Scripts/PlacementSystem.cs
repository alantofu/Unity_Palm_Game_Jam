using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    // https://www.youtube.com/watch?v=0yHBDZHLRbQ

    private static PlacementSystem _instance;
    public static PlacementSystem Instance { get { return _instance; } } // a singleton

    public GameObject selectedObject;
    public GameObject promptPlane;
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
        // if (selectedObject != null)
        // {
        //     selectedObject.transform.position = newPosition;
        // }
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    private void OnEnable()
    {
        selectedObject = null;
    }

    private void OnDisable()
    {
        selectedObject = null;
    }

    private void OnMouseUp()
    {
        selectedObject = null;
    }

    public void createPromptPlane()
    {
        GameObject oldPromptPlane = GameObject.Find("Prompt Plane");
        if (oldPromptPlane != null)
        {
            Destroy(oldPromptPlane);
        }
        // Instantiate(promptPlane, new Transform());
    }

    private void Update()
    {
        tempPosition = CameraController.GetPlaneIntersectPos();
        newPosition.x = Mathf.RoundToInt(tempPosition.x / GridSystem.Instance.gridSize) * GridSystem.Instance.gridSize;
        newPosition.y = 0;
        newPosition.z = Mathf.RoundToInt(tempPosition.z / GridSystem.Instance.gridSize) * GridSystem.Instance.gridSize;
    }

}
