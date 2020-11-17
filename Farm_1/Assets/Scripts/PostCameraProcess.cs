using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostCameraProcess : MonoBehaviour
{

    public bool toggle;

    private void OnPostRender()
    {
        if (toggle)
        {
            // GridSystem.Instance.DisplayGridLines();
        }
    }
}
