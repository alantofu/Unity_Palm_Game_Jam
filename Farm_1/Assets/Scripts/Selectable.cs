using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour
{
    private BuildSystem buildSystem;
    private PlantSystem plantSystem;

    private void Start()
    {
        buildSystem = BuildSystem.Instance;
        plantSystem = PlantSystem.Instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        if (this.gameObject.CompareTag("Forest Tree"))
        {
            plantSystem.UnhighlightForestObject();
            buildSystem.selectedForestObject = this.gameObject;
            plantSystem.selectedForestObject = this.gameObject;
        }
    }

    private void OnMouseUpAsButton()
    {
        // placementSystem.selectedObject = null;
    }
}
