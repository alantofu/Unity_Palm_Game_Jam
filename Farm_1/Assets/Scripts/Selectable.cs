using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    private PlacementSystem placementSystem;

    private void Start()
    {
        placementSystem = PlacementSystem.Instance;
    }

    private void OnMouseDown()
    {
        // placementSystem.selectedObject = this.gameObject;
        // placementSystem.gameObject.transform.position = transform.position;
        // Debug.Log("Seleted " + this.name);
        Destroy(this.gameObject);
    }

    private void OnMouseUpAsButton()
    {
        // placementSystem.selectedObject = null;
    }
}
