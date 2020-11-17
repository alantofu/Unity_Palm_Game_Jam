using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointDebug : MonoBehaviour
{
    private GridSystem gridSystem;

    private void Start()
    {
        gridSystem = GridSystem.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        // MouseRayDebug();
        PlaneRayCastDebug();
    }

    void MouseRayDebug()
    {
        Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        mousePosFar = Camera.main.ScreenToWorldPoint(mousePosFar);
        mousePosNear = Camera.main.ScreenToWorldPoint(mousePosNear);
        Debug.Log("mousePosFar: " + mousePosFar);
        Debug.Log("mousePosNear: " + mousePosNear);
        Debug.DrawRay(mousePosFar, -(mousePosFar - mousePosNear), Color.green);
    }

    void PlaneRayCastDebug()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        //Create a ray from the Mouse click position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;

        if (plane.Raycast(ray, out enter))
        {
            // Debug.Log("Plane Raycast hit at distance: " + enter);
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);
            // Debug.Log("Intersect point: " + hitPoint);
            Debug.DrawRay(ray.origin, ray.direction * enter, Color.blue);
            Vector2Int tempVector2 = gridSystem.getGridPointByPosition(hitPoint);
            // Debug.Log("GridPoint: " + tempVector2);
            // Debug.Log("Converted Position: " + gridSystem.getPositionByGridPoint(tempVector2.x, tempVector2.y));
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        }
    }
}
