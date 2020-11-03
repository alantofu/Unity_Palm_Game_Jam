using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    private void OnMouseDown() {
        PlacementSystem.Instance.selectedObject = this.gameObject;
        PlacementSystem.Instance.gameObject.transform.position = transform.position;
    }

    private void OnMouseUpAsButton() {
        PlacementSystem.Instance.selectedObject = null;
    }
}
