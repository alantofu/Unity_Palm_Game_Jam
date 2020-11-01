using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostCameraProcess : MonoBehaviour
{
    private void OnPostRender() {
        GridSystem.Instance.DisplayGridLines();
    }
}
